using BacklogConvertData.Entity;

namespace BacklogConvertData.App.Entity
{
    public class ApiResponse
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
        public string issueKey { get; set; }
        public string summary { get; set; }
    }
}
