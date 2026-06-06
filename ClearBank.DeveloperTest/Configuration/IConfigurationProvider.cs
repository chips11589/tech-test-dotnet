namespace ClearBank.DeveloperTest.Configuration
{
    public interface IConfigurationProvider
    {
        string Get(string key);
    }
}