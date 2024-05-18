using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Workout_API
{
    public class Configuration
    {
        private static IConfigurationRoot? configuration;
        public static IConfigurationRoot GetUserSecretsConfiguration()
        {
            configuration ??= new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            return configuration;
        }

        public static string GetConfigurationItem(string key)
        {
            var config = GetUserSecretsConfiguration();

            string configurationItem = config[key];

            if (configurationItem.IsNullOrEmpty())
                throw new Exception("User secret has not been configured");

            return configurationItem;
        }
    }
}
