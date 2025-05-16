using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Insurance> _insuranceRepo;

        public InsuranceService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _insuranceRepo = _unitOfWork.GetRepository<Insurance>();
        }

        public async Task<Dictionary<int, string>> GetInsurancesAsync(
            bool hasSiteId = false, CancellationToken cancellationToken = default)
        {
            return await _insuranceRepo.All(true)
                .WhereIf(x => x.EcwSiteId != null && x.EcwSiteId > 0, hasSiteId)
                .OrderBy(x => x.CompanyName)
                .ToDictionaryAsync(x => x.Id, x => x.CompanyName, cancellationToken);
        }
    }
}
