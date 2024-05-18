using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_API.DTO;
using Workout_API.Models;

namespace Workout_API_Test_Suite.UnitTests.DataTransferObjectTests
{
    public class UserTransferObjectTests
    {
        const string Name = "UserTransferObject";
        const string Email = "UserTransferObject@gmail.com";
        const int Id = 10;

        [Fact]
        public void ConvertUserTransferObjectToUserObject()
        {
            UserTransferObject userTransferObject = new UserTransferObject();
            userTransferObject.Id = Id;
            userTransferObject.Name = Name;
            userTransferObject.Email = Email;

            User userObject = userTransferObject.ToUser();

            Assert.NotNull(userObject);
            userObject.Id.Should().Be(Id);
            userObject.Name.Should().Be(Name);
            userObject.Email.Should().Be(Email);
        }
    }
}
