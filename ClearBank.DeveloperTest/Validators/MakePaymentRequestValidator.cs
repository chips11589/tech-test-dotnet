using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.PaymentSchemes;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Validators
{
    public class MakePaymentRequestValidator : IMakePaymentRequestValidator
    {
        private readonly IDictionary<PaymentScheme, IPaymentSchemeValidator> _paymentSchemeValidators;

        public MakePaymentRequestValidator(
            IDictionary<PaymentScheme, IPaymentSchemeValidator> paymentSchemeValidators)
        {
            _paymentSchemeValidators = paymentSchemeValidators;
        }

        public bool Validate(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (!_paymentSchemeValidators.TryGetValue(request.PaymentScheme, out var paymentSchemeValidator))
            {
                // If no specific validator exists for the payment scheme, we assume it's valid.
                // This is to retain the existing logic of the old code.
                return true;
            }

            return paymentSchemeValidator.Validate(request, account);
        }
    }
}