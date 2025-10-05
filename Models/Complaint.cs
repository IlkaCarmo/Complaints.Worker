

namespace Complaints.Worker.Models
{
    public class Complaint
    {
        public int Id { get; set; }
       
        public string CustomerName { get; set; }

        public string CPF { get; set; }
     
        public string Description { get; set; }

        public string Status { get; set; }

        public string Channel { get; set; }

        public string Categories { get; set; }

        public DateTime Deadline { get; set; }
        public List<string> AttachmentUrls { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}
