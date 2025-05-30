using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.SurgeryCases;
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
    public class SurgeryCaseService : ISurgeryCaseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeviceService _deviceService;
        private readonly IInvoiceService _invoiceService;
        private readonly IChangeLogService _changeLogService;
        private readonly IRepository<SurgeryCase> _surgeryCaseRepo;
        private readonly IRepository<SurgeryCaseCptCode> _caseCptCodeRepo;
        private readonly IRepository<SurgeryCaseCost> _caseCostRepo;

        public SurgeryCaseService(IMapper mapper, 
            IUnitOfWork unitOfWork,
            IDeviceService deviceService,
            IInvoiceService invoiceService,
            IChangeLogService changeLogService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _deviceService = deviceService;
            _invoiceService = invoiceService;
            _changeLogService = changeLogService;
            _surgeryCaseRepo = _unitOfWork.GetRepository<SurgeryCase>();
            _caseCptCodeRepo = _unitOfWork.GetRepository<SurgeryCaseCptCode>();
            _caseCostRepo = _unitOfWork.GetRepository<SurgeryCaseCost>();
        }

        public async Task<IPagedList<SurgeryCaseDto>> SearchAsync(
            SurgeryCaseFilterDto filter, CancellationToken cancellationToken = default)
        {
            var result = await _mapper.ProjectTo<SurgeryCaseDto>(Search(filter))
                .SortData(filter.Sidx, filter.Sord, nameof(SurgeryCase.SurgeryDate), SortOrder.Descending)
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);

            // Get list of surgery case ids
            var ids = result.Data.Select(x => x.Id).ToList();

            // Get a list of cpt codes of surgery cases
            var caseCptCodes = await _caseCptCodeRepo.All(true)
                .Include(x => x.BillingCode)
                .Where(x => ids.Contains(x.SurgeryCaseId))
                .ToListAsync(cancellationToken);
            var caseCptCodesDict = caseCptCodes
                .GroupBy(x => x.SurgeryCaseId)
                .ToDictionary(g => g.Key, g => g.Select(x => $"{x.BillingCode.Code} x{x.Quantity}").ToList());

            // Get a list of devices and costs of surgery cases
            var caseCosts = await _caseCostRepo.All(true)
                .Include(x => x.Device)
                .Where(x => ids.Contains(x.SurgeryCaseId))
                .ToListAsync(cancellationToken);
            var caseCostsDict = caseCosts
                .GroupBy(x => x.SurgeryCaseId)
                .ToDictionary(g => g.Key, g => g.Select(x => $"{x.Device.VendorName} - {x.Device.Name} x{x.Quantity}").ToList());

            foreach (var item in result.Data)
            {
                item.Codes = caseCptCodesDict.GetValueOrDefault(item.Id, new List<string>());
                item.Devices = caseCostsDict.GetValueOrDefault(item.Id, new List<string>());
            }

            return result;
        }

        public async Task<SurgeryCaseDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var surgeryCase = await _surgeryCaseRepo.All(true)
                .Include(x => x.Patient)
                .Include(x => x.PrimaryProvider)
                .Include(x => x.AttendingProvider)
                .Include(x => x.Location)
                .Include(x => x.EncounterType)
                .Include(x => x.Procedure)
                .Include(x => x.Insurance)
                .Include(x => x.ReferringProvider)
                .Include(x => x.Week)
                .Include(x => x.PurchaseInvoice)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return _mapper.Map<SurgeryCaseDto>(surgeryCase);
        }

        public async Task<bool> SetStatusAsync(CaseSetStatusDto model, CancellationToken cancellationToken = default)
        {
            var surgeryCase = await FindByIdAsync(model.Id, cancellationToken);

            if (surgeryCase == null)
            {
                return false;
            }

            model.ApplyChangesTo(surgeryCase);

            _surgeryCaseRepo.Update(surgeryCase);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(surgeryCase.Id, "SetStatus", model.Reason,
                null, surgeryCase, null, false, $"Set surgery case status = '{(SurgeryCaseStatus)model.Status}'", cancellationToken);

            return true;
        }

        public async Task<bool> LinkInvoiceAsync(CaseLinkInvoiceDto model, CancellationToken cancellationToken = default)
        {
            var surgeryCase = await FindByIdAsync(model.CaseId, cancellationToken);

            if (surgeryCase == null)
            {
                return false;
            }

            var invoice = await _invoiceService.GetByIdAsync(model.InvoiceId, false, cancellationToken);

            if (invoice == null)
            {
                return false;
            }

            var amount = invoice.IsBulk ? model.SeparateAmount : invoice.TotalAmount;

            surgeryCase.PurchaseInvoiceId = model.InvoiceId;
            surgeryCase.InvoiceAmount = amount;
            surgeryCase.UpdatedBy = "haotm";
            surgeryCase.UpdatedDate = DateTime.Now;

            _surgeryCaseRepo.Update(surgeryCase);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(surgeryCase.Id, "LinkInvoice",
                $"Link invoice #{invoice.InvoiceNo} with amount {amount.ToString("c2")} to surgery case #{surgeryCase.Id}", 
                false, null, cancellationToken);

            return true;
        }

        public async Task<bool> RemoveLinkedInvoiceAsync(int id, CancellationToken cancellationToken = default)
        {
            var surgeryCase = await FindByIdAsync(id, cancellationToken);

            if (surgeryCase == null)
            {
                return false;
            }

            if (surgeryCase.PurchaseInvoiceId == null)
            {
                return true;
            }

            var removedInvoice = await _invoiceService.GetByIdAsync(surgeryCase.PurchaseInvoiceId.Value, false, cancellationToken);

            surgeryCase.PurchaseInvoiceId = null;
            surgeryCase.InvoiceAmount = 0;
            surgeryCase.UpdatedBy = "haotm";
            surgeryCase.UpdatedDate = DateTime.Now;

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(surgeryCase.Id, "UnlinkInvoice",
                $"Remove linked invoice #{removedInvoice.InvoiceNo} from surgery case #{surgeryCase.Id}",
                false, null, cancellationToken);

            return true;
        }

        public async Task<List<ChangeLogDto>> GetChangeLogsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _changeLogService.GetChangeLogsAsync<SurgeryCase>(id, null, cancellationToken);
        }


        // CASE CPT CODES
        public async Task<IPagedList<CaseCptCodeDto>> SearchCaseCptCodesAsync(
            CaseCptCodeFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _caseCptCodeRepo.All(true)
                .Where(x => x.SurgeryCaseId == filter.CaseId);

            return await _mapper.ProjectTo<CaseCptCodeDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(CaseCptCodeDto.CptCode))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<bool> UpdateCaseCptCodeFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default)
        {
            var caseCptCode = await FindCaseCptCodeByIdAsync(model.Id, cancellationToken);

            if (caseCptCode == null)
            {
                return false;
            }

            switch (model.FieldName)
            {
                case nameof(SurgeryCaseCost.Quantity):
                    caseCptCode.Quantity = model.NewValue.ToInt();
                    break;
                default:
                    return false;
            }

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(caseCptCode.SurgeryCaseId, "UpdateCaseCptCode",
                $"Update CPT Code #{caseCptCode.BillingCodeId} from surgery case #{caseCptCode.SurgeryCaseId}: {model.FieldName} = {model.NewValue}",
                false, null, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteCaseCptCodeAsync(int id, CancellationToken cancellationToken = default)
        {
            var caseCptCode = await _caseCptCodeRepo.DeleteAsync(cancellationToken, id);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(caseCptCode.SurgeryCaseId, "DeleteCaseCptCode",
                $"Delete CPT Code #{caseCptCode.BillingCodeId} from surgery case #{caseCptCode.SurgeryCaseId}",
                false, null, cancellationToken);

            return true;
        }

        // CASE COSTS
        public async Task<IPagedList<CaseCostDto>> SearchCaseCostsAsync(
            CaseCostFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _caseCostRepo.All(true)
                .Where(x => x.SurgeryCaseId == filter.CaseId);

            return await _mapper.ProjectTo<CaseCostDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(CaseCostDto.DeviceName))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<bool> AddCaseCostAsync(CaseCostAddDto model, CancellationToken cancellationToken = default)
        {
            var existedCaseCost = await _caseCostRepo.All()
                .FirstOrDefaultAsync(x => x.SurgeryCaseId == model.CaseId && x.DeviceId == model.DeviceId, cancellationToken);

            if (existedCaseCost == null) 
            {
                var currentCost = await _deviceService.GetCurrentCostAsync(model.DeviceId.Value, cancellationToken);
                var caseCost = model.ToSurgeryCaseCost(currentCost);

                await _caseCostRepo.InsertAsync(caseCost, cancellationToken);
            }
            else
            {
                existedCaseCost.Quantity += model.Quantity.Value;
                existedCaseCost.UpdatedBy = "haotm";
                existedCaseCost.UpdatedDate = DateTime.Now;
            }

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(model.CaseId.Value, "AddCaseCost",
                $"Add device #{model.DeviceId} with quantity = {model.Quantity} to surgery case #{model.CaseId}",
                false, null, cancellationToken);

            return true;
        }

        public async Task<bool> UpdateCaseCostFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default)
        {
            var caseCost = await FindCaseCostByIdAsync(model.Id, cancellationToken);

            if (caseCost == null)
            {
                return false;
            }

            switch (model.FieldName)
            {
                case nameof(SurgeryCaseCost.Quantity):
                    caseCost.Quantity = model.NewValue.ToInt();
                    break;
                case nameof(SurgeryCaseCost.AppliedCost):
                    caseCost.AppliedCost = decimal.Parse(model.NewValue);
                    break;
                default:
                    return false;
            }

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(caseCost.SurgeryCaseId, "UpdateCaseCost",
                $"Update device #{caseCost.DeviceId} from surgery case #{caseCost.SurgeryCaseId}: {model.FieldName} = {model.NewValue}",
                false, null, cancellationToken);

            return true;
        }

        public async Task<bool> DeleteCaseCostAsync(int id, CancellationToken cancellationToken = default)
        {
            var caseCost = await _caseCostRepo.DeleteAsync(cancellationToken, id);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return false;
            }

            await _changeLogService.AddChangeLogAsync<SurgeryCase>(caseCost.SurgeryCaseId, "DeleteCaseCost",
                $"Delete device #{caseCost.DeviceId} from surgery case #{caseCost.SurgeryCaseId}",
                false, null, cancellationToken);

            return true;
        }

        // SUPPORT METHODS
        private IQueryable<SurgeryCase> Search(SurgeryCaseFilterDto filter)
        {
            return _surgeryCaseRepo.All(true)
                .WhereIf(x => x.SurgeryDate >= filter.FromDate, filter.FromDate != null)
                .WhereIf(x => x.SurgeryDate <= filter.ToDate, filter.ToDate != null)
                .WhereIf(x => x.SurgeryDate == filter.SurgeryDate, filter.SurgeryDate.HasValue)
                .WhereIf(x => x.Patient.EcwChartNo == filter.EcwChartNo, filter.EcwChartNo.HasValue())
                .WhereIf(x => x.Patient.FirstName.Contains(filter.FirstName), filter.FirstName.HasValue())
                .WhereIf(x => x.Patient.LastName.Contains(filter.LastName), filter.LastName.HasValue())
                .WhereIf(x => x.ProcedureId == filter.ProcedureId, filter.ProcedureId > 0)
                .WhereIf(x => x.LocationId == filter.LocationId, filter.LocationId > 0)
                .WhereIf(x => x.InsuranceId == filter.InsuranceId, filter.InsuranceId > 0)
                .WhereIf(x => x.PrimaryProviderId == filter.PrimaryProviderId, filter.PrimaryProviderId > 0)
                .WhereIf(x => x.CaseStatus == filter.CaseStatus, filter.CaseStatus.HasValue)
                .WhereIf(x => x.DeviceCosts.Any(), filter.HasDevices)
                .WhereIf(x => !x.DeviceCosts.Any(), filter.NoDevices)
                .WhereIf(x => x.PurchaseInvoiceId > 0, filter.LinkedInvoice)
                .WhereIf(x => !x.DeviceCosts.Any(), filter.NoDevices)
                .WhereIf(x => x.PurchaseInvoiceId == filter.PurchaseInvoiceId, filter.LinkedInvoice && filter.PurchaseInvoiceId > 0)
                .WhereIf(x => x.PurchaseInvoiceId == null || (filter.PurchaseInvoiceId > 0 && x.PurchaseInvoiceId == filter.PurchaseInvoiceId),
                    filter.NotLinkInvoice && filter.CaseIds.Count == 0)
                .WhereIf(x => x.PurchaseInvoiceId == null || (filter.PurchaseInvoiceId > 0 && x.PurchaseInvoiceId == filter.PurchaseInvoiceId) || filter.CaseIds.Contains(x.Id),
                    filter.NotLinkInvoice && filter.CaseIds.Count > 0)
                .WhereIf(x => x.PurchaseInvoiceId > 0 && x.PurchaseInvoice.InvoiceNo.Contains(filter.LinkedInvoiceNo), filter.LinkedInvoiceNo.HasValue())
                .WhereIf(x => x.DeviceCosts.Any(x => x.Device.VendorName.Contains(filter.VendorName)), filter.VendorName.HasValue())
                .WhereIf(x => filter.CaseIds.Contains(x.Id), filter.SelectedOnly)
                .OrderByDescending(x => x.SurgeryDate);
        }

        private async Task<SurgeryCase> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _surgeryCaseRepo.FindAsync(cancellationToken, id);
        }

        private async Task<SurgeryCaseCost> FindCaseCostByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _caseCostRepo.FindAsync(cancellationToken, id);
        }

        private async Task<SurgeryCaseCptCode> FindCaseCptCodeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _caseCptCodeRepo.FindAsync(cancellationToken, id);
        }
    }
}
