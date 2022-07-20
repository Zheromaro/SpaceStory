using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    [SerializeField] private Rigidbody2D rb;


    public GameObject impactEffect;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
}
