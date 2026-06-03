using Microsoft.EntityFrameworkCore;
using TurnBookiWeb.Models;

namespace TurnBookiWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {  }

        public DbSet<Category> Categories { get; set; }
    }
}
