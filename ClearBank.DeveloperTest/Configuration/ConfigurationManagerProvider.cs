using System.Configuration;

namespace ClearBank.DeveloperTest.Configuration
{
    public class ConfigurationManagerProvider : IConfigurationProvider
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}