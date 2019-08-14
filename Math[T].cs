﻿using System;
using System.Reflection;
using Platform.Exceptions;
using Platform.Reflection;
using Platform.Reflection.Sigil;

// ReSharper disable StaticFieldInGenericType

namespace Platform.Numbers
{
    public static class Math<T>
    {
        public static readonly Func<T, T> Abs;
        public static readonly Func<T, T> Negate;

        static Math()
        {
            Abs = DelegateHelpers.Compile<Func<T, T>>(emiter =>
            {
                Ensure.Always.IsNumeric<T>();
                emiter.LoadArgument(0);
                if (CachedTypeInfo<T>.IsSigned)
                {
                    emiter.Call(typeof(System.Math).GetTypeInfo().GetMethod("Abs", new[] { typeof(T) }));
                }
                emiter.Return();
            });
            Negate = DelegateHelpers.Compile<Func<T, T>>(emiter =>
            {
                Ensure.Always.IsSigned<T>();
                emiter.LoadArgument(0);
                emiter.Negate();
                emiter.Return();
            });
        }
    }
}
