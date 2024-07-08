using Microsoft.EntityFrameworkCore;
using System;

namespace scoopepooper_backend.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}
