using System.Collections.Generic;

public class Level1 : Level
{
    public List<SongEvent> GetEventsList()
    {
        return new List<SongEvent> {
            new SongEvent(0.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.0f),
            () => ActionManager.SetBossDistance(15.0f)
            })
        };
    }
}