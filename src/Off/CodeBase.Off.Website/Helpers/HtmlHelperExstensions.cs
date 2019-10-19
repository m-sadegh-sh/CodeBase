namespace CodeBase.Off.Website.Helpers {
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using CodeBase.Common.Web.Mvc;

    public static class HtmlHelperExstensions {
        public static MvcHtmlString GridLink<TGridModel>(this HtmlHelper helper,
                                                         string linkText,
                                                         object htmlAttributes = null) where TGridModel : class {
            var controllerName = HelperUtil.GetControllerName<TGridModel>();

            return helper.ActionLink(linkText,
                                     ActionNames.Grid,
                                     controllerName,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString GridLink<TGridModel>(this HtmlHelper helper,
                                                         string linkText,
                                                         CurrentMode mode,
                                                         object htmlAttributes = null) where TGridModel : class {
            var controllerName = HelperUtil.GetControllerName<TGridModel>();

            return helper.ActionLink(linkText,
                                     ActionNames.Grid,
                                     controllerName,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString EditLink<TEditModel>(this HtmlHelper helper,
                                                         TEditModel editModel,
                                                         string linkText,
                                                         object htmlAttributes = null) where TEditModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(editModel);

            return ActionLink(helper,
                              linkText,
                              ActionNames.Edit,
                              editModel,
                              routeValues,
                              htmlAttributes);
        }

        public static MvcHtmlString EditLink<TEditModel>(this HtmlHelper helper,
                                                         TEditModel editModel,
                                                         string linkText,
                                                         CurrentMode mode,
                                                         object htmlAttributes = null) where TEditModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(editModel);

            return ActionLink(helper,
                              linkText,
                              ActionNames.Edit,
                              editModel,
                              routeValues,
                              htmlAttributes,
                              mode);
        }

        public static MvcForm BeginEditForm<TEditModel>(this HtmlHelper helper,
                                                        TEditModel editModel,
                                                        FormMethod method,
                                                        object htmlAttributes = null) where TEditModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(editModel);
            var controllerName = HelperUtil.GetControllerName(editModel);

            return helper.BeginForm(ActionNames.Edit,
                                    controllerName,
                                    routeValues,
                                    method,
                                    htmlAttributes);
        }

        public static MvcHtmlString ShowLink<TShowModel>(this HtmlHelper helper,
                                                         TShowModel showModel,
                                                         string linkText,
                                                         object htmlAttributes = null) where TShowModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(showModel);

            return ActionLink(helper,
                              linkText,
                              ActionNames.Show,
                              showModel,
                              routeValues,
                              htmlAttributes);
        }

        public static MvcHtmlString ShowLink<TShowModel>(this HtmlHelper helper,
                                                         TShowModel showModel,
                                                         string linkText,
                                                         CurrentMode mode,
                                                         object htmlAttributes = null) where TShowModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(showModel);

            return ActionLink(helper,
                              linkText,
                              ActionNames.Show,
                              showModel,
                              routeValues,
                              htmlAttributes,
                              mode);
        }

        public static MvcHtmlString Admin(this HtmlHelper helper,
                                          string linkText,
                                          object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Admin,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Admin(this HtmlHelper helper,
                                          string linkText,
                                          CurrentMode mode,
                                          object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Admin,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString AdminBackup(this HtmlHelper helper,
                                                string linkText,
                                                object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Backup,
                                     ControllerNames.Admin,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString AdminBackup(this HtmlHelper helper,
                                                string linkText,
                                                CurrentMode mode,
                                                object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Backup,
                                     ControllerNames.Admin,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Home(this HtmlHelper helper,
                                         string linkText,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Home,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Home(this HtmlHelper helper,
                                         string linkText,
                                         CurrentMode mode,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Home,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Services(this HtmlHelper helper,
                                             string linkText,
                                             object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Services,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Services(this HtmlHelper helper,
                                             string linkText,
                                             CurrentMode mode,
                                             object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Services,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Portfolio(this HtmlHelper helper,
                                              string linkText,
                                              object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Portfolio,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Portfolio(this HtmlHelper helper,
                                              string linkText,
                                              CurrentMode mode,
                                              object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Portfolio,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString About(this HtmlHelper helper,
                                          string linkText,
                                          object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.About,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString About(this HtmlHelper helper,
                                          string linkText,
                                          CurrentMode mode,
                                          object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.About,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Contact(this HtmlHelper helper,
                                            string linkText,
                                            object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Contact,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Contact(this HtmlHelper helper,
                                            string linkText,
                                            CurrentMode mode,
                                            object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Contact,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Blog(this HtmlHelper helper,
                                         string linkText,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Blog,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Blog(this HtmlHelper helper,
                                         string linkText,
                                         CurrentMode mode,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Blog,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Feed(this HtmlHelper helper,
                                         string linkText,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Feed,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Feed(this HtmlHelper helper,
                                         string linkText,
                                         CurrentMode mode,
                                         object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Feed,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Search(this HtmlHelper helper,
                                           string linkText,
                                           object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Search,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Search(this HtmlHelper helper,
                                           string linkText,
                                           CurrentMode mode,
                                           object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Index,
                                     ControllerNames.Search,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        public static MvcHtmlString Subscrib(this HtmlHelper helper,
                                             string linkText,
                                             object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Subscrib,
                                     ControllerNames.Newsletters,
                                     null,
                                     htmlAttributes);
        }

        public static MvcHtmlString Subscrib(this HtmlHelper helper,
                                             string linkText,
                                             CurrentMode mode,
                                             object htmlAttributes = null) {
            return helper.ActionLink(linkText,
                                     ActionNames.Subscrib,
                                     ControllerNames.Newsletters,
                                     null,
                                     htmlAttributes,
                                     mode);
        }

        private static MvcHtmlString ActionLink<TModel>(HtmlHelper helper,
                                                        string linkText,
                                                        string actionName,
                                                        TModel model,
                                                        object routeValues,
                                                        object htmlAttributes) where TModel : class {
            var controllerName = HelperUtil.GetControllerName(model);

            return helper.ActionLink(linkText,
                                     actionName,
                                     controllerName,
                                     routeValues,
                                     htmlAttributes);
        }

        private static MvcHtmlString ActionLink<TModel>(HtmlHelper helper,
                                                        string linkText,
                                                        string actionName,
                                                        TModel model,
                                                        object routeValues,
                                                        object htmlAttributes,
                                                        CurrentMode mode) where TModel : class {
            var controllerName = HelperUtil.GetControllerName(model);

            return helper.ActionLink(linkText,
                                     actionName,
                                     controllerName,
                                     routeValues,
                                     htmlAttributes,
                                     mode);
        }
    }
}