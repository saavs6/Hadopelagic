using System.Collections.Generic;

public class Level2 : Level
{
    public Dictionary<float, Level.SongAction> GetEventsMap()
    {
        return new Dictionary<float, Level.SongAction>
        {
            { 0.0f, () => ActionManager.SomeAction("Parameter1") },
            { 5.0f, () => ActionManager.SomeAction("Parameter2") },
            { 10.0f, () => ActionManager.SomeAction("Parameter3") }
        };
    }
}
