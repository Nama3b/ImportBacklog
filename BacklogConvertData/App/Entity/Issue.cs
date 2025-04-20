namespace BacklogConvertData.Entity
{
    public class Issue
    {
        public int projectId { get; set; }
        public string issueKey { get; set; }
        public string summary { get; set; }
        public int issueTypeId { get; set; }
        public string categoryName { get; set; }
        public int[] categoryId { get; set; }
        public string milestoneName { get; set; }
        public int[] milestoneId { get; set; }
        public string versionName { get; set; }
        public int[] versionId { get; set; }
        public int priorityId { get; set; }
        public int assigneeId { get; set; }
    }
}
