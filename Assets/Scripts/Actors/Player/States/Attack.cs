using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachine
{
    private float clearCooldown = 0.5f;

    private float currentTime = 0f;

    private Player m_player;

    public Attack(Player player)
    {
        m_player = player;
    }

    public override void OnEnter()
    {
        currentTime = 0f;
    }

    public override void Action()
    {
        currentTime = Mathf.Clamp(currentTime + Time.deltaTime, 0, clearCooldown);
        if (currentTime >= clearCooldown) Next(m_player.idle);
    }

    public override void OnExit()
    {
    }

    public override void Next(StateMachine stateMachine)
    {
    }
}