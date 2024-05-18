using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workout_API.Models
{
    /// <summary>
    /// 
    /// A given movement consisting of sets. A movement will target given bodyparts
    /// and will follow a given movement pattern
    /// 
    /// Example: Bench Press will target the chest and follows the push movement pattern
    /// </summary>
    public class Movement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Warmup sets will not be considered for PRS or represented in progress over time
        /// </summary>
        public List<WarmupSet> WarmupSets { get; set; }
        public List<WorkingSet> WorkingSets { get; set; }
        
        public List<BodyPart> TargetedBodyparts { get; set; }
        public MovementPattern MovementPattern { get; set; }

        public int OrderStep { get; set; }

        public Workout Workout { get; set; }
    }
}
