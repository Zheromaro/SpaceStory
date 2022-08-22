using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler OnDash;
    public event EventHandler OnDashBack;

    [Space(8)]

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private int speedInFlip = 1;
    public bool moveFree = true;
    public bool FlipFree = true;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private int DashTimes;
    private int DashTimesMax;
    private TrailRenderer trailRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        // --------Dash---------------------------------------------------------------------------

        #region Dash
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            trailRenderer.emitting = true;
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

        if (DashTimes == 0)
        {
            canDash = false;
        }
        else if (DashTimes <= 3)
        {
            canDash = true;
        }
        #endregion

        // --------Movement-----------------------------------------------------------------------

        if (moveFree == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            movement.x = speedInFlip;
        }
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // --------Flip---------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.RightShift) && FlipFree)
        {
            transform.Rotate(0f, 180f, 0f);
            speedInFlip = speedInFlip * -1;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if(movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f)
        {
            rb.AddForce(new Vector2(movement.x * moveSpeed, movement.y * moveSpeed), ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveFree != true)
        {
            movement.x = 0f;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }

    #region Stamina
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

    #endregion

}