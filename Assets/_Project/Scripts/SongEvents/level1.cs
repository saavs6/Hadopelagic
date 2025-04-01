using System.Collections.Generic;

public class Level1 : Level
{
    public List<SongEvent> GetEventsList()
    {
        return new List<SongEvent> {
            new SongEvent(0.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.0f),
            () => ActionManager.SetBossDistance(15.0f)
            }),
            new SongEvent(2.5f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1.5f),
            () => ActionManager.SetBossDistance(3.0f)
            }),
            new SongEvent(5.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.0f),
            () => ActionManager.SetBossDistance(15.0f)
            }),
            new SongEvent(7.5f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1.5f),
            () => ActionManager.SetBossDistance(3.0f)
            }),
            new SongEvent(10.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.0f),
            () => ActionManager.SetBossDistance(15.0f)
            }),
            new SongEvent(12.5f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1.5f),
            () => ActionManager.SetBossDistance(3.0f)
            }),
            new SongEvent(15.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.0f),
            () => ActionManager.SetBossDistance(15.0f)
            }),
            new SongEvent(17.5f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1.5f),
            () => ActionManager.SetBossDistance(3.0f)
            })
        };
    }
}