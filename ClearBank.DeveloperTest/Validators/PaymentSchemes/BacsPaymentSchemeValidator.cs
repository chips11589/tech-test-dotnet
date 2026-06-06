using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.PaymentSchemes
{
    public class BacsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public PaymentScheme PaymentScheme => PaymentScheme.Bacs;

        public bool Validate(MakePaymentRequest request, Account account)
        {
            return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
}
