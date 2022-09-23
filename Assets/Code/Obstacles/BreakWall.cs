using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;  

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && GameManager.gameManager._PlayerStamina.IsSprint == true)
        {
            StartCoroutine(DeathEffect());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.gameManager._PlayerStamina.IsSprint == true)
        {
            StartCoroutine(DeathEffect());
        }
    }

    private IEnumerator DeathEffect()
    {
        boxCollider.enabled = false;
        animator.Play("Destroy");
        GameManager.gameManager.DoSlowMotion();
        yield return new WaitForSecondsRealtime(1.5f);
        Destroy(gameObject);
    }
}