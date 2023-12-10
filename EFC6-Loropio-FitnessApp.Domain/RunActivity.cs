namespace EFC6_Loropio_FitnessApp.Domain
{
    public class RunActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }

        // Foreign key for User
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
