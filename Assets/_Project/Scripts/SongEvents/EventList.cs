using System.Collections.Generic;

public class SongEventList
{
    private Queue<(float, Level.SongAction[])> events;

    public SongEventList(IEnumerable<(float, Level.SongAction[])> items)
    {
        events = new Queue<(float, Level.SongAction[])>();
        foreach (var item in items)
        {
            events.Enqueue(item);
        }
    }

    public (float, Level.SongAction[])? Pop()
    {
        if (events.Count > 0)
        {
            return events.Dequeue();
        }
        return null;
    }

    public (float, Level.SongAction[])? Peek()
    {
        if (events.Count > 0)
        {
            return events.Peek();
        }
        return null;
    }

    public int Count => events.Count;
}