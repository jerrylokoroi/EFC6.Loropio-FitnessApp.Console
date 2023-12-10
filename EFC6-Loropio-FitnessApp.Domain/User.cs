using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC6_Loropio_FitnessApp.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        // Navigation property for RunActivities
        public List<RunActivity> RunActivities { get; set; }
    }
}
