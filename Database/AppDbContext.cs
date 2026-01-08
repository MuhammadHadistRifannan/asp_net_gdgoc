using Microsoft.EntityFrameworkCore;

namespace gdgoc_aspnet;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> users {get;set;}
}
