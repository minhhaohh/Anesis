namespace Anesis.Web.Data.Services
{
    public interface IApiService : IAuthService, ICustomerService,
        IDeviceService, IDropdownDataService, IInvoiceService,
        ILocationService, IMediaFileService, IMenuService, 
        IPatientService, IProcedureService,  IProposalService, 
        IProviderService, IReconciliationService, ISiteSettingService, 
        ISurgeryCaseService, ITimetableService
    {
    }
}
