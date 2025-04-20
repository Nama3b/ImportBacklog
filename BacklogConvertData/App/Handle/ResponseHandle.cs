using BacklogConvertData.App.Entity;
using BacklogConvertData.App.Interface.IHandle;
using BacklogImportData;
using System.Collections.Generic;

namespace BacklogConvertData.App.Handle
{
    public class ResponseHandle : IResponseHandle
    {
        public static readonly string CATEGORY_VERSION_RESULT_TYPE = "category and version/milestone";

        public static readonly string ISSUE_RESULT_TYPE = "issue";

        public static readonly string WIKI_RESULT_TYPE = "wiki";

        public static MainForm _mainForm;

        public ResponseHandle(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void StartLog(string resultType)
        {
            _mainForm.AppendImportLog(string.IsNullOrEmpty(resultType) ? "Start import data to backlog." : $"[{resultType}] Start import data to backlog.");
        }

        public void EndLog()
        {
            _mainForm.AppendImportLog("Import data to backlog is done.");
        }

        public void ResponseResult(List<ApiResponse> response, string resultType)
        {
            if (response != null)
            {
                _mainForm.AppendImportLog($"[{resultType}] Data import to backlog is done.");
            }
            else
            {
                _mainForm.AppendImportLog($"[{resultType}] No data had import.");
            }
        }

        public void ResponseData(ApiResponse response, string resultType)
        {
            if (response == null)
            {
                _mainForm.AppendImportLog($"[{resultType}] No data has imported.");
            } 
            else
            {
                _mainForm.AppendImportLog($"[{resultType}] Import {response.name} successfully");
            }
        }
    }
}
