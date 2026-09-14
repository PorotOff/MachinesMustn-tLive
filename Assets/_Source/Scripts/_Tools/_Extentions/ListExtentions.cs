using System;
using System.Collections.Generic;

public static class ListExtentions
{
    private static Random _random = new Random();

    public static T PopRandom<T>(this List<T> list)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        if (list.Count == 0)
            throw new InvalidOperationException($"{nameof(list)} is empty.");

        int randomIndex = _random.Next(list.Count);
        T randomItem = list[randomIndex];
        
        list.Remove(randomItem);

        return randomItem;
    }
}