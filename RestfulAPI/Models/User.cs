using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using Workout_API.Utils;

namespace Workout_API.Models
{
    [PrimaryKey(nameof(Id), nameof(Email))]
    public class User
    {
        private string _Email = "";
        private string _Name = "";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (!UserUtils.ValidateEmail(value))
                {
                    throw new ArgumentException("Invalid email provided for user");
                }

                _Email = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if(value.IsNullOrEmpty())
                {
                    throw new ArgumentException("User name must not be null or empty");
                }

                _Name = value;
            }
        }

        public User(string Email, string Name, int Id = 0)
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
        }
    }
}
