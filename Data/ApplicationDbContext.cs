using Microsoft.EntityFrameworkCore;
using MonAppCRUD.Models;

namespace MonAppCRUD.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Produit>? Produits { get; set; }
    }
}
