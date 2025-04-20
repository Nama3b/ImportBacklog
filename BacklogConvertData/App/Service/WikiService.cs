using BacklogConvertData.App.Api;
using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Classes;
using BacklogConvertData.Classes.Config;
using BacklogConvertData.Entity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Service
{
    public class WikiService : IWikiService
    {
        public static string wikiTitle;

        private readonly string ISSUE = "作成資料";

        private readonly string CONTENT = "○△×－";

        private readonly string ISSUE_URL = "夕スクURL";

        private readonly string JP = "日本語";

        private readonly string VN = "ベトナム語";

        private readonly int StartCount = 0;

        private readonly int GetNumberRecord = 100;

        private readonly IApiUrlHandle _apiUrlHandle;

        private readonly IResponseHandle _responseHandle;

        private ICommonApi _commonApi;

        public WikiService(
            IApiUrlHandle apiUrlHandle,
            IResponseHandle responseHandle,
            ICommonApi commonApi
            )
        {
            _apiUrlHandle = apiUrlHandle;
            _responseHandle = responseHandle;
            _commonApi = commonApi;
        }

        public async Task<List<ApiResponse>> Handle(Dictionary<int, List<string>> dataExcel, List<ApiResponse> issueDataReponse)
        {
            _responseHandle.StartLog(ResponseHandle.WIKI_RESULT_TYPE);

            var wiki = new List<Wiki>();
            var content = await SetDataContent(dataExcel, issueDataReponse);

            var data = new List<Wiki>
            {
                new Wiki
                {
                    projectId = ApiUrlHandle.projectId,
                    name = wikiTitle,
                    content = content,
                }
            };

            var requests = new List<ApiRequest<Wiki>>
            {
                new ApiRequest<Wiki> { url = _apiUrlHandle.UrlHasNoProjectId(ApiUrlHandle.URL_WIKI), data = data },
            };

            var response = await _commonApi.Create(requests);

            _responseHandle.ResponseResult(response, ResponseHandle.WIKI_RESULT_TYPE);

            return response;
        }

        private async Task<string> SetDataContent(Dictionary<int, List<string>> dataExcel, List<ApiResponse> issueDataReponse)
        {
            StringBuilder markdown = new StringBuilder();
            var issueKeys = new List<string>();
            var content = new List<TypeManyString>();
            var issueSummaryReponse = new List<TypeManyString>();

            foreach (var item in issueDataReponse)
            {
                issueSummaryReponse.Add(new TypeManyString { content1 = item.summary, content2 = item.issueKey });
            }

            while (true)
            {
                var dataBatch = await _commonApi.List<Issue>(_apiUrlHandle.UrlGetAll(ApiUrlHandle.URL_ISSUE, CommonApi.GetNumberRecord, CommonApi.StartCount));

                if (dataBatch == null || dataBatch.Count == 0)
                {
                    break;
                }

                foreach (var item in dataBatch)
                {
                    issueKeys.Add(item.issueKey);
                }

                CommonApi.StartCount += CommonApi.GetNumberRecord;
            }

            markdown.Append("| ").Append(ISSUE).Append(" | ").Append(CONTENT).Append(" | ").Append(ISSUE_URL).Append(" | ").Append(JP).Append(" | ").Append(VN).AppendLine(" |");
            markdown.Append("|---------|---------|---------|---------|---------|").AppendLine();

            foreach (var item in dataExcel)
            {
                foreach (var value in item.Value)
                {
                    if (item.Key == ExcelHandle.ISSUE_COL)
                    {
                        content.Add( new TypeManyString { content1 = value });
                    }
                    else if (item.Key == ExcelHandle.WIKI_COL)
                    {
                        content.Add(new TypeManyString { content2 = value });
                    }
                }
            }

            foreach (var item in content)
            {
                foreach (var issueReponse in issueSummaryReponse)
                {
                    if (item.content1 == issueReponse.content1)
                    {
                        markdown.Append("| ").Append(issueReponse.content1).Append(" | ").Append(item.content2).Append(" | ").Append(AppConfig.IssueDetailUrl + issueReponse.content2).Append(" | ").Append(" | ").AppendLine(" |");
                    }
                }
            }

            return markdown.ToString();
        }
    }
}
