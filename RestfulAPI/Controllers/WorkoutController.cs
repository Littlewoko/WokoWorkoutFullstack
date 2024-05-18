using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Workout_API.DBContexts;
using Workout_API.Models;
using Workout_API.Utils;

namespace Workout_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : Controller
    {
        private DBContext _context;
        public WorkoutController(DBContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllWorkouts")]
        public IActionResult Get() // todo: should only return a given users workouts
        {
            if (!_context.Workouts.Any())
            {
                return NoContent();
            }

            List<Workout> workouts = _context.Workouts.ToList();

            return Ok(workouts);
        }

        // todo: get all of a users workouts within a given date range


        [HttpGet("{Id}", Name = "GetWorkoutById")]
        public IActionResult GetWorkoutById(int Id)
        {
            Workout? workout = HandleGetWorkout(Id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        [HttpPost(Name = "CreateWorkout")]
        public IActionResult CreateWorkout([FromBody] Workout newWorkout)
        {
            try
            {
                User? user = UserUtils.HandleGetUser(_context, newWorkout.User.Email, newWorkout.User.Id);
                if (user == null)
                {
                    return BadRequest("Provided user does not exist");
                }

                newWorkout.User = user; // ensure FK constraints are properly put in place
                HandleCreateWorkout(newWorkout);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return CreatedAtRoute("GetWorkoutById", new { newWorkout.Id }, newWorkout);
        }

        [HttpPut(Name = "UpdateWorkout")]
        public IActionResult UpdateWorkout(Workout updatedWorkout)
        {
            try
            {
                Workout? workout = HandleGetWorkout(updatedWorkout.Id);
                if (workout == null)
                    throw new InvalidOperationException("The workout you have attempted to update does not exist");
                else
                    HandleUpdateWorkout(workout, updatedWorkout);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

            return Ok();
        }

        [HttpDelete(Name = "DeleteWorkout")]
        public IActionResult DeleteWorkout(int Id)
        {
            try
            {
                Workout? workout = HandleGetWorkout(Id);
                HandleDeleteWorkout(workout);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        private Workout? HandleGetWorkout(int Id)
        {
            return _context.Workouts.SingleOrDefault(u => u.Id == Id);
        }

        private void HandleCreateWorkout(Workout newWorkout)
        {
            _context.Workouts.Add(newWorkout);
            _context.SaveChanges();
        }

        /// <param name="workout">Existing record in database</param>
        /// <param name="updatedWorkout">Record containing updated fields</param>
        private void HandleUpdateWorkout(Workout workout, Workout updatedWorkout)
        {
            workout.Date = updatedWorkout.Date;

            _context.SaveChanges();
        }

        private void HandleDeleteWorkout(Workout? workout)
        {
            if(workout != null)
            {
                _context.Workouts.Remove(workout);
                _context.SaveChanges();
            }
        }
    }
}
