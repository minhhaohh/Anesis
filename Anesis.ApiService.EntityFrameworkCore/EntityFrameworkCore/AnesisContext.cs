using Anesis.ApiService.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore
{
    public class AnesisContext : DbContext
	{
		public AnesisContext(DbContextOptions<AnesisContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            //IdentityDataContext
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserInRoleMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());

            //AnesisContext
            modelBuilder.ApplyConfiguration(new AppModuleMap());

            modelBuilder.ApplyConfiguration(new BankTransactionMap());
            modelBuilder.ApplyConfiguration(new BatchTransactionMap());
            modelBuilder.ApplyConfiguration(new BillingCodeMap());

            modelBuilder.ApplyConfiguration(new ClosedPayrollMap());
            modelBuilder.ApplyConfiguration(new CreditTransactionMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new CustomMediaFileMap());

            modelBuilder.ApplyConfiguration(new DepositBatchItemMap());
            modelBuilder.ApplyConfiguration(new DepositBatchMap());
            modelBuilder.ApplyConfiguration(new DeviceAndSupplyMap());
            modelBuilder.ApplyConfiguration(new DeviceCostMap());

            modelBuilder.ApplyConfiguration(new EmployeeHistoryMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new EncounterTypeMap());

            modelBuilder.ApplyConfiguration(new GeneralCategoryMap());
            modelBuilder.ApplyConfiguration(new GeneralChangeLogMap());
            modelBuilder.ApplyConfiguration(new GenericInvoiceMap());
            modelBuilder.ApplyConfiguration(new GenericInvoiceItemMap());
            modelBuilder.ApplyConfiguration(new GroupPermissionMap());

            modelBuilder.ApplyConfiguration(new InsuranceMap());

            modelBuilder.ApplyConfiguration(new JobRoleMap());

            modelBuilder.ApplyConfiguration(new LocalityAllowedAmountMap());
            modelBuilder.ApplyConfiguration(new LocationMap());

            modelBuilder.ApplyConfiguration(new MenuItemMap());
            modelBuilder.ApplyConfiguration(new MenuTabMap());

            modelBuilder.ApplyConfiguration(new PatientMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new PotentialProcedureMap());
            modelBuilder.ApplyConfiguration(new PreAuthInsuranceGroupMap());
            modelBuilder.ApplyConfiguration(new ProcedureMap());
            modelBuilder.ApplyConfiguration(new ProviderMap());

            modelBuilder.ApplyConfiguration(new ReconciliationMap());
            modelBuilder.ApplyConfiguration(new ReferringProviderMap());
            modelBuilder.ApplyConfiguration(new RiskLevelMap());
            modelBuilder.ApplyConfiguration(new RvuConfigMap());

            modelBuilder.ApplyConfiguration(new SignatureMap());
            modelBuilder.ApplyConfiguration(new SiteSettingMap());
            modelBuilder.ApplyConfiguration(new SurgeryCaseCostMap());
            modelBuilder.ApplyConfiguration(new SurgeryCaseCptCodeMap());
            modelBuilder.ApplyConfiguration(new SurgeryCaseMap());

            modelBuilder.ApplyConfiguration(new TimeClockHistoryMap());
            modelBuilder.ApplyConfiguration(new TimeClockMap());
            modelBuilder.ApplyConfiguration(new TimetableMap());

            modelBuilder.ApplyConfiguration(new UserGroupMap());
            modelBuilder.ApplyConfiguration(new UserInGroupMap());
            modelBuilder.ApplyConfiguration(new UserQuickLinkMap());

            modelBuilder.ApplyConfiguration(new WeekMap());
        }
	}
}
