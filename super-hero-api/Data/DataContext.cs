using Microsoft.EntityFrameworkCore;
using super_hero_api.Entities;

namespace super_hero_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<SuperHero> SuperHeros { get; set; }
    }
}
