using Complaints.Worker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Persistence
{
    public class ComplaintsDbContext : DbContext
    {
        public ComplaintsDbContext(DbContextOptions<ComplaintsDbContext> options)
         : base(options) { }

        public DbSet<Complaint> Complaints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.ToTable("Complaints");

                entity.HasKey(c => c.Id);
                entity.Property(c => c.CustomerName).IsRequired();
                entity.Property(c => c.CPF).IsRequired().HasMaxLength(11);
                entity.Property(c => c.Description).IsRequired();
                entity.Property(c => c.Channel).HasMaxLength(20);
                entity.Property(c => c.Categories).HasMaxLength(200);
                entity.Property(c => c.Deadline).IsRequired();
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }

    }
}
