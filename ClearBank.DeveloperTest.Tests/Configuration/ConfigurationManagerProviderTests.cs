using System;
using System.Configuration;
using ClearBank.DeveloperTest.Configuration;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Configuration
{
    public class ConfigurationManagerProviderTests : IDisposable
    {
        [Fact]
        public void Get_ReturnsExpectedValue()
        {
            // Arrange
            var provider = new ConfigurationManagerProvider();
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(ConfigurationKeys.DataStoreType, "Primary");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            // Act
            var value = provider.Get(ConfigurationKeys.DataStoreType);

            // Assert
            Assert.Equal("Primary", value);
        }

        public void Dispose()
        {
            // Clean up the configuration change after the test
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(ConfigurationKeys.DataStoreType);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}