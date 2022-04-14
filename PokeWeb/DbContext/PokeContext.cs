using Microsoft.EntityFrameworkCore;
using static PokeWeb.Models.DbModel;

namespace PokeWeb
{
    public partial class PokeContext : DbContext
    {
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
            db_Pokemon_Set(modelBuilder);
            modelBuilder.Entity<db_PokemonType>()
                .ToTable("PokemonTypes");
            modelBuilder.Entity<db_TypeCompare>()
                .ToTable("TypeCompare")
                .HasKey(p => new { p.Type_1, p.Type_2 });
            modelBuilder.Entity<db_Employee>()
                .ToTable("Employee")
                .HasKey(p => p.Account);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void db_Pokemon_Set(ModelBuilder m)
        {
            m.Entity<db_Pokemon>(x =>
            {
                x.Property(a => a.No).HasColumnType("varchar(4)").IsRequired(true);
                x.Property(a => a.Count).HasColumnType("int").IsRequired(true);
                x.Property(a => a.TwName).HasColumnType("nvarchar(50)").IsRequired(true);
                x.Property(a => a.EnName).HasColumnType("nvarchar(50)").IsRequired(false);
                x.Property(a => a.JpName).HasColumnType("nvarchar(50)").IsRequired(false);
                x.Property(a => a.ImgRoute).HasColumnType("nvarchar(200)").IsRequired(false);
                x.Property(a => a.Type_1).HasColumnType("int").IsRequired(true);
                x.Property(a => a.Type_2).HasColumnType("int").IsRequired(true);
                x.Property(a => a.CreatTime).HasColumnType("datetime").IsRequired(true);
            });
            m.Entity<db_Pokemon>()
                .ToTable("Pokemons")
                .HasKey(p => p.No);
        }
    }
}
