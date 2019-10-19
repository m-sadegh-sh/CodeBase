namespace CodeBase.Off.Website.Helpers {
    using System;

    using Utilities.DataTypes.ExtensionMethods;

    public static class HelperUtil {
        public static string GetControllerName<TModel>() {
            return GetControllerName(typeof (TModel));
        }

        public static string GetControllerName<TModel>(TModel model) where TModel : class {
            return GetControllerName(model.GetType());
        }

        public static string GetControllerName(Type type) {
            var typeName = type.Name;

            typeName = typeName.Replace("Model",
                                        "").
                                Replace("Grid",
                                        "").
                                Replace("Edit",
                                        "").
                                Replace("Summary",
                                        "").
                                Replace("Show",
                                        "");

            var controllerName = typeName.Pluralize();

            return controllerName;
        }
    }
}