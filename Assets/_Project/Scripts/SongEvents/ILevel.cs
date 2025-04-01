using System.Collections.Generic;

public interface Level
{
    public delegate void SongAction();
    public Dictionary<float, SongAction> GetEventsMap();
}
