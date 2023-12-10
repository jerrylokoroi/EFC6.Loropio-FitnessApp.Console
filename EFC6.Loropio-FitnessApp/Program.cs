using EFC6.Loropio_FitnessApp.Data;
using EFC6_Loropio_FitnessApp.Domain;
using Microsoft.EntityFrameworkCore;

using (FitnessAppContext dbContext = new FitnessAppContext())
        {
    // Ensure the database is created
    dbContext.Database.EnsureCreated();

    // Delete existing data (order matters due to foreign key relationship)
    DeleteExistingData(dbContext);

    // Add Users and RunActivities
    AddUsersAndRunActivities(dbContext);

    // Display Users with their RunActivities
    DisplayUsersWithActivities(dbContext);
}


static void DeleteExistingData(FitnessAppContext dbContext)
{
    dbContext.RunActivities.RemoveRange(dbContext.RunActivities);
    dbContext.users.RemoveRange(dbContext.users);
    dbContext.SaveChanges();

    Console.WriteLine("Existing data deleted successfully!");
}


static void AddUsersAndRunActivities(FitnessAppContext dbContext)
{
    // Add User 1 with RunActivities
    var user1 = AddUser(dbContext, "JerryFeely");
    AddRunActivity(dbContext, user1, "Morning Run", 5.0);
    AddRunActivity(dbContext, user1, "Evening Jog", 3.5);

    // Add User 2 with RunActivities
    var user2 = AddUser(dbContext, "Faith");
    AddRunActivity(dbContext, user2, "Afternoon Walk", 2.0);
    AddRunActivity(dbContext, user2, "Night Sprint", 7.0);

    dbContext.SaveChanges();

    Console.WriteLine("Users and RunActivities added successfully!");
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


/*static void CreateUserWithActivities(FitnessAppContext dbContext)
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
    }*/

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



