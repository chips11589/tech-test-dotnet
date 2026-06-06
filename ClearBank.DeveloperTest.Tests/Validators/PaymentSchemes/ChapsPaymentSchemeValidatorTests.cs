using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.PaymentSchemes;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemes
{
    public class ChapsPaymentSchemeValidatorTests
    {
        [Fact]
        public void Validate_AccountAllowsChaps_ReturnsTrue()
        {
            // Arrange
            var validator = new ChapsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_AccountIsLive_ReturnsTrue()
        {
            // Arrange
            var validator = new ChapsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_AccountDoesNotAllowChaps_ReturnsFalse()
        {
            // Arrange
            var validator = new ChapsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_AccountIsNotLive_ReturnsFalse()
        {
            // Arrange
            var validator = new ChapsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.False(result);
        }
    }
}