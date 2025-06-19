using Anesis.ApiService.Domain.DTOs.Menus;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionService _permissionService;
        private readonly IRepository<MenuTab> _menuTabRepo;
        private readonly IRepository<MenuItem> _menuItemRepo;
        private readonly IRepository<UserQuickLink> _quickLinksRepo;

        public MenuService(IMapper mapper, IUnitOfWork unitOfWork, IPermissionService permissionService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _permissionService = permissionService;
            _menuTabRepo = _unitOfWork.GetRepository<MenuTab>();
            _menuItemRepo = _unitOfWork.GetRepository<MenuItem>();
            _quickLinksRepo = _unitOfWork.GetRepository<UserQuickLink>();
        }

        // MENU ITEMS
        public async Task<List<MenuItemDto>> GetMenusForUserAsync(string accountId, CancellationToken cancellationToken = default)
        {
            var permissionIds = await _permissionService.GetPermissionIdsForUserAsync(accountId, cancellationToken);

            var filter = new MenuItemFilterDto() { VisibleOnly = true, PermissionIds = permissionIds };
            var query = SearchMenuItems(filter);

            return await _mapper.ProjectTo<MenuItemDto>(query)
                .OrderBy(x => x.TabDisplayOrder)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }


        // QUICK LINKS
        public async Task<List<QuickLinkDto>> GetQuickLinksForUserAsync(string accountId, CancellationToken cancellationToken = default)
        {
            var query = _quickLinksRepo.All(true)
                .Where(x => x.UserId == accountId);

            return await _mapper.ProjectTo<QuickLinkDto>(query)
                .OrderBy(x => x.GroupName)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> CreateQuickLinkAsync(QuickLinkEditDto model, CancellationToken cancellationToken = default)
        {
            // Update the display order of existing quick links in the same group
            await _quickLinksRepo.All()
                .Where(x => x.UserId == model.UserId && x.GroupName == model.GroupName && x.DisplayOrder >= model.DisplayOrder)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder + 1), cancellationToken);

            var newQuickLink = model.ToQuickLinkModel();

            _quickLinksRepo.Insert(newQuickLink);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateQuickLinkAsync(QuickLinkEditDto model, CancellationToken cancellationToken = default)
        {
            // Find the existing quick link by ID
            var existingQuickLink = await FindQuickLinkById(model.Id, cancellationToken);
            if (existingQuickLink == null)
            {
                return false;
            }

            if (model.GroupName != existingQuickLink.GroupName)
            {
                // If the group name has changed, we need to adjust the display order for both the old and new groups
                await _quickLinksRepo.All()
                    .Where(x => x.Id != model.Id && x.UserId == model.UserId
                        && x.GroupName == model.GroupName && x.DisplayOrder >= model.DisplayOrder)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder + 1), cancellationToken);

                await _quickLinksRepo.All()
                    .Where(x => x.Id != model.Id && x.UserId == model.UserId
                        && x.GroupName == existingQuickLink.GroupName && x.DisplayOrder >= existingQuickLink.DisplayOrder)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder - 1), cancellationToken);
            }
            else if (model.DisplayOrder > existingQuickLink.DisplayOrder)
            {
                // If the display order has changed and is greater than the existing one, we need to adjust the display order of other links in the same group
                await _quickLinksRepo.All()
                    .Where(x => x.Id != model.Id && x.UserId == model.UserId && x.GroupName == model.GroupName
                        && x.DisplayOrder >= existingQuickLink.DisplayOrder && x.DisplayOrder <= model.DisplayOrder)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder - 1), cancellationToken);
            }
            else if (model.DisplayOrder < existingQuickLink.DisplayOrder)
            {
                // If the display order has changed and is less than the existing one, we need to adjust the display order of other links in the same group
                await _quickLinksRepo.All()
                    .Where(x => x.Id != model.Id && x.UserId == model.UserId && x.GroupName == model.GroupName
                        && x.DisplayOrder >= model.DisplayOrder && x.DisplayOrder <= existingQuickLink.DisplayOrder)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder + 1), cancellationToken);
            }

            model.ApplyChangesTo(existingQuickLink);

            _quickLinksRepo.Update(existingQuickLink);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteQuickLinkAsync(int id, CancellationToken cancellationToken = default)
        {
            // Delete the quick link by ID
            var quickLink = await _quickLinksRepo.DeleteAsync(cancellationToken, id);
            if (quickLink == null)
            {
                return false;
            }

            // Adjust the display order of other quick links in the same group
            await _quickLinksRepo.All()
               .Where(x => x.Id != id && x.UserId == quickLink.UserId && x.GroupName == quickLink.GroupName && x.DisplayOrder >= quickLink.DisplayOrder)
               .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.DisplayOrder, x => x.DisplayOrder - 1), cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        // SUPPORT METHODS
        private IQueryable<MenuItem> SearchMenuItems(MenuItemFilterDto filter)
        {
            return _menuItemRepo.All(true)
                .Where(x => x.MenuTab.Visible)
                .WhereIf(x => x.MenuTabId == filter.MenuTabId, filter.MenuTabId > 0)
                .WhereIf(x => x.Visible, filter.VisibleOnly)
                .WhereIf(x => x.PermissionId == null || filter.PermissionIds.Contains(x.PermissionId.Value), filter.PermissionIds.Count > 0);
        }

        private async Task<UserQuickLink> FindQuickLinkById(int id, CancellationToken cancellationToken = default)
        {
            return await _quickLinksRepo.FindAsync(cancellationToken, id);
        }
    }
}
