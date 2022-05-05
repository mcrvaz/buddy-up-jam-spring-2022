using System;
using System.Collections.Generic;

public static class RandomUtils
{
    public static TSource WeightedRandom<TSource> (IList<TSource> nodes, Func<TSource, float> selector)
    {
        int nodeCount = nodes.Count;
        float total = 0;
        for (int i = 0; i < nodeCount; i++)
            total += selector(nodes[i]);

        float randomPoint = UnityEngine.Random.value * total;
        for (int i = 0; i < nodeCount; i++)
        {
            float nodeValue = selector(nodes[i]);

            if (randomPoint < nodeValue)
                return nodes[i];

            randomPoint -= nodeValue;
        }
        return nodes[nodeCount - 1];
    }
}