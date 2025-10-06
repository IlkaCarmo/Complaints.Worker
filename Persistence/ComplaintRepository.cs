using Complaints.Worker.Interfaces;
using Complaints.Worker.Models;

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
