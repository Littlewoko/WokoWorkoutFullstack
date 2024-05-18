using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Workout_API
{
    /// <summary>
    /// Manages configuration object which holds user secrets
    /// 
    /// implements singleton pattern
    /// </summary>
    public class Configuration
    {
        private static IConfigurationRoot? configuration;
        public static IConfigurationRoot GetConfiguration()
        {
            configuration ??= new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            return configuration;
        }

        public static string GetConfigurationItem(string key)
        {
            var config = GetConfiguration();

            string configurationItem = config[key];

            if (configurationItem.IsNullOrEmpty())
                throw new Exception("User secret has not been configured");

            return configurationItem;
        }
    }
}
