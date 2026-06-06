using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Microsoft.Extensions.Logging;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        private readonly IAccountDataStore _accountDataStore;
        private readonly IMakePaymentRequestValidator _makePaymentRequestValidator;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IAccountDataStoreFactory accountDataStoreFactory,
            IMakePaymentRequestValidator makePaymentRequestValidator,
            ILogger<PaymentService> logger)
        {
            _accountDataStoreFactory = accountDataStoreFactory;
            _accountDataStore = _accountDataStoreFactory.Create();
            _makePaymentRequestValidator = makePaymentRequestValidator;
            _logger = logger;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            try
            {
                Account account = _accountDataStore.GetAccount(request.DebtorAccountNumber);

                result.Success = _makePaymentRequestValidator.Validate(request, account);

                if (result.Success)
                {
                    account.Balance -= request.Amount;

                    _accountDataStore.UpdateAccount(account);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the payment request.");
                
                result.Success = false;
            }

            return result;
        }
    }
}
