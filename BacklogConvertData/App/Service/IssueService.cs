using BacklogConvertData.App.Api;
using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.App.Service;
using BacklogConvertData.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BacklogConvertData.Classes.Handle
{
    public class IssueService : IIssueService
    {
        private IApiUrlHandle _apiUrlHandle;

        private IResponseHandle _responseHandle;

        private ICommonApi _commonApi;

        private IComponentApi _componentApi;

        public IssueService(
            IApiUrlHandle apiUrlHandle,
            IResponseHandle responseHandle,
            ICommonApi commonApi,
            IComponentApi componentApi
            )
        {
            _apiUrlHandle = apiUrlHandle;
            _responseHandle = responseHandle;
            _commonApi = commonApi;
            _componentApi = componentApi;
        }

        public async Task<List<ApiResponse>> Handle(Dictionary<int, List<string>> dataExcel)
        {
            _responseHandle.StartLog(ResponseHandle.ISSUE_RESULT_TYPE);

            var issueFromExcel = SetProperty(dataExcel);
            var data = await SetPropertyId(issueFromExcel);
            var proccess = await SetDataAvailable(data);
            var param = RemovePropertyUnvailable(proccess);

            var requests = new List<ApiRequest<Issue>>
            {
                new ApiRequest<Issue> { url = _apiUrlHandle.UrlHasNoProjectId(ApiUrlHandle.URL_ISSUE), data = param },
            };

            var response = await _commonApi.Create(requests);

            _responseHandle.ResponseResult(response, ResponseHandle.ISSUE_RESULT_TYPE);

            return response;
        }

        private List<Issue> SetProperty(Dictionary<int, List<string>> data)
        {
            var issueFromExcel = new List<Issue>();
            var categoryIssue = new List<string>();
            var summaryIssue = new List<string>();

            foreach (var item in data)
            {
                foreach (var value in item.Value)
                {
                    if (item.Key == ExcelHandle.CATEGORY_COL)
                    {
                        categoryIssue.Add(value);
                    }
                    else if (item.Key == ExcelHandle.ISSUE_COL)
                    {
                        summaryIssue.Add(value);
                    }
                }
            }

            for (int i = 0; i < categoryIssue.Count; i++)
            {
                issueFromExcel.Add(new Issue 
                { 
                    categoryName = categoryIssue[i],
                    versionName = categoryIssue[i],
                    milestoneName = categoryIssue[i],
                    summary = summaryIssue[i] 
                });
            }

            return issueFromExcel;
        }

        private async Task<List<Issue>> SetPropertyId(List<Issue> data)
        {
            List<TypeIdName> categoryFromBacklog = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_CATEGORY));
            List<TypeIdName> versionFromBacklog = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_VERSION));


            foreach (var item in data)
            {
                var issueCategory = categoryFromBacklog.FirstOrDefault(c => c.name == item.categoryName);
                if (issueCategory != null)
                {
                    item.categoryId = new int[] { issueCategory.id };
                }

                var issueVersion = versionFromBacklog.FirstOrDefault(m => m.name == item.versionName);

                if (issueVersion != null)
                {
                    item.versionId = new int[] { issueVersion.id };
                    item.milestoneId = new int[] { issueVersion.id };
                }
            }

            return data;
        }

        private async Task<List<Issue>> SetDataAvailable(List<Issue> data)
        {
            var issueTypeDefault = await _componentApi.GetDefaultIssueType();
            var priorityDefault = await _componentApi.GetDefaultPriority();

            var issueFromBacklog = await _commonApi.List<Issue>(_apiUrlHandle.UrlHasNoProjectId(ApiUrlHandle.URL_ISSUE));
            var countIssues = issueFromBacklog.Count > 0 ? await _commonApi.CountIssues(data) : new List<int>();

            var processedData = ProcessData(data, countIssues, issueTypeDefault, priorityDefault);

            return processedData;
        }

        private List<Issue> ProcessData(List<Issue> data, List<int> countIssues, int? issueTypeDefault, int? priorityDefault)
        {
            var removeDataUnavailable = new List<Issue>();

            for (int i = 0; i < data.Count; i++)
            {
                var value = data[i];
                var count = countIssues.Count > 0 ? countIssues[i] : 0;

                if (count == 0)
                {
                    value.issueTypeId = issueTypeDefault.Value;
                    value.priorityId = priorityDefault.Value;
                    value.assigneeId = ApiUrlHandle.assigneeId;
                    value.projectId = ApiUrlHandle.projectId;
                }
                else
                {
                    removeDataUnavailable.Add(value);
                }
            }

            return data.Except(removeDataUnavailable).ToList();
        }

        private List<Issue> RemovePropertyUnvailable(List<Issue> data)
        {

            foreach (var item in data)
            {
                var itemType = item.GetType();
                PropertyInfo issueKeyProperty = itemType.GetProperty("issueKey");
                issueKeyProperty.SetValue(item, null);
                PropertyInfo categoryNameProperty = itemType.GetProperty("categoryName");
                categoryNameProperty.SetValue(item, null);
                PropertyInfo versionNameProperty = itemType.GetProperty("versionName");
                versionNameProperty.SetValue(item, null);
                PropertyInfo milestoneNameProperty = itemType.GetProperty("milestoneName");
                milestoneNameProperty.SetValue(item, null);
            }

            return data;
        }
    }
}
