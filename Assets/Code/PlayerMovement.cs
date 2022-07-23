using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(8)]
    public bool shipAlive = true;
    //public Animator animator;
    bool flipLeft;
    private Rigidbody2D rb;

    public float moveSpeed;
    Vector2 movement;


    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer _trailRenderer;


    void Start()
    {
        flipLeft = true;
        rb = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            _trailRenderer.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), y: 0);
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, y: 0);
            }
            StartCoroutine(routine: StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        // -------------------------------------------------------------------------------------

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // -------------------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.E) && flipLeft == true)
        {
            flipLeft = false;
            //animator.Play("Flip Right");
        }
        else if (Input.GetKeyDown(KeyCode.E) && flipLeft == false)
        {
            flipLeft = true;
            //animator.Play("Flip Left");
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        canDash = true;

        if (shipAlive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        _trailRenderer.emitting = false;
        isDashing = false;
    }
}