using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.PaymentSchemes;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemes
{
    public class BacsPaymentSchemeValidatorTests
    {
        [Fact]
        public void Validate_AccountAllowsBacs_ReturnsTrue()
        {
            // Arrange
            var validator = new BacsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_AccountDoesNotAllowBacs_ReturnsFalse()
        {
            // Arrange
            var validator = new BacsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.False(result);
        }
    }
}