using Microsoft.EntityFrameworkCore;

namespace CSharpLabs.MarriageProblem.DataBase;

public class ApplicationContext : DbContext
{
    public DbSet<NameDb> Names { get; set; } = null!;
    public DbSet<AttemptMemberDb> AttemptMembers { get; set; } = null!;
    public DbSet<ResultDb> Results { get; set; } = null!;

    public ApplicationContext()
    {
        Database.EnsureCreated();
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var names = File.ReadAllLines("names.txt");
        var namesDb = new List<NameDb>();
        for (var i = 0; i < names.Length; ++i)
        {
            namesDb.Add(new NameDb() { Id = i + 1, Name = names[i] });
        }
        modelBuilder.Entity<NameDb>().HasData(namesDb);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=marriage;Username=postgres;Password=ghbdtn");
    }
}