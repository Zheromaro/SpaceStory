using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 30;
    [SerializeField] private int damage = 40;
    [SerializeField] private int drops = 2;

    [SerializeField] private GameObject dropLootPrefab;
    [SerializeField] private GameObject deathEffect;

    private Animator animator;

    bool isAlive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerStatsSystem health = hitInfo.GetComponent<PlayerStatsSystem>();
        if (health != null & isAlive)
        {
            health.Damage(damage);
            this.health = 0;
        }

        if (this.health <= 0)
        {
            StartCoroutine("Die");
        }

    }

    private IEnumerator Die()
    {
        isAlive = false;
        animator.Play("DeathEffect");
        yield return new WaitForSeconds(1);

        for (int i = 0; i < drops; i++)
        {
            Instantiate(dropLootPrefab, transform.position + new Vector3(0, Random.Range(0, 4)), Quaternion.identity);
        }

        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    public void TakeDamage (int damege)
    {
        health -= damege;
    }

}
