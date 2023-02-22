using Microsoft.EntityFrameworkCore;


public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public UserContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=Terfar43$dG3ER#;database=return_it;",
            new MySqlServerVersion(new Version(8, 0, 32)));
    }
}


