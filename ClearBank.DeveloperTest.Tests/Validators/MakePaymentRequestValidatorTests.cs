using System.Collections.Generic;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using ClearBank.DeveloperTest.Validators.PaymentSchemes;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class MakePaymentRequestValidatorTests
    {
        [Fact]
        public void Validate_WhenAccountIsNull_ReturnsFalse()
        {
            // Arrange
            var validator = new MakePaymentRequestValidator(new Dictionary<PaymentScheme, IPaymentSchemeValidator>());

            // Act
            var result = validator.Validate(new MakePaymentRequest(), null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_WhenPaymentSchemeIsNotRegistered_ReturnsTrue()
        {
            // Arrange
            var validators = new Dictionary<PaymentScheme, IPaymentSchemeValidator>();
            var validator = new MakePaymentRequestValidator(validators);
            var request = new MakePaymentRequest
            {
                PaymentScheme = (PaymentScheme)999
            };

            // Act
            var result = validator.Validate(request, new Account());

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WhenPaymentSchemeIsRegistered_UsesValidatorResult()
        {
            // Arrange
            var paymentSchemeValidator = Substitute.For<IPaymentSchemeValidator>();
            paymentSchemeValidator.Validate(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>()).Returns(true);

            var validators = new Dictionary<PaymentScheme, IPaymentSchemeValidator>
            {
                { PaymentScheme.Bacs, paymentSchemeValidator }
            };

            var validator = new MakePaymentRequestValidator(validators);
            var request = new MakePaymentRequest
            {
                PaymentScheme = PaymentScheme.Bacs
            };
            var account = new Account();

            // Act
            var result = validator.Validate(request, account);

            // Assert
            Assert.True(result);
            paymentSchemeValidator.Received(1).Validate(request, account);
        }
    }
}
