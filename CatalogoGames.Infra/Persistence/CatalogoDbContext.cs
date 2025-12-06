using CatalogoGames.Domain.Entity;
using CatalogoGames.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infra.Persistence;

public class CatalogoDbContext : DbContext, IUnitOfWork
{
    public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options) { }
    public DbSet<Game> Games => Set<Game>();
    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await base.SaveChangesAsync(ct);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(b =>
        {
            b.ToTable("Games");
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Platform).IsRequired().HasMaxLength(100);
            b.Property(x => x.Publisher).HasMaxLength(200);
            b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            b.Property(x => x.ReleaseDate).IsRequired();
        });
    }
}