using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed;
    Vector3 targetPositon;
    private void Start()
    {
        targetPositon = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPositon, speed * Time.deltaTime);

        if (transform.position == targetPositon)
        {
            Destroy(gameObject);
        }
    }
}
