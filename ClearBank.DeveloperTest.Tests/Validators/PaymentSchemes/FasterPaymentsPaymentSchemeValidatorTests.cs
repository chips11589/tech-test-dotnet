using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.PaymentSchemes;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators.PaymentSchemes
{
    public class FasterPaymentsPaymentSchemeValidatorTests
    {
        [Fact]
        public void Validate_AccountAllowsFasterPayments_ReturnsTrue()
        {
            // Arrange
            var validator = new FasterPaymentsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_BalanceIsSufficient_ReturnsTrue()
        {
            // Arrange
            var validator = new FasterPaymentsPaymentSchemeValidator();
            var request = new MakePaymentRequest
            {
                Amount = 100m
            };
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 150m
            };

            // Act
            var result = validator.Validate(request, account);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_AccountDoesNotAllowFasterPayments_ReturnsFalse()
        {
            // Arrange
            var validator = new FasterPaymentsPaymentSchemeValidator();
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps
            };

            // Act
            var result = validator.Validate(new MakePaymentRequest(), account);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_BalanceIsInsufficient_ReturnsFalse()
        {
            // Arrange
            var validator = new FasterPaymentsPaymentSchemeValidator();
            var request = new MakePaymentRequest
            {
                Amount = 100m
            };
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 50m
            };

            // Act
            var result = validator.Validate(request, account);

            // Assert
            Assert.False(result);
        }
    }
}