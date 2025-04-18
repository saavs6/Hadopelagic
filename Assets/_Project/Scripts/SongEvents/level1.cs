using System.Collections.Generic;

public class Level1 : Level
{
    public List<SongEvent> GetEventsList()
    {
        return new List<SongEvent> {
            new SongEvent(0.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(12.5f),
            () => ActionManager.SetBossDistance(25.0f)
            }),
            new SongEvent(15.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10.5f),
            () => ActionManager.SetBossDistance(25.0f)
            }),
            new SongEvent(30.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(7.5f),
            () => ActionManager.SetBossDistance(25.0f)
            }),
            new SongEvent(43.0f, new Level.SongAction[] {
            () => ActionManager.SetMinionDistance(4.5f),
            () => ActionManager.SetBossDistance(17.5f)
            }),
            new SongEvent(79.0f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(8.0f),
            () => ActionManager.SetMinionDistance(25.0f),
            () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(79.4f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(80.2f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(81.7f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(84.35f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(84.7f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(86.2f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(88.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(89.0f, new Level.SongAction[] { 
                () => ActionManager.SetMinionDistance(4.0f),
            }),
            new SongEvent(91.3f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(93.7f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(94.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(95.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
                () => ActionManager.SetMinionDistance(15.0f),
            }),
            new SongEvent(97.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(98.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(100.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(101.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(103.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(106.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(107.0f, new Level.SongAction[] { 
                () => ActionManager.SetMinionDistance(5.0f)
            }),
            new SongEvent(114.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(115.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(115.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(117.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsSwarming(true)
            }),
            new SongEvent(121.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsSwarming(false)
            }),
            new SongEvent(122.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsSwarming(true),
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(127.0f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(11.0f),
            () => ActionManager.SetMinionDistance(5.0f),
            () => ActionManager.SetIsSwarming(false),
            }),
            new SongEvent(151.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(false)
            }),
            new SongEvent(174.0f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(15.0f),
            () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(194.5f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(10.0f),
            () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(254.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(5.0f),
            () => ActionManager.SetBossDistance(5.0f),
            () => ActionManager.SetIsSwarming(true)
            }),
            new SongEvent(332.5f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1000.5f),
            () => ActionManager.SetBossDistance(15.0f),
            () => ActionManager.SetIsSwarming(false)
            }),
        };
    }
}