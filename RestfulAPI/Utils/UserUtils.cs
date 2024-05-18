using System.Text.RegularExpressions;
using Workout_API.DBContexts;
using Workout_API.Models;

namespace Workout_API.Utils
{
    public static class UserUtils
    {
        public static User? HandleGetUser(DBContext _context, string Email, int Id)
        {
            return _context.Users.SingleOrDefault(u => u.Id == Id && u.Email == Email);
        }

        public static User? HandleGetUser(DBContext _context, string Email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == Email);
        }

        public static bool ValidateEmail(string Email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex validate = new Regex(emailPattern);
            bool validEmail = validate.IsMatch(Email);

            return validEmail;
        }
    }
}
