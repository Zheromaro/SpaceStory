using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamage;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private int healthAmount;
    private int healthAmountMax;

    public void Start()
    {
        Health(100);
    }

    public void Health(int healthAmount)
    {
        healthAmountMax = healthAmount;
        this.healthAmount = healthAmount;
    }

    public void Damage(int amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0)
        {
            healthAmount = 0;
        }
        if (OnDamage != null) OnDamage(this, EventArgs.Empty);
    }

    public void Heal(int amount)
    {
        healthAmount += amount;
        if ( healthAmount > healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }

        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    public void Dead()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }

        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
}
