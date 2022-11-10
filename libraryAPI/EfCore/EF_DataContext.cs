using Microsoft.EntityFrameworkCore;
namespace libraryAPI.EfCore;
public class EF_DataContext : DbContext
{
    public EF_DataContext(DbContextOptions<EF_DataContext> options): base(options) {}
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Relation> Relations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(row => new{
            row.id
        });
        modelBuilder.Entity<Author>().HasKey(row => new{
            row.id
        });
        modelBuilder.Entity<Relation>().HasKey(row => new{
            row.id
        });
    }
}