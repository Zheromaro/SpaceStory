using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 20;

    public GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HealthSystem health = hitInfo.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.Damage(damage);
        }

    }
    public void TakeDamage (int damege)
    {
        health -= damege;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
