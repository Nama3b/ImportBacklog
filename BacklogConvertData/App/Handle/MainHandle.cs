using System.Threading.Tasks;
using BacklogConvertData.App.Service;
using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
namespace BacklogConvertData.Classes.Handle
{
    public class MainHandle : IMainHandle
    {
        private readonly IResponseHandle _responseHandle;

        private readonly IExcelHandle _excelHandle;

        private readonly IIssueService _issueService;

        private readonly IWikiService _wikiService;

        private readonly ICategoryVersionService _categoryVersionService;

        public MainHandle(
            IResponseHandle responseHandle,
            IExcelHandle excelHandle,
            IIssueService issueService,
            IWikiService wikiService,
            ICategoryVersionService categoryVersionService
            )
        {
            _responseHandle = responseHandle;
            _excelHandle = excelHandle;
            _issueService = issueService;
            _wikiService = wikiService;
            _categoryVersionService = categoryVersionService;
        }

        public async Task Process(string directory, int projectId, int assigneeId)
        {
            _responseHandle.StartLog("");

            ApiUrlHandle.projectId = projectId;
            ApiUrlHandle.assigneeId = assigneeId;

            var dataExcel = _excelHandle.Read(directory);
            var categoryVersionDataReponse = await _categoryVersionService.Handle(dataExcel);
            var issueDataReponse = await _issueService.Handle(dataExcel);
            var wikiPost = await _wikiService.Handle(dataExcel, issueDataReponse);

            _responseHandle.EndLog();
        }
    }
}