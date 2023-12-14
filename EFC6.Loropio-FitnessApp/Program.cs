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
    AddMultipleUsersWithActivities(dbContext, 15, 2);

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


/*static void AddUsersAndRunActivities(FitnessAppContext dbContext)
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
}*/


static void AddMultipleUsersWithActivities(FitnessAppContext dbContext, int userCount, int activitiesPerUser)
{

    var Names = new List<string>
        {
            "Jerry Ekuwam",
            "Micheal Philip",
            "Faith Dion",
            "John Kirk",
            "Harrison Gray",
            "Bobby Johnson",
            "John Smith",
            "Morgan Freeman",
            "Lucy Filley",
            "Adolphe Nkoranyi",
            "Jonas Blue",
            "Alex Bayern",
            "John Doe",
            "Alice Smith",
            "Bob Johnson",
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



