using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Workout_API.DBContexts;
using Workout_API.Models;
using Workout_API.Utils;

namespace Workout_API_Test_Suite.IntegrationTests
{
    public class WorkoutControllerTests
    {
        private readonly HttpClient httpClient;
        private User User { get; set; }

        public WorkoutControllerTests()
        {
            httpClient = Utils.ScaffoldApplicationAndGetClient();

            ScaffoldValidUserBeforeEachTest();
        }

        private async void ScaffoldValidUserBeforeEachTest()
        {
            User = new User("test@test.com", "test");

            var response = await PostUser(User);
            User = await GetUserFromResponse(response);
        }

        private async Task<User> GetUserFromResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }

        private async Task<HttpResponseMessage> PostUser(User user)
        {
            return await httpClient.PostAsJsonAsync("/User", User);
        }

    }
}
