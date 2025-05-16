using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserInGroup> _userInGroupRepo;
        private readonly IRepository<GroupPermission> _groupPermissionRepo;

        public PermissionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userInGroupRepo = _unitOfWork.GetRepository<UserInGroup>();
            _groupPermissionRepo = _unitOfWork.GetRepository<GroupPermission>();
        }

        public async Task<bool> HasPermissionAsync(string accountId, int permUniqueId, CancellationToken cancellationToken = default)
        {
            return await _groupPermissionRepo.All(true)
                .Join(_userInGroupRepo.All(true),
                    gp => gp.UserGroupId,
                    ug => ug.UserGroupId,
                    (gp, ug) => new { gp.Permission.FlagValue, ug.AccountId, gp.UserGroup.IsActive })
                .AnyAsync(x => x.AccountId == accountId && x.IsActive && x.FlagValue == permUniqueId, cancellationToken);
        }

        public async Task<List<int>> GetPermissionsForUserAsync(string accountId, CancellationToken cancellationToken = default)
        {
            return await _groupPermissionRepo.All(true)
                .Join(_userInGroupRepo.All(true),
                    gp => gp.UserGroupId,
                    ug => ug.UserGroupId,
                    (gp, ug) => new { gp.Permission.FlagValue, ug.AccountId, gp.UserGroup.IsActive })
                .Where(x => x.AccountId == accountId && x.IsActive == true)
                .Select(x => x.FlagValue)
                .ToListAsync(cancellationToken);
        }
    }
}
