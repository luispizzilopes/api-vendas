using Api_Vendas.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Vendas.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Sale>? Sales { get; set; }
        public DbSet<Item>? Items { get; set; }
    }
}
