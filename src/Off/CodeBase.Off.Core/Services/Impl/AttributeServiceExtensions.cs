namespace CodeBase.Off.Core.Services.Impl {
    using CodeBase.Off.Core.Domain;

    public static class AttributeServiceExtensions {
        public static void Save(this IAttributeService service, object owner, string key, object value) {
            service.Save(new Attribute {
                Owner = owner,
                Key = key,
                Value = value
            });
        }
    }
}