using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler OnDash;
    public event EventHandler OnDashBack;

    [Space(8)]
    public bool shipAlive = true;
    //public Animator animator;
    bool flipLeft;
    [SerializeField]private Rigidbody2D rb;

    public float moveSpeed;
    Vector2 movement;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private int DashTimes;
    private int DashTimesMax;
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
            _trailRenderer.emitting = true;
            UseStamina();

            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            StartCoroutine(routine: StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        if (DashTimes == 3)
        {
            canDash = true;
        }
        else if (DashTimes == 0)
        {
            canDash = false;
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

    private IEnumerator ContinueDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        _trailRenderer.emitting = false;
        isDashing = false;
    }

    public void Stamina(int DashTimes)
    {
        DashTimesMax = DashTimes;
        this.DashTimes = DashTimes;
    }

    public void UseStamina()
    {
        DashTimes -= 1;
        if (DashTimes < 0)
        {
            DashTimes = 0;
        }
        if (OnDash != null) OnDash(this, EventArgs.Empty);
    }

    public void BackStamina()
    {
        DashTimes += 1;
        if (DashTimes > DashTimesMax)
        {
            DashTimes = DashTimesMax;
        }
        if (OnDashBack != null) OnDashBack(this, EventArgs.Empty);
    }

    public float GetStaminaNormalized()
    {
        return (float)DashTimes / DashTimesMax;
    }
}