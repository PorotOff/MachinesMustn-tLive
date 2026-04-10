using System.Collections.Generic;

public static class QueueExtention
{
    public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
    {
        foreach (var item in items)
            queue.Enqueue(item);
    }
}