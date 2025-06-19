using Anesis.ApiService.Domain.DTOs.Menus;

namespace Anesis.ApiService.Services.IServices
{
    public interface IMenuService
    {
        // MENU ITEMS
        Task<List<MenuItemDto>> GetMenusForUserAsync(string accountId, CancellationToken cancellationToken = default);


        // QUICK LINKS
        Task<List<QuickLinkDto>> GetQuickLinksForUserAsync(string userId, CancellationToken cancellationToken = default);

        Task<bool> CreateQuickLinkAsync(QuickLinkEditDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateQuickLinkAsync(QuickLinkEditDto model, CancellationToken cancellationToken = default);

        Task<bool> DeleteQuickLinkAsync(int id, CancellationToken cancellationToken = default);
    }
}
