using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout_API.Models
{
    /// <summary>
    /// 
    /// A workout is a single session consisting of one or more movements
    /// 
    /// </summary>
    public class Workout
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Movement>? Movements { get; set; }
    
        public User User { get; set; }

        public Workout() { }

        public Workout(int id, DateTime date, User user, List<Movement>? movements = null)
        {
            Id = id;
            Date = date;
            User = user;
            Movements = movements != null ? movements : new List<Movement>();
        }
    }
}