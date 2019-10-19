namespace CodeBase.Common.PackageInfo {
    using System.Reflection;

    public class CurrentBuildConstants {
        public const string Company = "CodeBase™ Inc";
        public const string Copyright = "Copyright © CodeBase™ Inc 2012";
        public const string Trademark = "CodeBase™";
        public const string Version = "1.0.*";
        public const string FileVersion = "1.0.0.0";

        public static string CurrentVersion {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}