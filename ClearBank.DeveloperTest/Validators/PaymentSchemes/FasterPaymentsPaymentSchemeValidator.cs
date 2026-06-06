using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.PaymentSchemes
{
    public class FasterPaymentsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public PaymentScheme PaymentScheme => PaymentScheme.FasterPayments;

        public bool Validate(MakePaymentRequest request, Account account)
        {
            return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments)
                && account.Balance >= request.Amount;
        }
    }
}
