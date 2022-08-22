using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private HealthUIvisual _healthUIvisual;

    float health;

    private void Start()
    {
        health = GameManager.gameManager._PlayerHealth.Health;
    }

    private void Update()
    {
        DamageAndHeal();

        Dead();
    }

    private void DamageAndHeal()
    {
        if (health > GameManager.gameManager._PlayerHealth.Health)
        {
            _healthUIvisual.Damaged(GetHealthNormalized());
            health = GameManager.gameManager._PlayerHealth.Health;
        }
        else if (health < GameManager.gameManager._PlayerHealth.Health)
        {
            _healthUIvisual.Healed(GetHealthNormalized());
            health = GameManager.gameManager._PlayerHealth.Health;
        }
    }

    private void Dead()
    {
        if (GameManager.gameManager._PlayerHealth.Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private float GetHealthNormalized()
    {
        return (float)GameManager.gameManager._PlayerHealth.Health / GameManager.gameManager._PlayerHealth.MaxHealth;
    }

}
