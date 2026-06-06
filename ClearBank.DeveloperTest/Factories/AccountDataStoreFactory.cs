using System;
using ClearBank.DeveloperTest.Configuration;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Enums;

namespace ClearBank.DeveloperTest.Factories
{
    public class AccountDataStoreFactory : IAccountDataStoreFactory
    {
        private readonly IConfigurationProvider _configurationProvider;

        public AccountDataStoreFactory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public IAccountDataStore Create()
        {
            if (!Enum.TryParse(_configurationProvider.Get(ConfigurationKeys.DataStoreType), out DataStoreType dataStoreType))
            {
                throw new ArgumentException("Invalid DataStoreType configuration");
            }

            return dataStoreType switch
            {
                DataStoreType.Backup => new BackupAccountDataStore(),
                DataStoreType.Primary => new AccountDataStore(),
                _ => throw new InvalidOperationException("Unsupported DataStoreType configuration")
            };
        }
    }
}