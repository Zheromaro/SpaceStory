using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] private bool SinUpDow;
    [SerializeField] private bool SinLeftRight;
    [SerializeField] private bool MoveLeftRight;
    [SerializeField] private bool MoveUpDown;

    [SerializeField] private float amp;
    [SerializeField] private float freq;
    [SerializeField] private float moveSpeed = 5;

    private Rigidbody2D rb;

    bool Move;

    Vector2 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        Move = true;
    }

    private void FixedUpdate()
    {
        Vector2 Pos = transform.position;

        if (SinLeftRight)
        {
            float sinX = Mathf.Sin(Time.time * freq) * amp;
            Pos.x = startPos.x + sinX;
        }

        if (SinUpDow)
        {
            float sinY = Mathf.Sin(Time.time * freq) * amp;
            Pos.y = startPos.y + sinY;
        }

        if (MoveLeftRight)
        {
            Pos.x -= moveSpeed * Time.fixedDeltaTime;
        }

        if (MoveUpDown)
        {
            Pos.y -= moveSpeed * Time.fixedDeltaTime;
        }

        if (Move == true)
        {
            rb.MovePosition(Pos);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            moveSpeed = moveSpeed * -1;
        }
        else
        {
            Move = false;
        }

    }
}
