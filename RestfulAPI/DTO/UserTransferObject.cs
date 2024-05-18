using Workout_API.Models;

namespace Workout_API.DTO
{
    public class UserTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public User ToUser()
        {
            return new User(Email, Name, Id);
        }
    }
}
