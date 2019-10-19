namespace CodeBase.Off.Website.AutoMapping {
    using AutoMapper;

    public static class MappingExtensions {
        public static T2 To<T1, T2>(this T1 t1) where T1 : class where T2 : class {
            return Mapper.Map<T1, T2>(t1);
        }

        public static void To<T1, T2>(this T1 t1, T2 t2) where T1 : class where T2 : class {
            Mapper.Map(t1, t2);
        }
    }
}