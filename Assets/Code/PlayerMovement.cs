using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool allowFlip = false;
    public bool freeMovement = false;
    public bool autoThrustForward = true;
    [Space(8)]
    public bool shipAlive = true;
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    //public Animator animator;

    Vector2 movement;
    Vector2 animationMove;
    bool flipLeft;
    bool isMove;

    void Start()
    {
        isMove = true;
        flipLeft = true;
    }

    void Update()
    {
        bool moveCommandAvailable = false;
        
        if (flipLeft == true)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                movement.x = 1.4f;
                moveCommandAvailable = true;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q)))
            {
                if (freeMovement)
                    movement.x = -0.6f;
                else
                    movement.x = 0.6f;

                moveCommandAvailable = true;
            }
            else
            {
                movement.x = 1f;
            }
        }
        else if (flipLeft == false)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (freeMovement)
                    movement.x = 0.6f;
                else
                    movement.x = -0.6f;

                moveCommandAvailable = true;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q)))
            {
                movement.x = -1.4f;
                moveCommandAvailable = true;
            }
            else
            {
                movement.x = -1f;
            }
        }
        
        movement.y = Input.GetAxisRaw("Vertical");
        //animationMove.y = Input.GetAxisRaw("Vertical");
        //animator.SetFloat("Vertical", animationMove.y);

        if (moveCommandAvailable || autoThrustForward)
        {

            //animationMove.x = Input.GetAxisRaw("Horizontal");

            //animator.SetFloat("Horizontal", animationMove.x);
            //animator.SetFloat("Speed", animationMove.sqrMagnitude);
        }
        else
        {
            movement.x = 0;
            // movement.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isMove = true;
        }

        // -------------------------------------------------------------------------------------

        if (allowFlip)
        {
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
    }

    private void FixedUpdate()
    {
        if (isMove == true && shipAlive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flip Right") && flipLeft == true)
        {
            flipLeft = false;
            //animator.Play("Flip Right");
        }

        if (collision.CompareTag("Flip Left") && flipLeft == false)
        {
            flipLeft = true;
            //animator.Play("Flip Left");
        }

        if (collision.CompareTag("Stop"))
        {
            isMove = false;
        }
    }
}