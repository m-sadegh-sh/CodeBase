namespace CodeBase.Off.Website.Configurators {
    using DataAnnotationsExtensions.ClientValidation;

    public static class ClientValidationExtensionsRegistrar {
        public static void RegisterExstensions() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();
        }
    }
}