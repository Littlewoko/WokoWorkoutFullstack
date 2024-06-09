using Workout_API.DBContexts;

namespace Workout_API.Models
{
    public class UserStore
    {
        private readonly DBContext _context;

        public UserStore(DBContext dBContext)
        {
            _context = dBContext;
        }
    }
}
