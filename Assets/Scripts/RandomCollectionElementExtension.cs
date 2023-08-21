using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class RandomCollectionElementExtension
{
    public static T GetRandomElement<T>(this IEnumerable<T> collection)
    {
        int i = 0;
        int target = Random.Range(0, collection.Count());

        foreach (var item in collection)
        {
            if (i == target)
                return item;

            i++;
        }

        return default(T);
    }
}
