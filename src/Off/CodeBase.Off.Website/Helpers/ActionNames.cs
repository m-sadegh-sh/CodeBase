namespace CodeBase.Off.Website.Helpers {
    public static class ActionNames {
        public const string Index = "Index";
        public const string Grid = "Grid";
        public const string List = "List";
        public const string ListLight = "List.Light";
        public const string Edit = "Edit";
        public const string Show = "Show";
        public const string Backup = "Backup";
        public const string Subscrib = "Subscrib";
        public const string Members = "Members";
        public const string GoogleAnalytics = "GoogleAnalytics";
    }

    public static class Structure {
        public static class Admin {
            public const string Index = "Index";
        }
    }

    public static class Views {
        public static class About {
            public const string _AboutLayout = "~/Views/About/_AboutLayout.cshtml";
            public const string _InDepth = "~/Views/About/_InDepth.cshtml";
            public const string _MemberDetailsBox = "~/Views/About/_MemberDetailsBox.cshtml";
            public const string _MemberMetas = "~/Views/About/_MemberMetas.cshtml";
            public const string _MemberResume = "~/Views/About/_MemberResume.cshtml";
            public const string _Members = "~/Views/About/_Members.cshtml";
            public const string Index = "~/Views/About/Index.cshtml";
            public const string Show = "~/Views/About/Show.cshtml";
        }

        public static class Shared {
            public static class Layouts {
                public const string _LayoutBase = "~/Views/Shared/Layouts/_LayoutBase.cshtml";
                public const string _MailLayoutBase = "~/Views/Shared/Layouts/_MailLayoutBase.cshtml";
                public const string _NullLayout = "~/Views/Shared/Layouts/_NullLayout.cshtml";
            }

            public static class Partials {
                public const string NoMatch = "~/Views/Shared/Partials/NoMatch.cshtml";
                public const string NoMore = "~/Views/Shared/Partials/NoMore.cshtml";
                public const string NoRecord = "~/Views/Shared/Partials/NoRecord.cshtml";
            }
        }
    }
}