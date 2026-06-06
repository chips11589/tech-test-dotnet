using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.PaymentSchemes
{
    public class ChapsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public PaymentScheme PaymentScheme => PaymentScheme.Chaps;

        public bool Validate(MakePaymentRequest request, Account account)
        {
            return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps)
                && account.Status == AccountStatus.Live;
        }
    }
}
