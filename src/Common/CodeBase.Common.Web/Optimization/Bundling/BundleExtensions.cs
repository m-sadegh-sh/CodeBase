namespace CodeBase.Common.Web.Optimization.Bundling {
    using System.Web.Optimization;

    public static class BundleExtensions {
        public static Bundle WithTransform<TTransform>(this Bundle bundle) where TTransform : IBundleTransform, new() {
            bundle.Transforms.Add(new TTransform());

            return bundle;
        }

        public static Bundle WithTransform<TTransform>(this Bundle bundle, TTransform transform) where TTransform : IBundleTransform {
            bundle.Transforms.Add(transform);

            return bundle;
        }
    }
}