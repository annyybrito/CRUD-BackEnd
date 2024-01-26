using Microsoft.EntityFrameworkCore;

namespace CRUD_Estudantes_Inspirali.Data;
public class EstudanteDBContext : DbContext
{
    public EstudanteDBContext (DbContextOptions<EstudanteDBContext> options) : base(options)
    {
    }
    public DbSet<Estudante> Estudantes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("anny");
        modelBuilder.Entity<Estudante>()
            .HasKey(a => a.Id);
    }
}

