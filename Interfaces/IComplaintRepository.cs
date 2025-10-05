using Complaints.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Interfaces
{
    public interface IComplaintRepository
    {
        Task SaveAsync(ComplaintDto dto, List<string> categorias);
    }
}
