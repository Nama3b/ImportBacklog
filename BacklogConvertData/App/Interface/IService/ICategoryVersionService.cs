using BacklogConvertData.App.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Service
{
    public interface ICategoryVersionService
    {
        Task<List<ApiResponse>> Handle(Dictionary<int, List<string>> dataExcel);
    }
}
