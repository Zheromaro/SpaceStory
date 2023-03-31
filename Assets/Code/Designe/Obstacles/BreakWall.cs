using System.Collections;
using UnityEngine;
using SpaceGame.Core.Stats;

namespace SpaceGame.Designe.Obstacles
{
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
            if(StatsManager.statsManager._PlayerSpeed.isSprinting == true)
            {
                StartCoroutine(DeathEffect());
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (StatsManager.statsManager._PlayerSpeed.isSprinting == true)
            {
                StartCoroutine(DeathEffect());
            }
        }

        private IEnumerator DeathEffect()
        {
            boxCollider.enabled = false;
            animator.Play("Destroy");
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(gameObject);
        }
    }
}