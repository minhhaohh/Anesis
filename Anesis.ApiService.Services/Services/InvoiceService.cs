using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.GenericInvoices;
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
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChangeLogService _changeLogService;
        private readonly IRepository<GenericInvoice> _invoiceRepo;
        private readonly IRepository<GenericInvoiceItem> _invoiceItemRepo;
        private readonly IRepository<SurgeryCase> _surgeryCaseRepo;

        public InvoiceService(IMapper mapper,
            IUnitOfWork unitOfWork,
            IChangeLogService changeLogService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _changeLogService = changeLogService;
            _invoiceRepo = _unitOfWork.GetRepository<GenericInvoice>();
            _invoiceItemRepo = _unitOfWork.GetRepository<GenericInvoiceItem>();
            _surgeryCaseRepo = _unitOfWork.GetRepository<SurgeryCase>();
        }

        public async Task<IPagedList<InvoiceDto>> SearchAsync(InvoiceFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            var query = Search(filter);

            return await _mapper.ProjectTo<InvoiceDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(InvoiceDto.InvoiceNo))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<List<InvoiceDto>> GetAvailableInvoicesToLinkToCaseAsync(
            int? currentId, int? locationId, CancellationToken cancellationToken = default)
        {
            var linkedInvoiceIds = _surgeryCaseRepo.All(true)
                .Where(x => x.PurchaseInvoiceId > 0)
                .Select(x => x.PurchaseInvoiceId)
                .Distinct()
                .ToList();

            var query = _invoiceRepo.All(true)
                .Where(x => x.IsBulk
                    || !linkedInvoiceIds.Contains(x.Id)
                    || (currentId > 0 && currentId == x.Id))
                .WhereIf(x => x.LocationId == locationId, locationId > 0);

            return await _mapper.ProjectTo<InvoiceDto>(query)
                .OrderBy(x => x.InvoiceNo)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<string>> GetAllOwnersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _invoiceRepo.All(true)
                .Select(x => x.CreatedBy)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);
        }

        public async Task<InvoiceDto> GetByIdAsync(int id,
            bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var query = _invoiceRepo.All(true)
                .Where(x => x.Id == id);

            var invoice = await _mapper.ProjectTo<InvoiceDto>(query)
                .FirstOrDefaultAsync(cancellationToken);

            if (invoice == null)
            {
                return null;
            }

            if (includeDetails)
            {
                var invoiceItems = await _invoiceItemRepo.All(true)
                    .Where(x => x.InvoiceId == id)
                    .ToListAsync(cancellationToken);

                invoice.Items = _mapper.Map<List<InvoiceItemDto>>(invoiceItems);
            }

            return invoice;
        }

        public async Task<List<ChangeLogDto>> GetChangeLogsAsync(int id,
            CancellationToken cancellationToken = default)
        {
            var excludeFields = new string[] { nameof(GenericInvoice.UpdatedBy), nameof(GenericInvoice.UpdatedDate) };

            return await _changeLogService.GetChangeLogsAsync<GenericInvoice>(id, excludeFields, cancellationToken);
        }

        // SUPPORT METHODS 
        private IQueryable<GenericInvoice> Search(InvoiceFilterDto filter)
        {
            return _invoiceRepo.All(true)
                .WhereIf(x => x.InvoiceDate >= filter.FromDate, filter.FromDate.HasValue)
                .WhereIf(x => x.InvoiceDate <= filter.ToDate, filter.ToDate.HasValue)
                .WhereIf(x => x.InvoiceNo.Contains(filter.InvoiceNo), filter.InvoiceNo.HasValue())
                .WhereIf(x => x.VendorId == filter.VendorId, filter.VendorId > 0)
                .WhereIf(x => x.Company == filter.Company, filter.Company.HasValue())
                .WhereIf(x => x.LocationId == filter.LocationId, filter.LocationId > 0)
                .WhereIf(x => x.PaymentMethod == filter.PaymentMethod, filter.PaymentMethod.HasValue)
                .WhereIf(x => x.InvoiceStatus == filter.InvoiceStatus, filter.InvoiceStatus.HasValue)
                .WhereIf(x => x.ReviewerId == filter.ReviewerId, filter.ReviewerId.HasValue())
                .WhereIf(x => x.CreatedBy == filter.CreatedBy, filter.CreatedBy.HasValue())
                .WhereIf(x => x.InvoiceStatus < InvoiceStatus.Paid, filter.UnpaidInvoicesOnly)
                .WhereIf(x => x.IsBulk, filter.BulkInvoicesOnly);
        }

        private async Task<GenericInvoice> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _invoiceRepo.FindAsync(cancellationToken, id);
        }
    }
}
