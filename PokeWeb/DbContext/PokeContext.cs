using Microsoft.EntityFrameworkCore;
using static PokeWeb.Models.DbModel;

namespace PokeWeb
{
    public partial class PokeContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }

        public PokeContext()
        {

        }

        public PokeContext(DbContextOptions<PokeContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //當發現沒有連結字串的時候才會跑進去
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=All");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Pokemon>();
            modelBuilder.Entity<PokemonType>();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
