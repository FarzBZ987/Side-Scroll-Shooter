using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBeing : MonoBehaviour
{
    [SerializeField] protected int hitPoints;
    [SerializeField] protected Animator animator;

    protected virtual void Move()
    {
    }

    protected virtual void Attack(EntityBeing other, int damage)
    {
    }

    public virtual bool TakeDamage(int damage)
    {
        if (hitPoints <= 0) return false;

        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            hitPoints = 0;
            Die();
            return true;
        }
        AttackedActions();
        return true;
    }

    protected virtual void Die()
    {
    }

    protected virtual void AttackedActions()
    {
    }
}