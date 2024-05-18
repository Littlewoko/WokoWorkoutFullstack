using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout_API.Models
{
    /// <summary>
    /// 
    /// A single completed set, a set is a single instance of completing
    /// some movement. A working set is one of the main sets completed as part of
    /// a workout and is considered for progress tracking.
    /// 
    /// Example: A group of reps done on after another
    /// Example: A single instance of distance moved during a run or walk
    /// Example: The time to complete a given movement such as a Plank
    /// 
    /// </summary>
    public class WorkingSet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? Reps { get; set; }

        /// <summary>
        /// 
        /// Measured in Kilograms
        /// 
        /// </summary>
        public int? Weight { get; set; } 

        /// <summary>
        /// 
        /// Measured in Meters
        /// 
        /// </summary>
        public float? Distance { get; set; }

        /// <summary>
        /// 
        /// Measured in Miliseconds
        /// 
        /// </summary>
        public int? Time { get; set; } 

        /// <summary>
        /// 
        /// Describes the relative order of this set within its grouping
        /// 
        /// </summary>
        public int OrderStep { get; set; }

        public Movement Movement { get; set; }
    }
}
