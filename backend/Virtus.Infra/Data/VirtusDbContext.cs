using Microsoft.EntityFrameworkCore;
using Virtus.Domain.Entities;

namespace Virtus.Infra.Data;

public class VirtusDbContext : DbContext
{
    public VirtusDbContext(DbContextOptions<VirtusDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.ToTable("Alunos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired();
        });
    }
}
