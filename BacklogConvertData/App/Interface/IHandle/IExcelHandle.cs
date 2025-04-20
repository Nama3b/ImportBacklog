using System.Collections.Generic;

namespace BacklogConvertData.App.Interface.IHandle
{
    public interface IExcelHandle
    {
        Dictionary<int, List<string>> Read(string directory);
    }
}
