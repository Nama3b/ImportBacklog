using BacklogConvertData.App.Entity;
using BacklogConvertData.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public interface ICommonApi
    {
        Task<T> Get<T>(string url);
        Task<List<T>> List<T>(string url);
        Task<List<ApiResponse>> Create<T>(List<ApiRequest<T>> requests);
        Task<List<T>> Delete<T>(string url);
        Task<List<int>> CountIssues(List<Issue> issues);
    }
}
