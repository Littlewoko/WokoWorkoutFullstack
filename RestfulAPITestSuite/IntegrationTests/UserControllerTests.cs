using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Workout_API.DTO;

namespace Workout_API_Test_Suite.IntegrationTests
{
    public class UserControllerTests
    {
        private static readonly string Name = "Test";
        private static readonly string Email = "test@email.com";

        private HttpClient httpClient;

        public UserControllerTests()
        {
            httpClient = Utils.ScaffoldApplicationAndGetClient();
        }

        [Fact]
        public async Task CreateUser()
        {
            var response = await AttemptCreateValidUser(httpClient);
            response.EnsureSuccessStatusCode();

            var clientReponse = await response.Content.ReadFromJsonAsync<UserTransferObject>();
            clientReponse?.Id.Should().BePositive();
            clientReponse?.Name.Should().Be(Name);
            clientReponse?.Email.Should().Be(Email);
        }

        [Fact]
        public async Task CreateInvalidUsersShouldFail()
        {
            UserTransferObject[] invalidUsers = GetInvalidUsers();

            for (int i = 0; i < invalidUsers.Length; i++)
            {
                UserTransferObject user = invalidUsers[i];
                var response = await httpClient.PostAsJsonAsync("/User", user);
                response.StatusCode.ToString().Should().Be("BadRequest");
            }
        }

        [Fact]
        public async Task CreateUserThatAlreadyExistsShouldFail()
        {
            await AttemptCreateValidUser(httpClient);

            var badCreateResponse = await AttemptCreateValidUser(httpClient);
            badCreateResponse.StatusCode.ToString().Should().Be("BadRequest");

            string responseBody = await badCreateResponse.Content.ReadAsStringAsync();
            responseBody.Should().Be("A user is already associated with that email");
        }

        private UserTransferObject[] GetInvalidUsers()
        {
            string invalidEmail = "testemai.com";

            UserTransferObject[] invalidUsers = new UserTransferObject[]
            {
                new UserTransferObject
                {
                    Name = Name,
                    Email = string.Empty,
                    Id = 0
                },
                new UserTransferObject
                {
                    Name = string.Empty,
                    Email = Email,
                    Id = 0
                },
                new UserTransferObject
                {
                    Name = string.Empty,
                    Email = invalidEmail,
                    Id = 0
                },
                new UserTransferObject
                {
                    Name = string.Empty,
                    Email = string.Empty,
                    Id = 0
                },
            };

            return invalidUsers;
        }

        [Fact]
        public async Task GetUser()
        {
            await AttemptCreateValidUser(httpClient);
            var response = await AttemptGetUser(httpClient, Email);

            response.EnsureSuccessStatusCode();
            var clientReponse = await response.Content.ReadFromJsonAsync<UserTransferObject>();
            clientReponse?.Name.Should().Be(Name);
            clientReponse?.Email.Should().Be(Email);
        }

        [Fact]
        public async Task DeleteUser()
        {
            await AttemptCreateValidUser(httpClient);

            var response = await httpClient.DeleteAsync($"/User?Email={Email}");
            response.EnsureSuccessStatusCode();

            var ensureDeletedResponse = await httpClient.GetAsync($"/User?Email={Email}");
            ensureDeletedResponse.StatusCode.ToString().Should().Be("NotFound");
        }

        [Fact]
        public async Task UpdateUser()
        {
            // create a valid user
            await AttemptCreateValidUser(httpClient);
            var getResponse = await AttemptGetUser(httpClient, Email);
            var clientReponse = await getResponse.Content.ReadFromJsonAsync<UserTransferObject>();

            if (clientReponse == null)
            {
                throw new Exception("Failed initial user creation");
            }

            string updatedName = "Updated";
            clientReponse.Name = updatedName;
            var updateResponse = await AttemptUserUpdate(httpClient, clientReponse);
            updateResponse.EnsureSuccessStatusCode();

            // validate update succeeded
            getResponse = await AttemptGetUser(httpClient, Email);
            getResponse.EnsureSuccessStatusCode();

            clientReponse = await getResponse.Content.ReadFromJsonAsync<UserTransferObject>();
            clientReponse?.Name.Should().Be(updatedName);
        }

        [Fact]
        public async Task UpdateUserThatDoesNotExistShouldFail()
        {
            UserTransferObject user = new UserTransferObject()
            {
                Name = Name,
                Email = Email,
                Id = 0

            };

            var updateResponse = await AttemptUserUpdate(httpClient, user);
            updateResponse.StatusCode.ToString().Should().Be("BadRequest");
        }

        private static async Task<HttpResponseMessage> AttemptUserUpdate(HttpClient httpClient, UserTransferObject user)
        {
            return await httpClient.PutAsJsonAsync("/User", user);
        }

        private static async Task<HttpResponseMessage> AttemptCreateValidUser(HttpClient httpClient)
        {
            UserTransferObject? user = new UserTransferObject
            {
                Name = Name,
                Email = Email,
                Id = 0
            };

            return await httpClient.PostAsJsonAsync("/User", user);
        }

        private static async Task<HttpResponseMessage> AttemptGetUser(HttpClient httpClient, string email)
        {
            return await httpClient.GetAsync($"/User?Email={email}");
        }
    }
}