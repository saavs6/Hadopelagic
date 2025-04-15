using System.Collections.Generic;

public interface Level
{
    public delegate void SongAction();
    public List<SongEvent> GetEventsList();
}
