using Microsoft.EntityFrameworkCore;

namespace ASP_Pokemon
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=App.db");
        }
    }
}
