using BacklogConvertData.App.Entity;
using System.Collections.Generic;

namespace BacklogConvertData.App.Interface.IHandle
{
    public interface IResponseHandle
    {
        void StartLog(string resultType);
        void EndLog();
        void ResponseResult(List<ApiResponse> response, string resultType);
        void ResponseData(ApiResponse response, string resultType);
    }
}
