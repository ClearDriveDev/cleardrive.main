using ClearDrive.backend.Models.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ClearDrive.backend.Context
{
    public class CASContext : DbContext
    {
        public DbSet<ProblemTicket> ProblemTickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Position> Positions { get; set; }

        public CASContext(DbContextOptions<CASContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
