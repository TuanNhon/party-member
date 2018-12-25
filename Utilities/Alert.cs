namespace WebApp1.Utilities
{
    public class Alert
    {
        public Alert()
        {
            AlertStyle = AlertStyles.Infomation;
            Message = "Notification";
            Dismissable = true;
        }
        public const string TempDataKey = "TempDataAlerts";
        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
    }

    public static class AlertStyles
    {
        public const string Success = "success";
        public const string Infomation = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";
    }
}
