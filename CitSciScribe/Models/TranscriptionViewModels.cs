namespace CitSciScribe.Models
{
    public class CollectionViewModel
    {
        public CollectionGroup CollectionGroup { get; set; }
    }

    public class ProjectsViewModel
    {
        public ProjectStatistics CharlestonIcth { get; set; }
    }

    public class ProjectStatistics
    {
        public int TranscriptionsTotal { get; set; }
        public int UsersContributing { get; set; }
    }

    public class SiteStatistics
    {
        public int TranscriptionsTotal { get; set; }
        public int UsersContributing { get; set; }
        public int ProjectsTotal { get; set; }
    }
}