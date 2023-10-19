using DiplomskiBlazor.Shared;

namespace DiplomskiBlazor.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Korisnik> Korisnici {get; set;}
        public DbSet<Vezba> Vezbe { get; set; }
        public DbSet<Statistika> Statistike { get; set; }
        public DbSet<DeoTela> DeloviTela { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}