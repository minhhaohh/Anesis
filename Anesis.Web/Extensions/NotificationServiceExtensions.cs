using Radzen;

namespace Anesis.Web.Extensions
{
    public static class NotificationServiceExtensions
    {
        public static void NotifySuccess(this NotificationService notifyService, string summary = "", string details = "", int duration = 3000)
        {
            notifyService.Notify(NotificationSeverity.Success, summary, details, duration);
        }

        public static void NotifyInfo(this NotificationService notifyService, string summary = "", string details = "", int duration = 3000)
        {
            notifyService.Notify(NotificationSeverity.Info, summary, details, duration);
        }

        public static void NotifyWarning(this NotificationService notifyService, string summary = "", string details = "", int duration = 3000)
        {
            notifyService.Notify(NotificationSeverity.Warning, summary, details, duration);
        }

        public static void NotifyError(this NotificationService notifyService, string summary = "", string details = "", int duration = 5000)
        {
            notifyService.Notify(NotificationSeverity.Error, summary, details, duration);
        }

        public static void NotifyErrors(this NotificationService notifyService, string summary = "", List<string> detailList = null, int duration = 5000)
        {
            if (detailList == null)
            {
                return;
            }

            foreach (var details in detailList) 
            {
                notifyService.NotifyError(summary, details, duration);
            }
        }
    }
}
