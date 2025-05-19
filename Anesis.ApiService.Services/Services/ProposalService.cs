using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
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
    public class ProposalService : IProposalService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PotentialProcedure> _potentialProcRepo;
        private readonly IPatientService _patientService;
        private readonly IChangeLogService _changeLogService;

        public ProposalService(IMapper mapper, 
            IUnitOfWork unitOfWork,
            IPatientService patientService,
            IChangeLogService changeLogService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _potentialProcRepo = _unitOfWork.GetRepository<PotentialProcedure>();
            _patientService = patientService;
            _changeLogService = changeLogService;
        }

        public async Task<IPagedList<ProposalDto>> SearchAsync(ProposalFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            return await _mapper.ProjectTo<ProposalDto>(Search(filter))
                .SortData(filter.Sidx, filter.Sord, nameof(PotentialProcedure.AppointmentDate))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<ProposalDto> GetByIdAsync(int id,
            CancellationToken cancellationToken = default)
        {
            var query = _potentialProcRepo.All(true)
                .Where(x => x.Id == id);

            return await _mapper.ProjectTo<ProposalDto>(query)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> CreateAsync(ProposalEditDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = model.ToPotentialProcedureModel();

            await _potentialProcRepo.InsertAsync(proposal, cancellationToken);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            var patient = await _patientService.GetByIdAsync(model.PatientId.Value, cancellationToken);

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, "Create", model.Notes,
                "*", proposal, null, false, $"Created new proposal for patient #{patient.EcwChartNo} {patient.FirstName} {patient.LastName}", cancellationToken);

            return true;
        }

        public async Task<bool> UpdateAsync(ProposalEditDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(model.Id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            var modifyFields = model.GetModifiedFields(proposal);

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, "Update", model.ReasonChange,
                modifyFields, proposal, null, false, $"Update proposal information", cancellationToken);

            return true;
        }

        public async Task<bool> ToggleFlagAsync(
            FlagToggleDto model, CancellationToken cancellationToken = default)
        {
            var flag = false;
            var proposal = await FindByIdAsync(model.Id, cancellationToken);

            switch (model.FieldName)
            {
                case nameof(PotentialProcedure.ChartNotePosted):
                    proposal.ChartNotePosted = flag = !proposal.ChartNotePosted;
                    break;
                default:
                    return false;
            }

            proposal.UpdatedBy = "haotm";
            proposal.UpdatedDate = DateTime.Now;

            _potentialProcRepo.Update(proposal);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, "ToggleFlag", null,
                null, proposal, null, false, $"Set '{model.FieldName}' = '{flag}'", cancellationToken);

            return true;
        }

        public async Task<bool> ReviewAsync(ProposalReviewDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(model.Id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            var modifyFields = model.GetModifiedFields(proposal);

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, "Review", model.ReviewerNotes,
                modifyFields, proposal, null, false, $"Review proposal", cancellationToken);

            return true;
        }

        public async Task<bool> ScheduleSurgeryAsync(ProposalScheduleSurgeryDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(model.Id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            var isRescheduled = proposal.SurgeonId > 0;
            var modifyFields = model.GetModifiedFields(proposal, isRescheduled);

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, isRescheduled ? "RescheduleSurgery" : "ScheduleSurgery", model.Notes,
                modifyFields, proposal, null, false, $"Schedule surgery case for proposal", cancellationToken);

            return true;
        }

        public async Task<bool> SetStatusAsync(ProposalSetStatusDto model, CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(model.Id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<PotentialProcedure>(proposal.Id, "SetStatus", model.Reason,
                null, proposal, null, false, $"Set proposal status = '{(PotentialProcedureStatus)model.Status}'", cancellationToken);

            return true;
        }

        public async Task<List<ChangeLogDto>> GetChangeLogsAsync(int id, CancellationToken cancellationToken = default)
        {
            var excludeFields = new string[] { nameof(PotentialProcedure.UpdatedBy), nameof(PotentialProcedure.UpdatedDate) };

            return await _changeLogService.GetChangeLogsAsync<PotentialProcedure>(id, excludeFields, cancellationToken);
        }

        // SUPPORT METHODS
        public IQueryable<PotentialProcedure> Search(ProposalFilterDto filter)
        {
            return _potentialProcRepo.All(true)
                .WhereIf(x => x.ProposerId == filter.ProposerId, filter.ProposerId > 0)
                .WhereIf(x => x.Patient.EcwChartNo == filter.EcwChartNo, filter.EcwChartNo.HasValue())
                .WhereIf(x => x.Patient.FirstName.Contains(filter.FirstName), filter.FirstName.HasValue())
                .WhereIf(x => x.Patient.LastName.Contains(filter.LastName), filter.FirstName.HasValue())
                .WhereIf(x => x.AppointmentDate == filter.AppointmentDate, filter.AppointmentDate != null)
                .WhereIf(x => x.ProviderId == filter.ProviderId, filter.ProviderId > 0)
                .WhereIf(x => x.LocationId == filter.LocationId, filter.LocationId > 0)
                .WhereIf(x => x.ProcedureId == filter.ProcedureId, filter.ProcedureId > 0)
                .WhereIf(x => x.ReviewerId == filter.ReviewerId, filter.ReviewerId > 0)
                .WhereIf(x => x.SurgeryDate == filter.SurgeryDate, filter.SurgeryDate != null)
                .WhereIf(x => x.SurgeonId == filter.SurgeonId, filter.SurgeonId > 0)
                .WhereIf(x => x.SurgeryLocationId == filter.SurgeryLocationId, filter.SurgeryLocationId > 0)
                .WhereIf(x => !x.ChartNotePosted, filter.UnpostedChartNote)
                .WhereIf(x => x.RequestStatus == filter.RequestStatus, filter.RequestStatus != null)
                .WhereIf(x => x.RequestStatus != PotentialProcedureStatus.Cancelled, filter.ExcludeCancelled);
        }

        private async Task<PotentialProcedure> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _potentialProcRepo.FindAsync(cancellationToken, id);
        }
    }
}
