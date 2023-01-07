using System.Collections;
using UnityEngine;
using SpaceGame.Core;
using SpaceGame.NotImportant;

namespace SpaceGame.Enemy
{
    public class EnemyHealth: MonoBehaviour, IDamagable
    {
        [HideInInspector] public float thrust = 5;
        [HideInInspector] public int damage = 40;
        [HideInInspector] public bool doKnockBack = true;
        [HideInInspector] public GameObject deathEffect;
        [HideInInspector] public bool isAlive;

        private Animator animator;
        private Rigidbody2D rb;
        private int startHealth;

        public UnitHealth _EnemyHealth = new UnitHealth(1, 1);

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            isAlive = true;

            startHealth = _EnemyHealth.Health;
        }

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.CompareTag("Player") && isAlive)
            {
                isAlive = false;
                GameManager.gameManager._PlayerHealth.DmgUnit(damage);
                _EnemyHealth.DmgUnit(damage);
            }

            if (_EnemyHealth.Health < startHealth)
            {
                KnockBack(transform, hitInfo.transform);
            }

            if (_EnemyHealth.Health <= 0)
            {
                StartCoroutine(Die());
            }

        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        private IEnumerator Die()
        {
            isAlive = false;
            animator.Play("DeathEffect");
            yield return new WaitForSeconds(1);

            Destroy(gameObject);
            //objectPooler.SpawnFromPool("dropLoot", transform.position + new Vector3(Random.Range(0, 3), Random.Range(0, 3)), Quaternion.identity);

            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        private void KnockBack(Transform me, Transform him)
        {
            Vector2 difference = me.position - him.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
        }

        public void TakeDamage(int damege)
        {
            _EnemyHealth.DmgUnit(damege);
        }

    }
}
