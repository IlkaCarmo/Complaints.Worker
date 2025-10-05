using Complaints.Worker.Interfaces;
using Complaints.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Persistence
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintsDbContext _context;

        public ComplaintRepository(ComplaintsDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ComplaintDto dto, List<string> categorias)
        {
            var entity = new Complaint
            {
                CustomerName = dto.CustomerName,
                CPF = dto.CPF,
                Description = dto.Description,
                Channel = dto.Channel,
                Categories = string.Join(",", categorias),
                Deadline = dto.Deadline,
                CreatedAt = DateTime.UtcNow
            };

            _context.Complaints.Add(entity);
            await _context.SaveChangesAsync();
        }

    }
}
