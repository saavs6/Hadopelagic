using System.Collections.Generic;

public class Level1 : Level
{
    public List<SongEvent> GetEventsList()
    {
        return new List<SongEvent> {
            new SongEvent(0.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(15.5f),
            () => ActionManager.SetBossDistance(25.0f)
            }),
            new SongEvent(15.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(12.5f),
            () => ActionManager.SetBossDistance(25.0f)  
            }),
            new SongEvent(30.0f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(10f),
            () => ActionManager.SetBossDistance(25.0f)
            }),
            new SongEvent(41.0f, new Level.SongAction[] {
            () => ActionManager.SetMinionDistance(4.5f),
            () => ActionManager.SetBossDistance(17.5f)
            }),
            new SongEvent(76.0f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(9.0f),
            () => ActionManager.SetMinionDistance(25.0f),
            () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(79.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(82f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(83f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(85f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(88.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(89f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(90f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(91.25f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(93.7f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(94.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(95.0f, new Level.SongAction[] {  
                () => ActionManager.SetIsCharging(true),
                () => ActionManager.SetMinionDistance(25.0f),
            }),
            new SongEvent(97.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(100.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(100.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(103.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(103.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(106.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(107.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(107.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(109.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(110.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(112f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(113f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(115.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(115.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(116f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(118.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(119.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(120f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(124.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(124.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(125f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(125.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(126.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(127.0f, new Level.SongAction[] { 
            () => ActionManager.SetBossDistance(12.0f),
            () => ActionManager.SetMinionDistance(6.0f),
            }),
            new SongEvent(132.0f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(4)
            }),
            new SongEvent(151.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
                () => ActionManager.SetMinionDistance(25.0f),
                () => ActionManager.SetBossDistance(10.0f),
            }),
            new SongEvent(154.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(157.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(159.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(163.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(163.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(164.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(166.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(166.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(167.0f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(169.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(173f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(5)
            }),
            new SongEvent(174f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(176f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(177.25f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(176.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(181f, new Level.SongAction[] { 
                () => ActionManager.SetIsSwarming(false),
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(182.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(183f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(183.2f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(183.6f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(6)
            }),
            new SongEvent(184f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(187f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(188f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(190f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(190.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(191f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(191.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(193.75f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(195f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(196f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(4)
            }),
            new SongEvent(197f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(198f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(201f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(201.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(203f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(203.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(205f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(208f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(5)
            }),
            new SongEvent(209f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(210.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(213.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(214f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(216.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(217f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(217.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(219f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(219.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(222.25f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(222.9f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(225.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(226f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(230f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(232.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(233.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(236f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(238.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(239.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(242f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(244f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(246f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true)
            }),
            new SongEvent(248f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(250f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true)
            }),
            new SongEvent(252f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true) 
            }),
            new SongEvent(252.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(253.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(255f, new Level.SongAction[] {
                () => ActionManager.SetIsAttacking(true),
                () => ActionManager.SetMinionDistance(35.0f),
            }),
            new SongEvent(256f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(256.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(257f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(258f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true) 
            }),
            new SongEvent(261f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(261.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true) 
            }),
            new SongEvent(261.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(262f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(262.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true) 
            }),
            new SongEvent(262.5f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(5) 
            }),
            new SongEvent(264f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(265f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(265.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(266f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(267f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(267.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(268f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(269f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(269.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(270f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(271.75f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(272f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(273f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(273.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(274.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(275.75f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(276f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(276.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(277f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(277.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(278f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(279f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(282f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(284f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(286f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(288f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(288.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(289f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(289.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(290f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(290.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(291f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(291.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(292f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(292.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(293f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(293.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(294f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(294.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(295f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(295.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(294.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(296f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(296.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(297f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(297.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(298f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(298.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(299f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(299.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(300f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(303f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(304.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(306.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(307.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(309f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(310.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(312f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(313.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(315.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(316.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(318f, new Level.SongAction[] { 
                () => ActionManager.SetIsCharging(true),
            }),
            new SongEvent(319f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(321f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true),
            }),
            new SongEvent(322.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(323f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
            }),
            new SongEvent(325f, new Level.SongAction[] { 
                () => ActionManager.SetPolyCount(7)
            }),
            new SongEvent(327f, new Level.SongAction[] { 
                () => ActionManager.SetIsTailWhipping(true),
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(330f, new Level.SongAction[] { 
            () => ActionManager.SetMinionDistance(1000.5f),
            () => ActionManager.SetBossDistance(15.0f),
            () => ActionManager.SetIsSwarming(false)
            }),
            new SongEvent(330f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(333.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(337.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(343.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
            new SongEvent(346.5f, new Level.SongAction[] { 
                () => ActionManager.SetIsAttacking(true)
            }),
        };
    }
}