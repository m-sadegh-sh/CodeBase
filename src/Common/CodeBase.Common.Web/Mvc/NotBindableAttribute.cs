namespace CodeBase.Common.Web.Mvc {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NotBindableAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value != null) {
                throw new SecurityException(
                    "Binding to this property is not allowed: "
                    + validationContext.ObjectType + "." + validationContext.DisplayName);
            }

            return null;
        }
    }
}