using System.Collections.Generic;

namespace BacklogConvertData.App.Entity
{
    public class ApiRequest<T>
    {
        public string url { get; set; }
        public List<T> data { get; set; }
    }
}
