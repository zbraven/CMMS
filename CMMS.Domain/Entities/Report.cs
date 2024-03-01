

namespace CMMS.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int ActivityLogId { get; set; }
        public virtual ActivityLog ActivityLog { get; set; }
        public string FilePath { get; set; }
        
    }
}
