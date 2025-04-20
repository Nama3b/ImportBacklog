using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Classes.Config;

namespace BacklogConvertData.App.Handle
{
    public class ApiUrlHandle : IApiUrlHandle
    {
        public static int projectId;
        public static int assigneeId;

        public static readonly string URL_USER = "users";
        public static readonly string URL_PROJECT = "projects";
        public static readonly string URL_CATEGORY = "categories";
        public static readonly string URL_VERSION = "versions";
        public static readonly string URL_ISSUE = "issues";
        public static readonly string URL_ISSUE_TYPE = "issueTypes";
        public static readonly string URL_WIKI = "wikis";
        public static readonly string URL_PRIORITY = "priorities";

        public string UrlDetail(int id, string apiType)
        {
            return AppConfig.BacklogUrl + $"{apiType}/{id}?apiKey={AppConfig.ApiKey}";
        }

        public string UrlHasProjectId(string apiType)
        {
            return AppConfig.BacklogUrl + $"projects/{projectId}/{apiType}?apiKey={AppConfig.ApiKey}";
        }

        public string UrlHasNoProjectId(string apiType)
        {
            return AppConfig.BacklogUrl + $"{apiType}?apiKey={AppConfig.ApiKey}";
        }

        public string UrlHasProjectIdInParam(string apiType)
        {
            return AppConfig.BacklogUrl + $"{apiType}?apiKey={AppConfig.ApiKey}&projectIdOrKey={projectId}";
        }

        public string UrlCountIssue(int categoryId, int versionId, string summary)
        {
            return AppConfig.BacklogUrl + $"issues/count?apiKey={AppConfig.ApiKey}&projectId[]={projectId}&categoryId[]={categoryId}&milestoneId[]={versionId}&versionId[]={versionId}&keyword={summary}";
        }

        public string UrlGetUserProjectList(int projectId, string apiType)
        {
            return AppConfig.BacklogUrl + $"projects/{projectId}/{apiType}?apiKey={AppConfig.ApiKey}";
        }

        public string UrlGetAll(string apiType, int count, int offset)
        {
            return AppConfig.BacklogUrl + $"{apiType}?apiKey={AppConfig.ApiKey}&count={count}&offset={offset}";
        }
    }
}
