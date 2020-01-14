using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
  public class AplicationDbContext : DbContext
  {
    public AplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Pagamento> Pagamento { get; set; }

  }
}
