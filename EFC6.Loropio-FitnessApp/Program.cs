using EFC6.Loropio_FitnessApp.Data;
using EFC6_Loropio_FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext dbContext = new FitnessAppContext())
        {
            dbContext.Database.EnsureCreated();

            // Create a User and assign RunActivities
            CreateUserWithActivities(dbContext);

            // Display Users with their RunActivities
            DisplayUsersWithActivities(dbContext);
        }


    static void CreateUserWithActivities(FitnessAppContext dbContext)
    {
        // Create a new user
        var newUser = new User
        {
            UserName = "JerryFeely",
            RunActivities = new List<RunActivity>
            {
                new RunActivity { Name = "Morning Run", Distance = 5.0 },
                new RunActivity { Name = "Evening Jog", Distance = 3.5 }
            }
        };

        dbContext.users.Add(newUser);
        dbContext.SaveChanges();

        Console.WriteLine("User created successfully with assigned RunActivities!");
    }

    static void DisplayUsersWithActivities(FitnessAppContext dbContext)
    {
        var usersWithActivities = dbContext.users
            .Include(user => user.RunActivities)
            .ToList();

        Console.WriteLine("List of Users with RunActivities:");
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



