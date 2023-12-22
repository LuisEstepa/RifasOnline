using Microsoft.EntityFrameworkCore;
using RifasOnline.Models.Entities;

namespace RifasOnline.Models
{
    public class DbRifasOnlineContext : DbContext
    {
        public DbRifasOnlineContext()
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        public DbRifasOnlineContext(DbContextOptions<DbRifasOnlineContext> options)
            : base(options)
        {
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuario>(tarea =>
            {
                tarea.ToTable("USUARIO");
                tarea.HasKey(p => p.IdUsuario);

                //tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                tarea.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(200);

                tarea.Property(p => p.Correo).IsRequired().HasMaxLength(150);

                tarea.Property(p => p.Clave).IsRequired().HasMaxLength(200);

                tarea.Property(p => p.Restablecer);

                tarea.Property(p => p.Confirmado);

                tarea.Property(p => p.Token);

            });

        }
        
    }
}
