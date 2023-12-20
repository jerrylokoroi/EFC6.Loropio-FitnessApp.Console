using EFC6.Loropio_FitnessApp.Data;
using EFC6_Loropio_FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace SetupDb
{
    public class DbSeeder
    {

        private readonly FitnessAppContext _dbContext;
        private static FitnessAppContext dbContext;

        public DbSeeder(FitnessAppContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            CreateTestData();
        }

        static void DeleteExistingData(FitnessAppContext dbContext)
        {
            dbContext.RunActivities.RemoveRange(dbContext.RunActivities);
            dbContext.users.RemoveRange(dbContext.users);
            dbContext.SaveChanges();

            Console.WriteLine("Existing data deleted successfully!");
        }

        public void CreateTestData()
        {
            // Add Users and RunActivities
            AddMultipleUsersWithActivities(5, 2);


            // Display Users with their RunActivities, sorted by UserName in ascending order
            DisplayUsersWithActivities(pageSize: 5, pageNumber: 1, sortBy: "UserNameAscending");
        }

        static void AddMultipleUsersWithActivities(int userCount, int activitiesPerUser)
        {

            var Names = new List<string>
        {
            "Jerry Ekuwam",
            "Micheal Philip",
            "Faith Dion",
            "John Kirk",
            "Harrison Gray",
        };

            foreach (var Name in Names)
            {
                var user = AddUser(dbContext, Name);

                // Add RunActivities for each user as needed
                var distance = (double)new Random().NextDouble() * 10; // Random distance between 0 and 10 km

                AddRunActivity(dbContext, user, "Morning Run", distance);
                AddRunActivity(dbContext, user, "Evening Jog", distance);
            }

            dbContext.SaveChanges();

            Console.WriteLine($"Added {userCount} Users with at least {activitiesPerUser} RunActivities each.");
        }


        static User AddUser(FitnessAppContext dbContext, string userName)
        {
            var newUser = new User
            {
                UserName = userName,
                RunActivities = new List<RunActivity>()
            };

            dbContext.users.Add(newUser);

            return newUser;
        }



        static void AddRunActivity(FitnessAppContext dbContext, User user, string name, double distance)
        {
            var newRunActivity = new RunActivity
            {
                Name = name,
                Distance = distance,
                User = user
            };

            user.RunActivities.Add(newRunActivity);
            dbContext.RunActivities.Add(newRunActivity);
        }


        public static void DisplayUsersWithActivities(int pageSize, int pageNumber, string sortBy)
        {
            var query = dbContext.users.Include(user => user.RunActivities).AsQueryable();

            switch (sortBy.ToLower())
            {
                case "usernameascending":
                    query = query.OrderBy(user => user.UserName);
                    break;
                case "usernamedescending":
                    query = query.OrderByDescending(user => user.UserName);
                    break;
                    // Add more sorting options as needed
            }

            var usersWithActivities = query
                .OrderBy(user => user.UserId) // Order by UserId for consistent pagination
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Console.WriteLine($"Page {pageNumber} of Users with RunActivities (Page Size: {pageSize}), Sorted by: {sortBy}:");
            foreach (var user in usersWithActivities)
            {
                Console.WriteLine($"User ID: {user.UserId}, Name: {user.UserName}");
                foreach (var runActivity in user.RunActivities)
                {
                    Console.WriteLine($"  RunActivity ID: {runActivity.Id}, Distance: {runActivity.Distance} km");
                }
                Console.WriteLine();
            }
        }

    }
}
