using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        [Fact]
        public void MakePayment_ValidatorReturnsTrue_UpdatesAccountBalanceAndPersists()
        {
            // Arrange
            var account = new Account { Balance = 180m };

            var store = Substitute.For<IAccountDataStore>();
            store.GetAccount(account.AccountNumber).Returns(account);

            var factory = Substitute.For<IAccountDataStoreFactory>();
            factory.Create().Returns(store);

            var validator = Substitute.For<IMakePaymentRequestValidator>();
            validator.Validate(Arg.Any<MakePaymentRequest>(), account).Returns(true);

            var logger = Substitute.For<ILogger<PaymentService>>();

            var service = new PaymentService(factory, validator, logger);
            var request = new MakePaymentRequest { Amount = 80m };

            // Act
            var result = service.MakePayment(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(100m, account.Balance);
            store.Received(1).UpdateAccount(account);
        }

        [Fact]
        public void MakePayment_ValidatorReturnsFalse_DoesNotUpdateAccount()
        {
            // Arrange
            var account = new Account();

            var store = Substitute.For<IAccountDataStore>();
            store.GetAccount(account.AccountNumber).Returns(account);

            var factory = Substitute.For<IAccountDataStoreFactory>();
            factory.Create().Returns(store);

            var validator = Substitute.For<IMakePaymentRequestValidator>();
            validator.Validate(Arg.Any<MakePaymentRequest>(), account).Returns(false);

            var logger = Substitute.For<ILogger<PaymentService>>();

            var service = new PaymentService(factory, validator, logger);
            var request = new MakePaymentRequest();

            // Act
            var result = service.MakePayment(request);

            // Assert
            Assert.False(result.Success);
            store.DidNotReceive().UpdateAccount(Arg.Any<Account>());
        }

        [Fact]
        public void MakePayment_WhenGetAccountThrows_LogsErrorAndReturnsFalse()
        {
            // Arrange
            var store = Substitute.For<IAccountDataStore>();
            store.When(x => x.GetAccount(Arg.Any<string>())).Do(x => throw new InvalidOperationException());

            var factory = Substitute.For<IAccountDataStoreFactory>();
            factory.Create().Returns(store);

            var validator = Substitute.For<IMakePaymentRequestValidator>();
            var logger = Substitute.For<ILogger<PaymentService>>();

            var service = new PaymentService(factory, validator, logger);
            var request = new MakePaymentRequest();

            // Act
            var result = service.MakePayment(request);

            // Assert
            Assert.False(result.Success);
            logger.Received(1).Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                Arg.Any<InvalidOperationException>(),
                Arg.Any<Func<object, Exception, string>>());
        }
    }
}
