﻿using System;

namespace TidyUtility.Core.Extensions;

public static class WeakReferenceExtensions
{
    public static T? TryGetTarget<T>(this WeakReference<T> weakReference)
        where T : class
    {
        return weakReference.TryGetTarget(out T? target) ? target : null;
    }
}