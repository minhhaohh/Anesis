using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Procedures;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Procedure> _procedureRepo;

        public ProcedureService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _procedureRepo = _unitOfWork.GetRepository<Procedure>();
        }

        public async Task<IPagedList<ProcedureDto>> SearchAsync(
            ProcedureFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _procedureRepo.All(true)
                .WhereIf(x => x.Name.Contains(filter.Name), filter.Name.HasValue())
                .WhereIf(x => x.EcwSiteId > 0, filter.HasSiteId)
                .WhereIf(x => x.Status == ProcedureStatus.ACTIVE, filter.ActiveOnly);

            return await _mapper.ProjectTo<ProcedureDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(Procedure.Name))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetProceduresAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _procedureRepo.All(true)
               .WhereIf(x => x.Status == ProcedureStatus.ACTIVE, activeOnly)
               .OrderBy(x => x.Name)
               .ToDictionaryAsync(x => x.Id, x => x.Name,cancellationToken);
        }
    }
}
