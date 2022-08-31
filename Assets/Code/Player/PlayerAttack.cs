using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float staminaUsed;
    [SerializeField] private float attackRange;
    [SerializeField] private int waveAttackDamege = 20;
    [SerializeField] private LayerMask enemyLayer;
    private Animator waveAnimator;
    private Transform firePoint;
    private ObjectPooler objectPooler;

    void Start()
    { 
        firePoint = transform.Find("FirePoint").GetComponent<Transform>();
        waveAnimator = transform.Find("Wave Atack").GetComponent<Animator>();
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameManager.gameManager._PlayerStamina.Stamina > 0)
            {
                GameManager.gameManager._PlayerStamina.UseStamina(staminaUsed);
                StartCoroutine(WaveAttack());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Shoot()
    {
        objectPooler.SpawnFromPool("bullet", firePoint.position, firePoint.rotation);
    }

    private IEnumerator WaveAttack()
    {
        GameManager.gameManager._PlayerHealth.InDefence = true;
        waveAnimator.Play("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HealthEnemy>().TakeDamage(waveAttackDamege);
        }

        yield return new WaitForSeconds(1f);
        GameManager.gameManager._PlayerHealth.InDefence = false;
    }

}
