using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.RuntimeServices.Interfaces
{
    interface IRuntimeApi
    {
    }


    /// <summary>
    ///     Defines runtime comparison methods for values and objects.
    /// </summary>
    public interface IRuntimeComparer : IComparable
    {

    }

    public interface IRuntimeComparable { }

    /// <summary>
    ///     Defines a method that a type implements to compare two objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRuntimeComparer<T> : IRuntimeComparer, IComparer<T>
    {

    }

    /// <summary>
    /// Defines methods to support runtime comparison of objects for equality.
    /// </summary>
    public interface IRuntimeEqualityComparer : IEqualityComparer
    {

    }


    public interface IRuntimeEquatable : IEqualityComparer
    {

    }
    public interface IRuntimeEquatable<T> : IRuntimeEquatable, IEquatable<T>, IEqualityComparer<T>
    {

    }
}
