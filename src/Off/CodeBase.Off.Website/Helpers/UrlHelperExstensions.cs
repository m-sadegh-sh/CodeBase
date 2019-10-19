namespace CodeBase.Off.Website.Helpers {
    using System.Web.Mvc;

    public static class UrlHelperExstensions {
        public static string Grid<TGridModel>(this UrlHelper helper) where TGridModel : class {
            var controllerName = HelperUtil.GetControllerName<TGridModel>();

            return Grid(helper,
                        controllerName);
        }

        public static string Grid(this UrlHelper helper,
                                  string controllerName) {
            return helper.Action(ActionNames.Grid,
                                 controllerName);
        }

        public static string Edit<TEditModel>(this UrlHelper helper,
                                              TEditModel editModel) where TEditModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(editModel);

            return Action(helper,
                          ActionNames.Edit,
                          editModel,
                          routeValues);
        }

        public static string Show<TShowModel>(this UrlHelper helper,
                                              TShowModel showModel) where TShowModel : class {
            var routeValues = ModelRouteValuesProvider.GetRouteValues(showModel);

            return Action(helper,
                          ActionNames.Show,
                          showModel,
                          routeValues);
        }

        public static string Admin(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Admin);
        }

        public static string AdminBackup(this UrlHelper helper) {
            return helper.Action(ActionNames.Backup,
                                 ControllerNames.Admin);
        }

        public static string Home(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Home);
        }

        public static string Services(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Services);
        }

        public static string Portfolio(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Portfolio);
        }

        public static string About(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.About);
        }

        public static string Contact(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Contact);
        }

        public static string Blog(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Blog);
        }

        public static string Feed(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Feed);
        }

        public static string Search(this UrlHelper helper) {
            return helper.Action(ActionNames.Index,
                                 ControllerNames.Search);
        }

        public static string Subscrib(this UrlHelper helper) {
            return helper.Action(ActionNames.Subscrib,
                                 ControllerNames.Newsletters);
        }

        private static string Action<TModel>(UrlHelper helper,
                                             string actionName,
                                             TModel model,
                                             object routeValues) where TModel : class {
            var controllerName = HelperUtil.GetControllerName(model);

            return helper.Action(actionName,
                                 controllerName,
                                 routeValues);
        }
    }
}