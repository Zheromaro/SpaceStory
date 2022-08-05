using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;
    [SerializeField] private int drops = 2;

    [SerializeField] private GameObject dropLootPrefab;
    //[SerializeField] private GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerStatsSystem health = hitInfo.GetComponent<PlayerStatsSystem>();
        if (health != null)
        {
            health.Damage(damage);
        }

    }

    private void Die()
    {
        for (int i = 0; i < drops; i++)
        {
            Instantiate(dropLootPrefab, transform.position + new Vector3(0, Random.Range(0, 4)), Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public void TakeDamage (int damege)
    {
        health -= damege;

        if (health <= 0)
        {
            Die();
        }
    }

}
