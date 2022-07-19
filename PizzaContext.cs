

using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }

        public DbSet<Ingrediente> Ingrediente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=pizze-db;Integrated Security=True");
        }
    }
}
