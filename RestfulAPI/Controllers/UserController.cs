using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using Workout_API.DBContexts;
using Workout_API.DTO;
using Workout_API.Models;
using Workout_API.Utils;

namespace Workout_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly DBContext _context;

        public UserController(DBContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUser")]
        public IActionResult Get(string Email)
        {
            try
            {
                User? user = UserUtils.HandleGetUser(_context, Email);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "CreateUser")]
        public IActionResult CreateUser([FromBody] UserTransferObject _newUser)
        {
            try
            {
                User newUser = _newUser.ToUser();
                HandleCreateUser(newUser);
                return CreatedAtRoute("GetUser", new { newUser.Email }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(Name = "UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserTransferObject _updatedUser)
        {
            try
            {
                User updatedUser = _updatedUser.ToUser();

                User? user = UserUtils.HandleGetUser(_context, updatedUser.Email);
                if (user == null)
                    throw new InvalidOperationException("The user you have attempted to update does not exist");
                else
                    HandleUpdateUser(user, updatedUser);

                return Ok();

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(Name = "DeleteUserByEmail")]
        public IActionResult DeleteUserByEmail(string Email)
        {
            try
            {
                User? user = UserUtils.HandleGetUser(_context, Email);
                HandleDeleteUser(user);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private void HandleCreateUser(User newUser)
        {
            User? user = _context.Users.SingleOrDefault(u => u.Email == newUser.Email);
            if (user != null)
            {
                throw new InvalidOperationException("A user is already associated with that email");
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void HandleUpdateUser(User user, User updatedUser)
        {
            user.Name = updatedUser.Name;
            
            _context.SaveChanges();
        }

        private void HandleDeleteUser(User? user)
        {
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
