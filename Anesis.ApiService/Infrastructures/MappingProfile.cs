using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using Anesis.ApiService.Domain.DTOs.CreditTransactions;
using Anesis.ApiService.Domain.DTOs.Customers;
using Anesis.ApiService.Domain.DTOs.Deposits;
using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.GenericInvoices;
using Anesis.ApiService.Domain.DTOs.Locations;
using Anesis.ApiService.Domain.DTOs.MediaFiles;
using Anesis.ApiService.Domain.DTOs.Menus;
using Anesis.ApiService.Domain.DTOs.Patients;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Domain.DTOs.Reconciliations;
using Anesis.ApiService.Domain.DTOs.SurgeryCases;
using Anesis.ApiService.Domain.Entities;
using AutoMapper;

namespace Anesis.ApiService.Infrastructures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateCustomerMap();
            CreateChangeLogMap();
            CreateDeviceMap();
            CreateInvoiceMap();
            CreateLocationMap();
            CreateMediaFileMap();
            CreateMenuMap();
            CreatePatientMap();
            CreateProposalMap();
            CreateReconciliationMap();
            CreateSurgeryCaseMap();
            CreateTimetableMap();
        }

        public void CreateCustomerMap()
        {
            CreateMap<Customer, CustomerDto>();
        }

        public void CreateChangeLogMap()
        {
            CreateMap<GeneralChangeLog, ChangeLogDto>()
                .ForMember(x => x.ChangedFields, options => options.Ignore());
        }

        public void CreateDeviceMap()
        {
            CreateMap<DeviceAndSupply, DeviceWithCostDto>();
            CreateMap<DeviceWithCostDto, DeviceAndSupply>();
            CreateMap<DeviceWithCostEditDto, DeviceAndSupply>();

            CreateMap<DeviceCost, DeviceCostDto>();
        }

        public void CreateInvoiceMap()
        {
            CreateMap<GenericInvoice, InvoiceDto>()
                .ForMember(x => x.VendorName, options => options.MapFrom(x => x.Vendor.CustomerName))
                .ForMember(x => x.VendorDescription, options => options.MapFrom(x => x.Vendor.Description))
                .ForMember(x => x.LocationName, options => options.MapFrom(x => x.Location.ShortName))
                .ForMember(x => x.ItemsCount, options => options.MapFrom(x => x.Items.Count))
                .ForMember(x => x.Items, options => options.Ignore());

            CreateMap<GenericInvoiceItem, InvoiceItemDto>();
        }

        public void CreateLocationMap()
        {
            CreateMap<Location, LocationDto>()
                .ForMember(x => x.CountyName, options => options.MapFrom(x => x.County.DisplayName));
        }

        public void CreateMediaFileMap()
        {
            CreateMap<CustomMediaFile, MediaFileDto>()
                .ForMember(x => x.Signed, options => options.MapFrom(x => x.Signatures != null && x.Signatures.Count > 0));
        }

        public void CreateMenuMap()
        {
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(x => x.TabName, options => options.MapFrom(x => x.MenuTab.TabName))
                .ForMember(x => x.TabDisplayOrder, options => options.MapFrom(x => x.MenuTab.DisplayOrder))
                .ForMember(x => x.TabIconPath, options => options.MapFrom(x => x.MenuTab.NewIconPath))
                .ForMember(x => x.LinkUrl, options => options.MapFrom(x => x.NewLinkUrl))
                .ForMember(x => x.IconPath, options => options.MapFrom(x => x.NewIconPath));

            CreateMap<UserQuickLink, QuickLinkDto>()
                .ForMember(x => x.MenuText, options => options.MapFrom(x => x.MenuItem.MenuText))
                .ForMember(x => x.LinkUrl, options => options.MapFrom(x => x.MenuItem.NewLinkUrl))
                .ForMember(x => x.Tooltip, options => options.MapFrom(x => x.MenuItem.Tooltip))
                .ForMember(x => x.IconPath, options => options.MapFrom(x => x.MenuItem.NewIconPath))
                .ForMember(x => x.CssClass, options => options.MapFrom(x => x.MenuItem.CssClass));
        }

        public void CreatePatientMap()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(x => x.RiskLevelName, options => options.MapFrom(x => x.RiskLevel.LevelName));
        }

        public void CreateProposalMap()
        {
            CreateMap<PotentialProcedure, ProposalDto>()
               .ForMember(x => x.ProposerName, options => options.MapFrom(x => x.Proposer.ProviderName))
               .ForMember(x => x.ChartNo, options => options.MapFrom(x => x.Patient.ChartNo))
               .ForMember(x => x.EcwChartNo, options => options.MapFrom(x => x.Patient.EcwChartNo))
               .ForMember(x => x.FirstName, options => options.MapFrom(x => x.Patient.FirstName))
               .ForMember(x => x.LastName, options => options.MapFrom(x => x.Patient.LastName))
               .ForMember(x => x.Gender, options => options.MapFrom(x => x.Patient.Gender))
               .ForMember(x => x.Dob, options => options.MapFrom(x => x.Patient.Dob))
               .ForMember(x => x.ProviderName, options => options.MapFrom(x => x.ApptProvider.ProviderName))
               .ForMember(x => x.LocationName, options => options.MapFrom(x => x.ApptLocation.ShortName))
               .ForMember(x => x.ProcedureName, options => options.MapFrom(x => x.Procedure.Name))
               .ForMember(x => x.ReviewerName, options => options.MapFrom(x => x.Reviewer.ProviderName))
               .ForMember(x => x.SurgeonName, options => options.MapFrom(x => x.Surgeon.ProviderName))
               .ForMember(x => x.SurgeryLocationName, options => options.MapFrom(x => x.SurgeryLocation.ShortName));

            CreateMap<PotentialProcedure, ProposalReviewDto>()
                .ForMember(x => x.IsApproved, options => options.Ignore());

            CreateMap<PotentialProcedure, ProposalScheduleSurgeryDto>();
        }

        public void CreateReconciliationMap()
        {
            // Reconciliation
            CreateMap<Reconciliation, ReconciliationDto>();

            // BankTransaction
            CreateMap<BankTransaction, BankTransactionDto>();

            // BatchTransaction
            CreateMap<BatchTransaction, BatchTransactionDto>();

            // CreditTransaction
            CreateMap<CreditTransaction, CreditTransactionDto>();

            // Deposit
            CreateMap<DepositBatch, DepositDto>();
            CreateMap<DepositBatchItem, DepositItemDto>();
        }

        public void CreateSurgeryCaseMap()
        {
            CreateMap<SurgeryCase, SurgeryCaseDto>()
                .ForMember(x => x.ChartNo, options => options.MapFrom(x => x.Patient.ChartNo))
                .ForMember(x => x.EcwChartNo, options => options.MapFrom(x => x.Patient.EcwChartNo))
                .ForMember(x => x.FirstName, options => options.MapFrom(x => x.Patient.FirstName))
                .ForMember(x => x.LastName, options => options.MapFrom(x => x.Patient.LastName))
                .ForMember(x => x.Gender, options => options.MapFrom(x => x.Patient.Gender))
                .ForMember(x => x.Dob, options => options.MapFrom(x => x.Patient.Dob))
                .ForMember(x => x.PrimaryProviderName, options => options.MapFrom(x => x.PrimaryProvider.ProviderName))
                .ForMember(x => x.AttendingProviderName, options => options.MapFrom(x => x.AttendingProvider.ProviderName))
                .ForMember(x => x.ReferringProviderName, options => options.MapFrom(x => x.ReferringProvider.ProviderName))
                .ForMember(x => x.ProcedureName, options => options.MapFrom(x => x.Procedure.Name))
                .ForMember(x => x.InsuranceName, options => options.MapFrom(x => x.Insurance.CompanyName))
                .ForMember(x => x.LocationName, options => options.MapFrom(x => x.Location.ShortName))
                .ForMember(x => x.EncounterTypeName, options => options.MapFrom(x => x.EncounterType.Name))
                .ForMember(x => x.WeekNumber, options => options.MapFrom(x => x.Week.WeekNumber))
                .ForMember(x => x.InvoiceNumber, options => options.MapFrom(x => x.PurchaseInvoice.InvoiceNo))
                .ForMember(x => x.IsBulkInvoice, options => options.MapFrom(x => x.PurchaseInvoice.IsBulk));

            CreateMap<SurgeryCaseCost, CaseCostDto>()
                .ForMember(x => x.DeviceName, options => options.MapFrom(x => x.Device.Name))
                .ForMember(x => x.VendorName, options => options.MapFrom(x => x.Device.VendorName))
                .ForMember(x => x.VendorCost, options => options.MapFrom(x => x.DeviceCost.AppliedCost))
                .ForMember(x => x.EffectiveDate, options => options.MapFrom(x => x.DeviceCost.EffectiveDate));

            CreateMap<SurgeryCaseCptCode, CaseCptCodeDto>()
                .ForMember(x => x.CptCode, options => options.MapFrom(x => x.BillingCode.Code))
                .ForMember(x => x.WorkRvu, options => options.MapFrom(x => x.RvuConfig.WorkRvu))
                .ForMember(x => x.RvuEffectiveDate, options => options.MapFrom(x => x.RvuConfig.EffectiveDate))
                .ForMember(x => x.ConversionFactor, options => options.MapFrom(x => x.RvuConfig.ConversionFactor))
                .ForMember(x => x.CmsAllowedAmt, options => options.MapFrom(x => x.AllowedAmount.AllowedAmount));
        }

        public void CreateTimetableMap()
        {

        }
    }
}
