using BacklogConvertData.App.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Api
{
    public interface IResourceApi
    {
        Task<T> ApiDetail<T>(string url);
        Task<List<T>> ApiGet<T>(string url);
        Task<ApiResponse> ApiPost<T>(string url, object data);
        Task<List<T>> ApiDelete<T>(string url);
    }
}
