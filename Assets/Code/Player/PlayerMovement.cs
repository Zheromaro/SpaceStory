using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Space(8)]

    #region Valus: (Effect)
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private ParticleSystem speedDust;
    [SerializeField] private float thrust = 5;

    #endregion

    #region Valus: (Movement)
    [SerializeField] private float speed = 1.2f;
    [SerializeField] private bool moveFree = true;
    [SerializeField] private bool FlipFree = true;
    private float OriginalSpeed;
    private float SprintSpeed;
    private int speedInFlip = 1;
    private int startHealth;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        OriginalSpeed = speed;
        SprintSpeed = speed * 2;
        startHealth = GameManager.gameManager._PlayerHealth.Health;
    }

    private void Update()
    {
        // --------Movement-----------------------------------------------------------------------

        #region Movement
        if (moveFree == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            movement.x = speedInFlip;
        }
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(GameManager.gameManager._PlayerStamina.Stamina > 0)
            {
                speedDust.Play();
                GameManager.gameManager._PlayerStamina.UseStaminaByTime(20f);
                GameManager.gameManager.virtualCamera.m_Lens.OrthographicSize = 6.2f;
                if (speed != SprintSpeed)
                {
                    speed = SprintSpeed;
                }
            }
            else
            {
                GameManager.gameManager.virtualCamera.m_Lens.OrthographicSize = 6.25f;
                speedDust.Stop();
                if (speed != OriginalSpeed)
                {
                    speed = OriginalSpeed;
                }
            }
        }
        else
        {
            GameManager.gameManager.virtualCamera.m_Lens.OrthographicSize = 6.3f;
            speedDust.Stop();
            if (speed != OriginalSpeed)
            {
                speed = OriginalSpeed;
            }
        }

        if(Time.timeScale == 0)
        {
            animator.enabled = false;
        }
        else
        {
            animator.enabled = true;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        #endregion

        // --------Flip---------------------------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.RightShift) && FlipFree)
        {
            transform.Rotate(0f, 180f, 0f);
            speedInFlip = speedInFlip * -1;
        }
    }

    private void FixedUpdate()
    {
        if(movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f)
        {
            rb.AddForce(new Vector2(movement.x * speed, movement.y * speed), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dust.Play();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            if (GameManager.gameManager._PlayerHealth.Health < startHealth)
            {
                startHealth = GameManager.gameManager._PlayerHealth.Health;
                Vector2 difference = transform.position - hitInfo.transform.position;
                difference = difference.normalized * thrust;
                rb.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }

}