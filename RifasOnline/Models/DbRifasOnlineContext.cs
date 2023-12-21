using Microsoft.EntityFrameworkCore;
using RifasOnline.Models.Entities;

namespace RifasOnline.Models
{
    public class DbRifasOnlineContext : DbContext
    {
        public DbRifasOnlineContext()
        {
        }

        public DbRifasOnlineContext(DbContextOptions<DbRifasOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
        }
    }
}
