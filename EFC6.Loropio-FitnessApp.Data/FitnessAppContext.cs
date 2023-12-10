using EFC6_Loropio_FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFC6.Loropio_FitnessApp.Data
{
    public class FitnessAppContext : DbContext
    {
        public DbSet<RunActivity> RunActivities { get; set; }

        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = localhost\\SQLEXPRESS; Database=FitnessDb; Trusted_Connection = True;"
                );

        }
    } 
}
