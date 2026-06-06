using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.PaymentSchemes
{
    public interface IPaymentSchemeValidator
    {
        PaymentScheme PaymentScheme { get; }

        bool Validate(MakePaymentRequest request, Account account);
    }
}