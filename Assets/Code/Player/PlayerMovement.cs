using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dust;
        private Rigidbody2D rb;
        private Animator animator;
        private int startHealth;

        [Space(8)]
        [Header("ForMovement")]
        [HideInInspector] public Vector2 movement;
        [HideInInspector] public float Manual = 1f;
        public bool freeHorizontalMovement = false;
        public bool freeVerticalMovement = true;
        private float x;
        private float y;


        [Space(8)]
        public float speed = 1.2f;
        public static float theTrueSpeed;
        public static bool backToNormalSpeed = true;
        [SerializeField] private float plusAmount;
        [SerializeField] private float minusAmount;

        [Space(8)]
        [SerializeField] private float thrust = 5;

        [Space(8)]
        [Header("ForCamera")]
        [SerializeField] private float cameraInSprint;
        [SerializeField] private float cameraInSlow;

        //------------------------------------------------------------------

        #region inStart
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            startHealth = GameManager.gameManager._PlayerHealth.Health;
            theTrueSpeed = speed;
        }
        #endregion

        private void Update()
        {
            if (freeHorizontalMovement)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                x = Input.GetAxisRaw("Horizontal") * plusAmount;
                movement.x = Manual + x;
            }

            if (freeVerticalMovement)
            {
                movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                y = Input.GetAxisRaw("Horizontal") * minusAmount;
                movement.y = Manual + y;
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            ManageSpeed();

        }

        private void FixedUpdate()
        {
            if (movement.x > 0.1f || movement.x < -0.1f || movement.y > 0.1f || movement.y < -0.1f)
            {
                rb.AddForce(new Vector2(movement.x * theTrueSpeed, movement.y * theTrueSpeed), ForceMode2D.Impulse);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            dust.Play();

            if (collision.CompareTag("Enemy"))
            {
                if (GameManager.gameManager._PlayerHealth.Health < startHealth)
                {
                    startHealth = GameManager.gameManager._PlayerHealth.Health;
                    Vector2 difference = transform.position - collision.transform.position;
                    difference = difference.normalized * thrust;
                    rb.AddForce(difference, ForceMode2D.Impulse);
                }
            }
        }

        private void ManageSpeed()
        {
            if (theTrueSpeed != speed && backToNormalSpeed == true)
            {
                theTrueSpeed = speed;
            }
            else if (theTrueSpeed > speed)
            {
                GameManager.gameManager._PlayerStamina.IsSprint = true;
            }
        }
    }

}