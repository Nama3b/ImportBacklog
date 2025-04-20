using BacklogConvertData.App.Entity;
using BacklogConvertData.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Interface.IHandle
{
    public interface IQueueHandle
    {
        void EnqueueData<T>(List<ApiRequest<T>> requests);
        Task ProcessQueue(int rateLimit, List<ApiResponse> result);
        void EnqueueDataIssueCount(List<Issue> issues);
        Task ProcessQueueIssueCount(int rateLimit, List<int> result);
    }
}
