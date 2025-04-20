using BacklogConvertData.App.Api;
using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Handle
{
    public class QueueHandle : IQueueHandle
    {

        private readonly Queue<(string url, object item)> dataQueue ;

        private readonly Queue<Issue> dataQueueIssueCount;

        private readonly IApiUrlHandle _apiUrlHandle;

        private readonly IResourceApi _resourceApi;

        private int queueCount;

        public QueueHandle(
            IApiUrlHandle apiUrlHandle,
            IResourceApi resourceApi
            )
        {
            dataQueue = new Queue<(string url, object item)>();
            dataQueueIssueCount = new Queue<Issue>();
            _apiUrlHandle = apiUrlHandle;
            _resourceApi = resourceApi;
        }

        public void EnqueueData<T>(List<ApiRequest<T>> requests)
        {
            foreach (var item in requests)
            {
                foreach (var data in item.data)
                {
                    dataQueue.Enqueue((item.url, data));
                }
            }
        }

        public async Task ProcessQueue(int rateLimit, List<ApiResponse> result)
        {
            queueCount = dataQueue.Count;

            while (dataQueue.Count > 0)
            {
                for (int i = 0; i < queueCount; i += rateLimit)
                {
                    var tasks = new List<Task<ApiResponse>>();
                    var batch = dataQueue.Take(rateLimit).ToList();

                    foreach (var item in batch)
                    {
                        tasks.Add(CreateData(item.url, item.item));
                        dataQueue.Dequeue();
                    }

                    var responses = await Task.WhenAll(tasks);
                    result.AddRange(responses);

                    if (batch.Count <= rateLimit && queueCount > rateLimit)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
                }
            }
        }

        private async Task<ApiResponse> CreateData<T>(string url, T item)
        {
            return await _resourceApi.ApiPost<T>(url, item);
        }

        public void EnqueueDataIssueCount(List<Issue> issues)
        {
            foreach (var item in issues)
            {
                dataQueueIssueCount.Enqueue(item);
            }
        }

        public async Task ProcessQueueIssueCount(int rateLimit, List<int> result)
        {
            var queueCount = dataQueueIssueCount.Count;

            while (dataQueueIssueCount.Count > 0)
            {
                for (int i = 0; i < queueCount; i += rateLimit)
                {
                    var tasks = new List<Task<int>>();

                    var batch = new List<Issue>();

                    batch = dataQueueIssueCount.Take(
                        i == 0 && queueCount < rateLimit ?
                        rateLimit - queueCount :
                        rateLimit).ToList();

                    foreach (var item in batch)
                    {
                        tasks.Add(CountIssue(item));
                        dataQueueIssueCount.Dequeue();
                    }

                    var responses = await Task.WhenAll(tasks);
                    result.AddRange(responses);

                    if (batch.Count <= rateLimit && queueCount > rateLimit)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
                }
            }
        }

        private async Task<int> CountIssue(Issue item)
        {
            var url = _apiUrlHandle.UrlCountIssue(item.categoryId[0], item.versionId[0], item.summary);
            var response = await ResourceApi.client.GetStringAsync(url);

            JObject jsonResponse = JObject.Parse(response);
            return (int)jsonResponse["count"];
        }

    }
}
