using BacklogConvertData.App.Api;
using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.Classes;
using BacklogConvertData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BacklogConvertData.App.Service
{
    public class CategoryVersionService : ICategoryVersionService
    {
        private readonly IApiUrlHandle _apiUrlHandle;

        private readonly IResponseHandle _responseHandle;

        private ICommonApi _commonApi;

        public CategoryVersionService(
            IApiUrlHandle apiUrlHandle,
            IResponseHandle responseHandle,
            ICommonApi commonApi
            )
        {
            _apiUrlHandle = apiUrlHandle;
            _responseHandle = responseHandle;
            _commonApi = commonApi;
        }

        public async Task<List<ApiResponse>> Handle(Dictionary<int, List<string>> dataExcel)
        {
            _responseHandle.StartLog(ResponseHandle.CATEGORY_VERSION_RESULT_TYPE);

            List<string> dataFromExcel = AddDataExcelToList(dataExcel);
            List<TypeIdName> dataFromBacklog = await _commonApi.List<TypeIdName>(_apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_CATEGORY));

            List<TypeString> dataPost = checkDataDuplicate(dataFromExcel, dataFromBacklog);

            var requests = new List<ApiRequest<TypeString>>
            {
                new ApiRequest<TypeString> { url = _apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_CATEGORY), data = dataPost },
                new ApiRequest<TypeString> { url = _apiUrlHandle.UrlHasProjectId(ApiUrlHandle.URL_VERSION), data = dataPost },
            };

            var response = await _commonApi.Create(requests);

            _responseHandle.ResponseResult(response, ResponseHandle.CATEGORY_VERSION_RESULT_TYPE);

            return response;
        }

        private List<string> AddDataExcelToList(Dictionary<int, List<string>> dataExcel)
        {
            var categoryFromExcel = new List<string>();
            var versionFromExcel = new List<TypeIdName>();
            var milestoneFromExcel = new List<TypeIdName>();

            var existingCategories = new HashSet<string>();
            foreach (var item in dataExcel)
            {
                if (item.Key == ExcelHandle.CATEGORY_COL)
                {
                    foreach (var category in item.Value)
                    {
                        if (!existingCategories.Contains(category))
                        {
                            existingCategories.Add(category);
                            categoryFromExcel.Add(category);
                            versionFromExcel.Add(new TypeIdName { name = category });
                            milestoneFromExcel.Add(new TypeIdName { name = category });
                        }
                    }
                }
            }

            return categoryFromExcel;
        }

        private List<TypeString> checkDataDuplicate(List<string> dataFromExcel, List<TypeIdName> dataFromBacklog)
        {
            return dataFromExcel
            .Where(e => !dataFromBacklog.Any(b => b.name.Equals(e, StringComparison.OrdinalIgnoreCase)))
            .Select(e => new TypeString { name = e })
            .ToList();
        }
    }
}
