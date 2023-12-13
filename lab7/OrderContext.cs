using Microsoft.EntityFrameworkCore;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Username=postgress; Password=postgrespassqord; Database=dblab";);
    }
}
