using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_API.Models;

namespace Workout_API_Test_Suite.UnitTests.ModelTests
{
    public class WorkoutTests
    {
        readonly User User;
        const int ValidId = 1;
        readonly DateTime ValidDateTime = DateTime.Now;
        List<Movement> Movements = new List<Movement>();

        public WorkoutTests()
        {
            User = new User("test@email.com", "test", 10);

            int movementsCount = 10;
            PopulateMovementsList(movementsCount);
        }

        private void PopulateMovementsList(int count)
        {
            for (int i = 10; i < count; i++)
            {
                Movements.Add(new Movement());
            }
        }

        [Fact]
        public void InstantiateValidWorkoutObjectWithoutMovementsList()
        {
            Workout workout = new Workout(ValidId, ValidDateTime, User);
            Assert.NotNull(workout);
            workout.Id.Should().Be(ValidId);    
            workout.Date.Should().Be(ValidDateTime);    
            workout.User.Should().Be(User); 
            workout.Movements.Should().HaveCount(0);
        }

        [Fact]
        public void InstantiateValidWorkoutObjectWithEmptyMovementsList()
        {
            Workout workout = new Workout(ValidId, ValidDateTime, User, new List<Movement>());

            Assert.NotNull(workout);
            workout.Id.Should().Be(ValidId);
            workout.Date.Should().Be(ValidDateTime);
            workout.User.Should().Be(User);
            workout.Movements.Should().HaveCount(0);
        }

        [Fact]
        public void InstantiateValidWorkoutObjectWithPopulatedMovementsList()
        {
            Workout workout = new Workout(ValidId, ValidDateTime, User, Movements);

            Assert.NotNull(workout);
            workout.Id.Should().Be(ValidId);
            workout.Date.Should().Be(ValidDateTime);
            workout.User.Should().Be(User);
            workout.Movements.Should().HaveCount(Movements.Count);
        }
    }
}
