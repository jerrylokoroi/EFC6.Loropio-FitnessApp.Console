using EFC6.Loropio_FitnessApp.Data;
using SetupDb;

using (var dbContext = new FitnessAppContext())
{
    var dbSeeder = new SetupDb.DbSeeder(dbContext);

    Console.WriteLine("Select an option:");
    Console.WriteLine("1. Display Users with Activities");
    Console.WriteLine("2. Find User by ID");
    // Add more options as needed

    int option;
    if (int.TryParse(Console.ReadLine(), out option))
    {
        switch (option)
        {
            case 1:
                DbSeeder.DisplayUsersWithActivities(pageSize: 5, pageNumber: 1, sortBy: "UserNameAscending");
                break;
            case 2:
                FindUserById(dbContext);
                break;
            // Add more cases for additional options
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}

        static void FindUserById(FitnessAppContext dbContext)
{
    Console.Write("Enter User ID: ");
    if (int.TryParse(Console.ReadLine(), out int userId))
    {
        var user = dbContext.users.Find(userId);
        if (user != null)
        {
            Console.WriteLine($"User ID: {user.UserId}, Name: {user.UserName}");
        }
        else
        {
            Console.WriteLine($"User with ID {userId} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input for User ID.");
    }
}