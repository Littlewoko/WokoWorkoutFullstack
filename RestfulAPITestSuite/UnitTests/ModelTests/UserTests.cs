using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_API.Models;

namespace Workout_API_Test_Suite.UnitTests.ModelTests
{
    public class UserTests
    {
        const int testId = 10;
        const string ValidName = "Test";
        const string ValidEmail = "test@email.com";

        [Fact]
        public void InstantiateValidUserWithoutId()
        {
            User ValidUser = new User(ValidEmail, ValidName);

            Assert.NotNull(ValidUser);
            ValidUser.Name.Should().Be(ValidName);
            ValidUser.Email.Should().Be(ValidEmail);
            ValidUser.Id.Should().Be(0);
        }

        [Fact]
        public void InstantiateValidUserWithId()
        {
            User ValidUser = new User(ValidEmail, ValidName, testId);

            Assert.NotNull(ValidUser);
            ValidUser.Name.Should().Be(ValidName);
            ValidUser.Email.Should().Be(ValidEmail);
            ValidUser.Id.Should().Be(testId);
        }

        [Theory]
        [InlineData(ValidEmail, "", testId)]
        [InlineData(ValidEmail, null, testId)]
        public void InstantiateUserWithNoNameShouldFail(string Email, string Name, int Id)
        {
            try
            {
                User user = new User(Email, Name, Id);
                Assert.Fail("User instantiation should have failed due to empty name");
            }
            catch (ArgumentException ex)
            {
                ex.Message.Should().Be("User name must not be null or empty");
            }
        }

        [Theory]
        [InlineData("", ValidName, testId)]
        [InlineData("test", ValidName, testId)]
        [InlineData("test.com", ValidName, testId)]
        [InlineData("test@.com", ValidName, testId)]
        [InlineData(".com@test.co.,uk", ValidName, testId)]
        public void InstantiateUserWithInvalidEmailShouldFail(string Email, string Name, int Id)
        {
            try
            {
                User user = new User(Email, Name, Id);
                Assert.Fail("User instantiation should have failed due to invalid email");
            }
            catch (ArgumentException ex)
            {
                ex.Message.Should().Be("Invalid email provided for user");
            }
        }

    }
}
