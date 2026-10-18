using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Бросает ArgumentNullException, если последовательность null
    /// или количество элементов в ней = 0,
    /// или содержит null-элементы.
    /// </summary>
    public static void CastException<T>(this IEnumerable<T> enumerable)
        where T : class
    {
        if (enumerable is null)
            throw new ArgumentNullException(nameof(enumerable));

        if (!enumerable.Any())
            throw new ArgumentException("Sequence must not be empty.", nameof(enumerable));

        foreach (var item in enumerable)
        {
            if (item is null)
                throw new ArgumentException(
                    "Sequence contains a null element.", nameof(enumerable));
        }
    }
}