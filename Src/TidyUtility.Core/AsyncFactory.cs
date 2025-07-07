 #nullable disable
 using System;
 using System.Threading.Tasks;

 namespace TidyUtility.Core;

// Adapted from: https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html

public interface IAsyncInitializer
{
    Task AsyncInitialization { get; }
}

public static class AsyncFactory
{
    public static async Task<T> ConstructAsync<T>()
        where T : IAsyncInitializer, new()
    {
        T obj = Factory<T>.Create();
        await obj.AsyncInitialization;
        return obj;
    }

    public static async Task<T> ConstructAsync<T>(Func<T> constructorFunc)
        where T : IAsyncInitializer
    {
        T obj = constructorFunc();
        await obj.AsyncInitialization;
        return obj;
    }
}