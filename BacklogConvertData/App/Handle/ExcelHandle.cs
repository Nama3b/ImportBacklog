using BacklogConvertData.App.Handle;
using BacklogConvertData.App.Interface.IHandle;
using BacklogConvertData.App.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace BacklogConvertData.Classes
{
    public class ExcelHandle : IExcelHandle
    {
        private List<int> GET_DATA_FROM_COL = new List<int> { 1, 14, 42 };

        private readonly int SHEET_DEFAULT = 1;

        public static readonly int ROW_START = 10;

        public static readonly int CATEGORY_COL = 1;

        public static readonly int ISSUE_COL = 14;

        public static readonly int WIKI_COL = 42;

        public static readonly int WIKI_TITLE_ROW = 1;

        public static readonly int WIKI_TITLE_COL = 1;

        public Dictionary<int, List<string>> Read(string directory)
        {
            ResponseHandle._mainForm.AppendImportLog("Read data from excel...");

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(new FileInfo(directory)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[SHEET_DEFAULT];

                    int rowCount = worksheet.Dimension.Rows;

                    var data = new Dictionary<int, List<string>>();

                    WikiService.wikiTitle = worksheet.Cells[WIKI_TITLE_ROW, WIKI_TITLE_COL].Value.ToString();

                    foreach (var columnIndex in GET_DATA_FROM_COL)
                    {
                        data[columnIndex] = new List<string>();
                    }

                    for (int row = ROW_START; row <= rowCount; row++)
                    {
                        foreach (var columnIndex in GET_DATA_FROM_COL)
                        {
                            object cellValue = worksheet.Cells[row, columnIndex].Value;
                            if (cellValue != null)
                            {
                                data[columnIndex].Add(cellValue.ToString());
                            }
                        }
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading excel file: {ex.Message}", ex);
            }
        }
    }
}
