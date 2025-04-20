using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public class CommonApi : ICommonApi
    {
        public static int StartCount = 0;

        public static int GetNumberRecord = 100;

        private readonly IResourceApi _resourceApi;

        private readonly IRateLimitApi _rateLimitApi;

        private readonly IQueueHandle _queueHandle;

        public CommonApi(
            IResourceApi resourceApi,
            IRateLimitApi rateLimitApi,
            IQueueHandle queueHandle
            ) 
        {
            _resourceApi = resourceApi;
            _rateLimitApi = rateLimitApi;
            _queueHandle = queueHandle;
        }
        
        public async Task<T> Get<T>(string url)
        {
            return await _resourceApi.ApiDetail<T>(url);
        }

        public async Task<List<T>> List<T>(string url)
        {
            TimeSpan delayBetweenRequests = TimeSpan.FromMilliseconds(TimeSpan.FromMinutes(1).TotalMilliseconds / await _rateLimitApi.List(RateLimitApi.SEARCH));
            var result = await _resourceApi.ApiGet<T>(url);
            await Task.Delay(delayBetweenRequests);

            return result;
        }

        public async Task<List<ApiResponse>> Create<T>(List<ApiRequest<T>> requests)
        {
            var result = new List<ApiResponse>();
            int rateLimit = await _rateLimitApi.List(RateLimitApi.UPDATE);

            _queueHandle.EnqueueData(requests);

            await _queueHandle.ProcessQueue(rateLimit, result);

            return result;
        }

        public async Task<List<T>> Delete<T>(string url)
        {
            TimeSpan delayBetweenRequests = TimeSpan.FromMilliseconds(TimeSpan.FromMinutes(1).TotalMilliseconds / await _rateLimitApi.List(RateLimitApi.UPDATE));
            var result = await _resourceApi.ApiDelete<T>(url);
            await Task.Delay(delayBetweenRequests);

            return result;
        }

        public async Task<List<int>> CountIssues(List<Issue> issues)
        {
            var result = new List<int>();
            int rateLimit = await _rateLimitApi.List(RateLimitApi.SEARCH);

            _queueHandle.EnqueueDataIssueCount(issues);

            await _queueHandle.ProcessQueueIssueCount(rateLimit, result);

            return result;
        }
    }
}
