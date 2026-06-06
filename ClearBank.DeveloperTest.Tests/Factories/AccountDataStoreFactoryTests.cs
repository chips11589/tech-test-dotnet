using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using NSubstitute;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    public class AccountDataStoreFactoryTests
    {
        [Fact]
        public void Create_WhenConfiguredAsBackup_ReturnsBackupAccountDataStore()
        {
            // Arrange
            var configurationProvider = Substitute.For<IConfigurationProvider>();
            configurationProvider.Get(ConfigurationKeys.DataStoreType).Returns("Backup");
            var factory = new AccountDataStoreFactory(configurationProvider);
        
            // Act
            var accountDataStore = factory.Create();

            // Assert
            Assert.IsType<BackupAccountDataStore>(accountDataStore);
        }

        [Fact]
        public void Create_WhenConfiguredAsPrimary_ReturnsAccountDataStore()
        {
            // Arrange
            var configurationProvider = Substitute.For<IConfigurationProvider>();
            configurationProvider.Get(ConfigurationKeys.DataStoreType).Returns("Primary");
            var factory = new AccountDataStoreFactory(configurationProvider);

            // Act
            var accountDataStore = factory.Create();

            // Assert
            Assert.IsType<AccountDataStore>(accountDataStore);
        }

        [Fact]
        public void Create_WhenConfiguredWithInvalidValue_ThrowsArgumentException()
        {
            // Arrange
            var configurationProvider = Substitute.For<IConfigurationProvider>();
            configurationProvider.Get(ConfigurationKeys.DataStoreType).Returns("InvalidValue");
            var factory = new AccountDataStoreFactory(configurationProvider);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => factory.Create());
        }
    }
}
