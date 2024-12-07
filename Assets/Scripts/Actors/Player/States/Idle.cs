using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachine
{
    private Player player;

    public Idle(Player assignedPlayer)
    {
        player = assignedPlayer;
    }

    public override void Action()
    {
        if (player.horizontal != 0 || player.vertical != 0)
        {
            Next(player.moving);
            return;
        }
    }

    public override void Next(StateMachine stateMachine)
    {
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }
}