namespace CodeBase.Common.Infrastructure.Application {
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionExtensions {
        public static string GetMemberName<T>(this Expression<Func<T>> expression) {
            return GetMemberNameCore(expression.Body);
        }

        public static string GetMemberName<T, TValue>(this Expression<Func<T, TValue>> expression) {
            return GetMemberNameCore(expression.Body);
        }

        private static string GetMemberNameCore(Expression expression) {
            var memberName = string.Empty;

            var memberExpr = expression as MemberExpression;

            if (memberExpr == null) {
                var unaryExpr = expression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                memberName = memberExpr.Member.Name;

            return memberName;
        }

        public static Type GetBaseType(this Type type, params Type[] limitedTypes) {
            var baseType = type;
            var objectType = typeof (object);

            while (baseType.BaseType != null) {
                if (baseType.BaseType == objectType)
                    break;

                if (limitedTypes == null || limitedTypes.Length == 0)
                    continue;

                if (limitedTypes.Any(t => t == type))
                    break;

                baseType = baseType.BaseType;
            }

            return baseType;
        }
    }
}