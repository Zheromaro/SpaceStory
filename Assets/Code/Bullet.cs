using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damege = 40;
    [SerializeField] private Rigidbody2D rb;


    public GameObject impactEffect;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamege(damege);
        }

        GameObject effect = ObjectPool.instance.GetPooledEffect();
         if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
