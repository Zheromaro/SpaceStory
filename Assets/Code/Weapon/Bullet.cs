using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 40;
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HealthEnemy enemy = hitInfo.GetComponent<HealthEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        gameObject.SetActive(false);
    }
}
