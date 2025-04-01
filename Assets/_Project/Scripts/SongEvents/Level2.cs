using System.Collections.Generic;

public class Level2 : Level
{
    public List<SongEvent> GetEventsList()
    {
        return new List<SongEvent> {
            new SongEvent(0.0f, new Level.SongAction[] { 
                () => ActionManager.SetMinionDistance(10.0f),
                () => ActionManager.SetBossDistance(14.5f)
            }),
            new SongEvent(15.0f, new Level.SongAction[] { 
                () => ActionManager.SetMinionDistance(1.5f),
                () => ActionManager.SetBossDistance(3.0f)
            }),
            new SongEvent(30.0f, new Level.SongAction[] { 
                () => ActionManager.SetMinionDistance(4.5f),
                () => ActionManager.SetBossDistance(6.0f)
            })
        };
    }
}
