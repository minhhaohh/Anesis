using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Filters;

namespace Anesis.ApiService.Services.Services
{
    public class ProposalService : IProposalService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PotentialProcedure> _potentialProcRepo;

        public ProposalService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _potentialProcRepo = _unitOfWork.GetRepository<PotentialProcedure>();
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

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(int id, ProposalEditDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ReviewAsync(int id, ProposalReviewDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetStatusAsync(int id, PotentialProcedureStatus status,
            string reason = null, CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            proposal.RequestStatus = status;
            proposal.UpdatedBy = "haotm";
            proposal.UpdatedDate = DateTime.Now;

            if (reason.HasValue())
            {
                proposal.Notes = (proposal.Notes ?? "") + $"\n{status.ToString()} Reason: {reason}";
            }

            _potentialProcRepo.Update(proposal);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ScheduleSurgeryAsync(int id, ProposalScheduleDto model,
            CancellationToken cancellationToken = default)
        {
            var proposal = await FindByIdAsync(id, cancellationToken);

            if (proposal == null)
            {
                return false;
            }

            model.ApplyChangesTo(proposal);

            _potentialProcRepo.Update(proposal);

            return await _unitOfWork.SaveChangesAsync() > 0;
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
