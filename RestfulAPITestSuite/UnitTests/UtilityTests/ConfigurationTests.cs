using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workout_API;

namespace Workout_API_Test_Suite.UnitTests.UtilityTests
{
    public class ConfigurationTests
    {
        const string connectionStringKey = "ConnectionString";
        const string testConnectionStringKey = "TESTConnectionString";

        [Fact]
        public void GetConfigurationObject()
        {
            var configuration = Configuration.GetConfiguration();
            Assert.NotNull(configuration);
        }

        [Theory]
        [InlineData(connectionStringKey)]
        [InlineData(testConnectionStringKey)]
        public void GetUserSecretFromConfigruationObject(string key)
        {
            var userSecret = Configuration.GetConfigurationItem(key);
            Assert.NotNull(userSecret);
        }

    }
}
