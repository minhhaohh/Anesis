using Anesis.ApiService.Domain.DTOs.Patients;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;

namespace Anesis.ApiService.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepo;

        public PatientService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _patientRepo = _unitOfWork.GetRepository<Patient>();
        }

        public async Task<PatientDto> GetByIdAsync(int id, CancellationToken cancellationToken = default) 
        {
            var patient = await FindByIdAsync(id, cancellationToken);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> GetByChartNoAsync(string chartNo, CancellationToken cancellationToken = default)
        {
            var patient = await FindByChartNoAsync(chartNo, cancellationToken);
            return _mapper.Map<PatientDto>(patient);
        }

        // SUPPORT METHODS
        public async Task<Patient> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _patientRepo.FindAsync(cancellationToken, id);
        }

        public async Task<Patient> FindByChartNoAsync(string chartNo, CancellationToken cancellationToken = default)
        {
            return await _patientRepo.GetFirstAsync(x => x.EcwChartNo == chartNo, true, cancellationToken);
        }
    }
}
