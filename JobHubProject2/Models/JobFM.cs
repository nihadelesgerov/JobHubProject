namespace JobHubProject2.Models
{
    public class JobFM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public int Salary { get; set; }
        public string JobType { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public DateTime ApplicationDeadline { get; set; }
    }
}
