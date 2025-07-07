 #nullable disable
 using System;
 using System.Linq.Expressions;
 using System.Reflection;

 namespace TidyUtility.Core
{
    // Adapted from: https://stackoverflow.com/a/46503267/677612
    // A lot faster than constructing a generic with T() (uses Activator),
    // and just a little slower (~35%) than calling a constructor directly

    /// <summary>
    /// Faster way to call the default constructor than using reflection including the Activator class.
    /// Useful when needing to call the default constructor on generic types like "new T()", which is slow.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Factory<T>
        where T : new()
    {
        // Cached "return new T()" delegate.
        public static readonly Func<T> Create = CreateFactory();

        private static Func<T> CreateFactory()
        {
            NewExpression newExpr = Expression.New(typeof(T));

            return Expression
                .Lambda<Func<T>>(newExpr)
                .Compile();
        }
    }

    // Adapted from: https://rogerjohansson.blog/2008/02/28/linq-expressions-creating-objects/
    public delegate T FactoryMethod<T>(params object[] args);

    public static class Factory
    {
        public static FactoryMethod<T> MethodBuilder<T>(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();
            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp = new Expression[paramsInfo.Length];

            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            NewExpression newExp = Expression.New(ctor, argsExp);
            LambdaExpression lambda = Expression.Lambda(typeof(FactoryMethod<T>), newExp, param);

            return (FactoryMethod<T>) lambda.Compile();
        }
    }
}
