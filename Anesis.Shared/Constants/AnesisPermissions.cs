using System.ComponentModel;

namespace Anesis.Shared.Constants
{
    public static class AnesisPermissions
    {
        [Description("Managing system users, roles, user groups and permissions")]
        public enum SystemModule
        {
            [Description("View list of user accounts")]
            ViewUsers = 1000,

            [Description("Create or update user account")]
            EditAccount,

            [Description("View list of system roles")]
            ViewRoles,

            [Description("Add or update a role")]
            EditRole,

            [Description("View list of user in given role")]
            ViewUsersInRole,

            [Description("Add or remove a user from given role")]
            AddRemoveUsersInRole,

            [Description("View list of group of users")]
            ViewGroups,

            [Description("Add or update a group of users")]
            EditGroup,

            [Description("View and grant group permissions")]
            GrantGroupPermissions,

            [Description("View list of users in a given group")]
            ViewUsersInGroup,

            [Description("Add a user into group or remove user from group")]
            AddRemoveUsersInGroup,

            [Description("View list of system modules")]
            ViewModules,

            [Description("Add or edit a module information")]
            EditModule,

            [Description("View list of system permissions")]
            ViewPermissions,

            [Description("Add or update a given permission")]
            EditPermission,

            [Description("Add permission to group of users or remove permission from group")]
            AssignPermission,

            [Description("View list of users who are granted to permission")]
            ViewUsersHasPermission,

            [Description("View list of activity logs")]
            ViewActivityLogs,

            [Description("Delete activity log")]
            DeleteActivityLog,

            [Description("Allow a user to temporarily sign in as a different user")]
            Impersonate,

            [Description("Manage generic or custom attributes")]
            ManageCustomAttributes,
        }


        [Description("Managing menu tabs and menu items and quick links access")]
        public enum MenuModule
        {
            [Description("View list of menu tabs")]
            ViewMenuTabs = 2000,

            [Description("Add or update a menu tab")]
            EditMenuTab,

            [Description("View list of menu items")]
            ViewMenuItems,

            [Description("Add or update a menu item")]
            EditMenuItem
        }

        [Description("Managing softwares/programs which are used for Anesis and Zoey Lab")]
        public enum SoftwareModule
        {
            [Description("View list of software/programs")]
            ViewList = 3000,

            [Description("Add new software or update existing information")]
            UpdateSoftware,

            [Description("View list of versions/releases of a given software")]
            ViewReleases,

            [Description("Add new version or update release information")]
            UpdateRelease,

            [Description("View list of settings/options of a given software")]
            ViewOptions,

            [Description("Add or update a software option")]
            UpdateOption,

            [Description("View actual values that the user set for software options")]
            ViewUserPreferences,

            [Description("Set value of software option / user preference")]
            UpdateUserPreference,
        }


        [Description("Reconcile batch transactions and bank transactions to ensure figures are correct and in agreement")]
        public enum ReconciliationModule
        {
            [Description("View list of reconciliations")]
            ViewList = 4000,

            [Description("View all batch and bank transactions in a given reconciliation")]
            ViewDetail,

            [Description("Add adjustment batch transactions")]
            AddAdjustment,

            [Description("Review reconciled results of a past month")]
            Review,

            [Description("Edit or update reconciled results of a past month")]
            ReconcilePastMonth,

            [Description("Delete a given reconciliation and set reconciliation id on batch/bank transactions to NULL")]
            UndoReconciliation
        }

        [Description("Managing employee's information")]
        public enum EmployeeModule
        {
            [Description("Employees : View list of employees")]
            ViewList = 5000,

            [Description("Add new employee or update employee information")]
            EditInfo,

            [Description("Re-hire an employee or mark an employee as terminated or leave")]
            ChangeTermStatus,

            [Description("Can view employee salary")]
            ViewSalary,

            [Description("Can close employee salary")]
            CloseSalary,

            [Description("Accrual PTO Rate : View employee accrual rate")]
            ViewAccrualRate,

            [Description("Update or create new accrual rate for an employee")]
            EditAccrualRate,

            [Description("Can view list of employees in the same company only")]
            ViewStaffSameCompanyOnly,

            [Description("View list of job roles/positions")]
            ViewJobRoles,

            [Description("Add or update job role/position")]
            EditJobRole,

            [Description("View list of certificates or documents for each job position")]
            ViewCertificates,

            [Description("Add or update certificate/document information")]
            EditCertificate,

            [Description("Add or update certificate information required for each employee")]
            ManageEmployeeCertificates
        }

        [Description("Managing Regular / Holiday / PTO / Unpaid hours and schedules")]
        public enum TimeClockModule
        {
            [Description("Clock In/Out : Can clock in / clock out, used for hourly employees")]
            ClockInOut = 6000,

            [Description("Review Your Hours : View personal hours")]
            ViewMyHours,

            [Description("Can view hours of all employees")]
            CanViewAllEmployees,

            [Description("Can view hours of all employees in same company that is managed by current user")]
            CanViewCompanyEmployees,

            [Description("Can view hours of subordinate employees who are managed by current user")]
            CanViewSubordinates,

            [Description("Manual Clock Entry : Add or edit a clock entry manually")]
            EditClockEntry,

            [Description("Allow add/edit clock entry manually with negative total hours")]
            SetNegativeHours,

            [Description("Soft delete clock entry (Mark status as Deleted)")]
            DeleteClock,

            [Description("Self delete permanently pending PTO requests")]
            DeletePendingPto,

            [Description("Holiday Hours : Add holiday hours for employees")]
            AddHolidayHours,

            [Description("PTO Accrual : Accure PTO hours for half month")]
            AccruePto,

            [Description("Schedules : View PTO hours on Calendar")]
            ViewSchedules,

            [Description("PTO Summary & Request: Request PTO/Unpaid hours")]
            MakePtoRequest,

            [Description("Can make PTO requests for other employees")]
            CanRequestPtoForOthers,

            [Description("PTO Review: Can review/approve PTO requests")]
            ReviewPtoRequest,

            [Description("Can review and approve/deny manual regular clock")]
            ReviewRegularClock,

            [Description("View accrued PTO hours and do the carry over")]
            CarryOverPto,

            [Description("Manage global calendar events")]
            ManageCalendarEvents,

            [Description("View schedules of a specific staff (used for employees)")]
            ViewStaffSchedules,

            [Description("View schedules of all staff (used for managers)")]
            ViewAllSchedules,

            [Description("Can create/update/delete staff schedules")]
            ScheduleStaff,
        }

        [Description("Managing PTO reports")]
        public enum ClockReportModule
        {
            [Description("PTO Report : View PTO report")]
            ViewPtoReport = 7000,

            [Description("Can export PTO report to PDF file")]
            ExportPtoReport,

            [Description("Can unapproved PTO requests")]
            UnapprovedPto,

            [Description("Daily Clock Report: View daily clock hours")]
            ViewDailyClock,

            [Description("PTO Termination Report: View PTO Termination report")]
            ViewPtoTermination
        }


        [Description("Managing payrolls")]
        public enum PayrollModule
        {
            [Description("Closed Payroll Report : View closed payrolls report")]
            ViewClosedPayrolls = 8000,

            [Description("View closed payroll detail")]
            ViewPayrollDetail,

            [Description("Re-open a closed payroll")]
            ReopenPayroll,

            [Description("Close Payroll Period : View current payroll hours")]
            ViewCurrentPayroll,

            [Description("Close current payroll period")]
            ClosePayroll,

            //[Description("Can close hours for all employees (PTOAdmin)")]
            //CanCloseForAllStaffs
        }

        [Description("Managing patients, prescription results and PMP search criteria")]
        public enum PatientModule
        {
            [Description("View list of patients")]
            ViewPatients = 9000,

            [Description("Update patient information")]
            UpdatePatient,

            [Description("Create or update PMP search criteria")]
            EditCriteria,

            [Description("View prescription search results")]
            ViewPmpResults,

            [Description("Confirm search candidates which are returned after first phase of PMP searching process")]
            ConfirmCandidates
        }

        [Description("Managing PMP drugs, drug cross referrences, drug acronyms, costs and devices")]
        public enum DeviceSupplyModule
        {
            [Description("View list of drugs which were crawled by PMP program")]
            ViewDrugList = 10000,

            [Description("Assign test classes to drug or mark drug as Not affected")]
            AssignClass,

            [Description("PMP Drug Abbreviations : View list of drug acronyms and abbreviations")]
            ViewAcronyms,

            [Description("Add or update drug acronyms/abbreviations")]
            EditAcronym,

            [Description("Drug Cross References : View list of drug brands (cross referrences)")]
            ViewDrugBrands,

            [Description("Add or update drug brand information")]
            EditDrugBrand,

            [Description("View list of devices and cost")]
            ViewDevices,

            [Description("View history changes on device's price")]
            ViewDevicePriceHistory,

            [Description("Update information of device and cost")]
            UpdateDevice
        }


        [Description("Managing providers/doctors")]
        public enum ProviderModule
        {
            [Description("View list of providers")]
            ViewList = 11000,

            [Description("Update provider information")]
            EditInfo,

            [Description("View Anesis provider matrix (Credentialing screen)")]
            ViewProviderMatrix,

            [Description("Can start/end credential of a provider with an insurance")]
            UpdateProviderMatrix,

            [Description("Change the visibility of providers and insurances on Credentialing screen")]
            ChangeCredentialingSettings
        }


        [Description("Managing fax numbers and senders")]
        public enum FaxesModule
        {
            [Description("View list of fax numbers")]
            ViewFaxNumbers = 12000,

            [Description("Add, edit information regarding to fax number such as default type, notes")]
            EditFaxNumber,

            [Description("View list of fax senders")]
            ViewSenders,

            [Description("Add a new fax sender or update sender information")]
            UpdateSender
        }

        [Description("Managing billing codes, deposit batch and automated EFT deposit")]
        public enum BillingModule
        {
            [Description("View list of billing codes")]
            ViewCodes = 13000,

            [Description("Add or update billing code")]
            EditCode,

            [Description("View list of deposit batch and batch items")]
            ViewDepositBatch,

            [Description("Add, edit, close, complete deposit batch")]
            EditDepositBatch,

            [Description("Re-open a closed deposit batch")]
            ReopenDepositBatch,

            [Description("Automated EFT : View list of automated EFT deposit")]
            ViewEftDeposit,

            [Description("Create new EFT deposit batch")]
            EditEftDeposit,

            [Description("View list of medical records")]
            ViewMedRec,

            [Description("Create new or update medical record")]
            EditMedRec,

            [Description("Re-open a closed medical record")]
            ReopenMedRec,

            [Description("View billing lab orders (Can access LabBillingApp)")]
            AccessLabBillingApp,

            [Description("Mark a lab order as BILLED or NOBILL (VOID)")]
            BillOrVoidOrder,

            [Description("Undo BILLED or VOID action on a lab order")]
            UndoBilledOrder,

            [Description("View list of mismatched billing charges")]
            ViewMismatchedCharges,

            [Description("View RVU configs")]
            ViewRvuConfigs,

            [Description("Update RVU value")]
            UpdateRvuValue,

            [Description("View allowed amount by code by insurance by year")]
            ViewAllowedAmounts,

            [Description("Update allowed amount by code by insurance by year")]
            UpdateAllowedAmount,

            [Description("View list of vendor invoices")]
            ViewVendorInvoices,

            [Description("Add or edit vendor invoices")]
            CreateVendorInvoice,

            [Description("Approve vendor invoices")]
            ApproveVendorInvoice,

            [Description("Mark a vendor invoice as PAID")]
            PayVendorInvoices,

            [Description("Delete vendor invoices")]
            DeleteVendorInvoice,

            [Description("Edit any vendor invoices")]
            EditAnyVendorInvoice
        }

        [Description("Managing appointment-related and revenue and forecast reports")]
        public enum ReportModule
        {
            [Description("View appointment summary trend in line chart")]
            ViewSummaryTrend = 14000,

            [Description("Mid-Level Bonus : View monthly Mid-Level bonus")]
            ViewMidLevelBonus,

            [Description("Indicates whether user can view mid-level bonus of all providers")]
            CanViewDataOfAllProviders,

            [Description("Compute monthly Mid-Level bonus")]
            ComputeMidLevelBonus,

            [Description("View Revenue by Provider report")]
            ViewRevenueByProvider,

            [Description("Can export Revenue by Provider report to PDF or Excel")]
            CanExportRevenueReport,

            [Description("View list of analyzer lab results which were not reviewed by providers")]
            ViewUnreviewedAnalyzerLabs,

            [Description("View Reimbursement Report by CPT Code by Insurance")]
            ViewRbccReport,

            [Description("View Collected Amount by Month by CPT Code or Procedure")]
            ViewCacpReport,

            [Description("View Allowed Amounts By Charge Code By Insurance")]
            ViewAllowedAmounts,

            [Description("View Revenue by Provider report for Sound ASC")]
            ViewRevenueByProviderForAsc,

            [Description("ASC pro-fees bonus: View quarterly ASC pro-fees bonus")]
            ViewAscProFeeBonus,

            [Description("Compute quarterly ASC pro-fees bonus")]
            ComputeAscProFeeBonus,

            [Description("View and download RVU by Doctor by Month report")]
            ViewRvuReport,

            [Description("View and download Gross Margin by Doctor by Month report")]
            ViewGrossMarginReport,

            [Description("View list of custom reports, sheets, columns, params")]
            ViewCustomReports,

            [Description("Can add or edit information of custom reports, sheets, columns, params")]
            ManageCustomReports,
        }

        [Description("Managing referrals related reports")]
        public enum ReferralModule
        {
            [Description("View New Referring Providers report")]
            ViewNewReferrers = 15000,

            [Description("Update referring provider contact and clinic name")]
            UpdateReferrer,

            [Description("View Number of Referrals (New Patient) per Month report")]
            ViewMonthlyCount,

            [Description("View Referring Provider Rating report")]
            ViewReferrerRating,

            [Description("View Referring Provider By Clinic report")]
            ViewReferrerByClinic,

            [Description("Can rename referring provider's clinic")]
            CanRenameClinic,

            [Description("View incoming referrals report")]
            ViewIncomingReferrals,

            [Description("Can add/update incoming referral record")]
            EditIncomingReferrals,

            [Description("View and edit incoming referrals which were deleted")]
            ViewDeletedReferrals,

            [Description("View referring provider contact list")]
            ViewReferrerContacts,

            [Description("Edit contact information of referring providers")]
            EditReferrerContact
        }

        [Description("Managing prior authorization procedures")]
        public enum PreAuthModule
        {
            [Description("View list of all prior authorization records")]
            ViewList = 16000,

            [Description("Update prior authorization information")]
            EditInfo,

            [Description("View Codes by Insurances Cheatsheet")]
            ViewCbiCheatsheet,

            [Description("Build Codes by Insurances Cheatsheet")]
            BuildCbiCheatsheet,

            [Description("View list of procedures")]
            ViewProcedures,

            [Description("Update procedure such as changing site id, pre-auth flag, status")]
            UpdateProcedure,

            [Description("Link billing codes to procedures")]
            LinkProcCodes,

            [Description("View the report of number of pre-auth procedures by status")]
            ViewCountByStatusReport,

            [Description("View list of procedure appointments that don't have a PA order")]
            ViewMissedPaProcAppts,

            [Description("View list of ASC surgery cases")]
            ViewAscCases,

            [Description("Update ASC surgery case information")]
            EditAscCases,

            [Description("Manage CPT codes associated with an ASC surgery case")]
            ManageCaseCptCodes,

            [Description("Manage devices and other cases applied to an ASC case")]
            ManageCaseCosts,

            [Description("View list of procedures proposed by mid-level providers")]
            ViewProposedProcedures,

            [Description("Update proposed procedures (used by mid-level providers)")]
            EditProposedProcedures,

            [Description("Approve or cancel proposed procedures (used by primary providers)")]
            ReviewProposedProcedures,

            [Description("Schedule procedure and set procedure date")]
            ScheduleProposedProcedures,
        }

        [Description("Managing system settings such as locations, location open days, encounter types, ...")]
        public enum SettingsModule
        {
            [Description("View weekly opening days at each location")]
            ViewLocationOpenDays = 17000,

            [Description("Set/update weekly opening days for each location")]
            UpdateLocationOpenDays,

            [Description("View list of insurances and their settings")]
            ViewInsurances,

            [Description("Update insurance information and settings")]
            UpdateInsurance,

            [Description("View list of locations")]
            ViewLocations,

            [Description("Update location information such as type and status")]
            UpdateLocation,

            [Description("View list of encounter types")]
            ViewEncTypes,

            [Description("Update encounter type information")]
            UpdateEncTypes,

            [Description("View list of the system settings")]
            ViewSiteSettings,

            [Description("Add or update site setting value")]
            EditSiteSetting,

            [Description("View list of name mappings")]
            ViewNameMappings,

            [Description("Update name mapping information")]
            UpdateNameMapping,
        }

        [Description("Managing appointments and appointment-related reports")]
        public enum AppointmentModule
        {
            [Description("View Daily Follow Up Patients report")]
            ViewDailyFollowUp = 18000,

            [Description("Can mark urine samples of daily follow up patients as collected")]
            CanToggleCollected,

            [Description("View Daily Urine-Collected Patients report")]
            ViewUrineCollectedReport,

            [Description("Can user (should be provider) decide if the urine should be sent to Lab")]
            CanToggleSendToLab,

            [Description("Can mark a urine sample as Reviewed")]
            CanToggleReviewed,

            [Description("User can view data of all providers")]
            CanViewDataOfAllProviders,

            [Description("View Follow Up to Discard report")]
            ViewFollowUpToDiscard,

            [Description("Can mark a urine sample as Discarded")]
            CanToggleDiscarded,

            [Description("View Daily Appointments report")]
            ViewDailyAppts,

            [Description("View One Time Appointments report")]
            ViewOneTimeAppts,

            [Description("View Weekly Appointments report")]
            ViewWeeklyAppts,

            [Description("Can export weekly appointments report to Excel")]
            CanExportWeeklyAppts,

            [Description("View Not Credentialed Appointments report")]
            ViewNotCredentialedAppts,

            [Description("View Appointment Forecast report")]
            ViewAppointmentForecast,

            [Description("View Current Week Appointments report")]
            ViewCurrentWeekAppts,

            [Description("View Patients To See Doctor report")]
            ViewPatientsToSeeDoctor
        }

        [Description("Managing checklist, billing tasks and task assignment")]
        public enum TaskModule
        {
            [Description("View list of all tasks including checklist, billing tasks, ...")]
            ViewTasks = 19000,

            [Description("Add new task or update task information")]
            EditTask,

            [Description("Assign tasks to users/employees")]
            AssignTasks,

            [Description("View Today's checklist and mark task in checklist as DONE")]
            MarkChecklistItemAsDone,

            [Description("View Today's checklist of all clinics and its results")]
            ReviewChecklist,

            [Description("View, add or remove billing task team members")]
            ManageTeamMembers,

            [Description("Submit daily tasks and figures directly (used for team members)")]
            SubmitDailyTasks,

            [Description("Used for managers who can submit daily tasks for team members")]
            CanSubmitForAllMembers,

            [Description("View daily submission tasks and figures")]
            ViewSubmissions,

            [Description("Indicates whether user can view all submissions of all team members")]
            CanViewAllSubmissions,

            [Description("Update and review daily tasks and figures submitted by team members")]
            ReviewSubmission,

            [Description("Undo reviewing a submission so that user can edit figures")]
            UndoReviewing,

            [Description("Indicates whether user can change the total lab charges")]
            CanChangeLabCount,

            [Description("View billing efficiency report")]
            ViewBillingEfficiencyReport
        }

        [Description("Managing key performance indicators, assessor groups, target audiences and related report")]
        public enum KpiModule
        {
            [Description("View list of key performance indicators")]
            ViewKpi = 20000,

            [Description("Add/edit key performance indicators")]
            EditKpi,

            [Description("View list of KPI assessor groups")]
            ViewAssessors,

            [Description("Add/edit KPI assessor group")]
            EditAssessor,

            [Description("View list of KPI target audiences")]
            ViewAudiences,

            [Description("Add/edit KPI target audience")]
            EditAudience
        }

        [Description("Managing medical assistant rating results and bonus")]
        public enum KpiBonusModule
        {
            [Description("Can evaluate KPIs in general. Used to check permission in action.")]
            EvaluateKpi = 21000,

            [Description("Can evaluate medical assistant KPIs")]
            EvaluateMaKpi,

            [Description("View MA rating summary results including total points and scores")]
            ViewRatingSummary,

            [Description("View MA rating details including sub KPI points")]
            ViewRatingDetails,

            [Description("Delete MA rating points and summary record in bonus table")]
            DeleteRatings,

            [Description("Lock and unlock/re-open the ability to evaluate MA KPIs")]
            ToggleLockRatings,

            [Description("View medical assistant bonus report")]
            ViewBonusReport,

            [Description("Update medical assistant bonus & status")]
            UpdateBonus,

            [Description("View and update MA winner discount factors")]
            ViewDiscountFactors,

            [Description("Can evaluate KPIs for Philippines Team")]
            EvaluatePhiKpi
        }

        [Description("Managing health insurances and plans")]
        public enum HealthInsuranceModule
        {
            [Description("View list of health insurances registered by employee")]
            ViewList = 22000,

            [Description("Create new health insurances or update existing entries")]
            EditHiEntry,

            [Description("View all the health insurance plan types")]
            ViewPlans,

            [Description("Add or update a health insurance plan")]
            EditPlan
        }

        [Description("Managing lab tests and results and categories")]
        public enum LabModule
        {
            [Description("View list of LCMS categories")]
            ViewLcmsCategories = 24000,

            [Description("Update LCMS category information")]
            UpdateLcmsCategory,

            [Description("View list of pending LCMS test orders")]
            ViewPendingLcmsTests,

            [Description("Update information of a pending LCMS test order")]
            UpdatePendingLcmsTest,

            [Description("Request LCMS tests from UrineAnalyzerApp or Daily Follow Up report")]
            RequestLcmsTests,

            [Description("View list of LCMS test results that are pending to upload to eCW")]
            ViewPendingLabResults,

            [Description("Update information of a pending LCMS test result set")]
            UpdatePendingLabResult,

            [Description("View list of analyzer lab results")]
            ViewAnalyzerLabResults,

            [Description("Review analyzer lab results and request new LCMS lab orders")]
            ReviewOwnLabResults,

            [Description("Review any analyzer lab results and edit LCMS requests of any providers")]
            ReviewAnyLabResults,

            [Description("Set lab sample id (accession no)")]
            SetSampleId
        }

        [Description("Managing survey forms, answers and results")]
        public enum SurveyModule
        {
            [Description("View list of survey forms")]
            ViewForms = 25000,

            [Description("Update information of survey forms")]
            UpdateForms,

            [Description("View list of survey questions")]
            ViewQuestions,

            [Description("Update information of survey questions")]
            UpdateQuestions,

            [Description("View survey answers and results")]
            ViewResults,

            [Description("View list of target audiences")]
            ViewAudiences,

            [Description("Update information of target audiences")]
            UpdateAudiences,
        }

        [Description("Managing customers, vendors, suppliers and partners")]
        public enum CustomerModule
        {
            [Description("View all customers")]
            ViewCustomers = 26000,

            [Description("View list of vendors or suppliers")]
            ViewVendors,

            [Description("View list of partners")]
            ViewPartners,

            [Description("Add or update customer information")]
            UpdateCustomer = 26010,
        }

        [Description("Managing dynamic content and documents")]
        public enum CmsModule
        {
            [Description("View list of document categories")]
            ViewCategories = 27000,

            [Description("Add or update document categories")]
            EditCategory,

            [Description("View list of documents or dynamic content")]
            ViewDocuments,

            [Description("Add or update document or dynamic content")]
            EditDocument,

            [Description("View document content")]
            ViewDocumentContent,

            [Description("View list of all media files")]
            ViewMediaFiles,

            [Description("Upload or update media file status")]
            EditMediaFile,
        }
    }
}
