### Implemented Changes
- Refactored the `MakePayment` method and the `PaymentService` class, and split and move specific concerns into new classes.
    - Introduced `IConfigurationProvider` to abstract the use of `ConfigurationManager.AppSettings`. Considered `IOptions` but did not use it because that could be a breaking change for the consumers of the `PaymentService` class. This improves testability as it is easily to mock the configurations. This also removes the concern of how to retrieve a configuration from the `PaymentService` class.
    - Introduced `AccountDataStoreFactory` to instantiate the specific instance of the `AccountDataStore` class based on the configured `DataStoreType` in the `AppSettings`. This removes the concern of choosing which type of `DataStore` to create and how to create them from the `PaymentService` class. If later, we need to add a new type of `DataStore` or change the `DataStoreType` setting, the `PaymentService` doesn't need to change. This adheres to the `S` principle.
    - Introduced `MakePaymentRequestValidator` which utilises the `Strategy Pattern` to determine which `IPaymentSchemeValidator` to use to validate the input `MakePaymentRequest`. This provides great flexibility as we can later introduce new payment schemes validators without changing the `MakePaymentRequestValidator` class. The new `IPaymentSchemeValidator` implementation can be registered using dependency injection, and then the `MakePaymentRequestValidator` can seamlessly pick up the new validator instance to handle the new payment scheme in the request. This embraces the `O` and `L` principles.
    - Overall, the new `PaymentService` class now only depends on abstractions, not specific implementations. This makes it easy to test different concerns of the service in isolation, and inject different implementations as needed. This approach enhances testability and embraces the `D` principle.

- Added logging to improve observability.
- Added basic exception handling to ensure the code doesn't crash unexpectedly and the appropriate result is returned.
- Fixed a potential logical flaw in the old `PaymentService` code, i.e. when `request.PaymentScheme` has a value not in the switch-cases, and the `account` is `null` -> the code would just crash
- Added unit tests

### Further Changes if having more time or better understanding of the wider context
- Consider using `IOptions` for strongly-typed configuration classes. Reason for postponing: unclear about the impact on the consumers of the `PaymentService` class, e.g. `IOptions` doesn't offer direct support for the legacy structure of `App.Config` or `Web.Config` XML, while `ConfigurationManager` does.

### Key Point
- Refactoring exercise only, not changing the logic
