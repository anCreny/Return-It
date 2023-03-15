using Microsoft.EntityFrameworkCore;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
         optionsBuilder.UseMySql("server=return_it_db;user=root;port=5050;password=root;database=return_it_data;",
         new MySqlServerVersion(new Version(8, 0, 32)));
        
    
    }
}


