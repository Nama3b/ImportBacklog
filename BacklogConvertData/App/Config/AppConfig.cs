using System.Configuration;

namespace BacklogConvertData.Classes.Config
{
    public class AppConfig
    {
        public static string BacklogUrl = ConfigurationManager.AppSettings["BacklogUrl"];

        public static string IssueDetailUrl = ConfigurationManager.AppSettings["IssueDetailUrl"];

        public static string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        public static string IssueTypeDefault = ConfigurationManager.AppSettings["IssueTypeDefault"];

        public static string PriorityDefault = ConfigurationManager.AppSettings["PriorityDefault"];
    }
}
