using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Anesis.ApiService.Services.Services
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<GeneralChangeLog> _changeLogRepo;

        public ChangeLogService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _changeLogRepo = _unitOfWork.GetRepository<GeneralChangeLog>();
        }

        public async Task<List<ChangeLogDto>> GetChangeLogsAsync<T>(int entityId,
            string[] excludeFields = null, CancellationToken cancellationToken = default)
        {
            var changeLogsDto = new List<ChangeLogDto>();
            var changeLogs = await GetChangeLogsAsync<T>(entityId, cancellationToken);

            for (var i = 0; i < changeLogs.Count(); i++)
            {
                var currentChangeLog = changeLogs[i];
                var beforeChangeLog = i == changeLogs.Count() - 1 ? null : changeLogs[i + 1];

                var changedFields = currentChangeLog.ChangedFields == null || currentChangeLog.ChangedFields == "*" 
                    ? null : currentChangeLog.ChangedFields.Split(",");

                var afterChange = JsonConvert.DeserializeObject<T>(currentChangeLog.AfterChange);
                var beforeChange = JsonConvert.DeserializeObject<T>(beforeChangeLog?.AfterChange ?? "");

                var changedFieldsDto = GetChangedFields<T>(changedFields, excludeFields, afterChange, beforeChange);

                var logDto = _mapper.Map<ChangeLogDto>(currentChangeLog);
                logDto.ChangedFields = changedFieldsDto;

                changeLogsDto.Add(logDto);
            }

            return changeLogsDto;
        }

        public async Task<bool> AddChangeLogAsync<T>(
            int entityId, string actionName, string userComment,
            bool adminOnly = false, string notes = null,
            CancellationToken cancellationToken = default)
        {
            await _changeLogRepo.InsertAsync(new GeneralChangeLog()
            {
                EntityType = typeof(T).Name,
                EntityId = entityId,
                ActionName = actionName,
                ActionTime = DateTime.Now,
                ChangedBy = "haotm",
                UserComment = userComment,
                ChangedFields = null,
                BeforeChange = null,
                AfterChange = "",
                AdminOnly = adminOnly,
                Notes = notes
            }, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> AddChangeLogAsync<T>(
            int entityId, string actionName, string userComment,
            string changedFields, T afterChange, T beforeChange,
            bool adminOnly = false, string notes = null,
            CancellationToken cancellationToken = default)
        {
            await _changeLogRepo.InsertAsync(new GeneralChangeLog()
            {
                EntityType = nameof(T),
                EntityId = entityId,
                ActionName = actionName,
                ActionTime = DateTime.Now,
                ChangedBy = "haotm",
                UserComment = userComment,
                ChangedFields = changedFields,
                BeforeChange = JsonConvert.SerializeObject(beforeChange),
                AfterChange = afterChange != null ? JsonConvert.SerializeObject(afterChange) : "",
                AdminOnly = adminOnly,
                Notes = notes
            }, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<int> DeleteChangeLogsAsync<T>(int entityId, CancellationToken cancellationToken = default)
        {
            var changeLogs = await _changeLogRepo.All()
                .Where(x => x.EntityType == typeof(T).Name && x.EntityId == entityId)
                .ToListAsync(cancellationToken);

            if (changeLogs.Count == 0)
            {
                return 0;
            }

            _changeLogRepo.DeleteRange(changeLogs);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteManyChangeLogsAsync<T>(List<int> entityIds, CancellationToken cancellationToken = default)
        {
            var changeLogs = await _changeLogRepo.All()
                .Where(x => x.EntityType == typeof(T).Name && entityIds.Contains(x.EntityId))
                .ToListAsync(cancellationToken);

            if (changeLogs.Count == 0)
            {
                return 0;
            }

            _changeLogRepo.DeleteRange(changeLogs);
            return await _unitOfWork.SaveChangesAsync(cancellationToken);
        }


        // SUPPORT METHODS
        private async Task<List<GeneralChangeLog>> GetChangeLogsAsync<T>(int entityId,
            CancellationToken cancellationToken = default)
        {
            return await _changeLogRepo.All(true)
                .Where(x => x.EntityType == typeof(T).Name && x.EntityId == entityId)
                .OrderByDescending(x => x.ActionTime)
                .ToListAsync(cancellationToken);
        }

        private List<ChangedFieldDto> GetChangedFields<T>(
            string[] changedFields, string[] excludeFields,
            T afterChange, T beforeChange)
        {
            var changedFieldsDto = new List<ChangedFieldDto>();

            // Check changedFields, beforeChange, afterChanged columns have value
            if (changedFields == null || changedFields.Count() == 0
                || afterChange == null || beforeChange == null)
            {
                return changedFieldsDto;
            }

            excludeFields ??= Array.Empty<string>();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!changedFields.Contains(prop.Name)
                    || excludeFields.Contains(prop.Name))
                {
                    continue;
                }

                changedFieldsDto.Add(new ChangedFieldDto()
                {
                    FieldName = prop.Name,
                    BeforeChange = prop.GetValue(beforeChange)?.ToString(),
                    AfterChange = prop.GetValue(afterChange)?.ToString()
                });
            }

            return changedFieldsDto;
        }
    }
}
