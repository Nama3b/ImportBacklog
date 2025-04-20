using BacklogConvertData.App.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Service
{
    public interface IWikiService
    {
        Task<List<ApiResponse>> Handle(Dictionary<int, List<string>> dataExcel, List<ApiResponse> issueDataReponse);
    }
}
