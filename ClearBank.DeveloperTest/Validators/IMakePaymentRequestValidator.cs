using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IMakePaymentRequestValidator
    {
        bool Validate(MakePaymentRequest request, Account account);
    }
}