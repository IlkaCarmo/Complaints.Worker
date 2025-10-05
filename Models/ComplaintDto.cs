using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Models
{
    public class ComplaintDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
    
        public string CPF { get; set; }
        
        public string Description { get; set; }

        public string Channel { get; set; }

        public string Categories { get; set; }

        public string Status { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
