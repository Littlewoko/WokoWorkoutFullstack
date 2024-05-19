using Workout_API.Models;

namespace Workout_API.DTO
{
    public class UserTransferObject : IDataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public object ToObject()
        {
            return new User(Email, Name, Id);
        }
    }
}
