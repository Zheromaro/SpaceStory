using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float thrust = 5;
    [SerializeField] private int damage = 40;
    [SerializeField] private bool dontDie = false;
    [SerializeField] private GameObject deathEffect;

    private ObjectPooler objectPooler;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isAlive;
    private bool once = true;
    private int startHealth;

    public UnitHealth _EnemyHealth = new UnitHealth(1, 1);

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;

        startHealth = _EnemyHealth.Health;
    }

    private void Update()
    {
        if (_EnemyHealth.Health < startHealth && once)
        {
            Vector2 difference = transform.position - GameManager.gameManager._PlayerHealth.Target.transform.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
            once = false;
        }
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
            Vector2 difference = transform.position - hitInfo.transform.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
        }

        if (_EnemyHealth.Health <= 0 && dontDie == false)
        {
            StartCoroutine(Die());
        }

    }

    private IEnumerator Die()
    {
        isAlive = false;
        animator.Play("DeathEffect");
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
        objectPooler.SpawnFromPool("dropLoot", transform.position + new Vector3(Random.Range(0, 3), Random.Range(0, 3)), Quaternion.identity);

        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damege)
    {
        _EnemyHealth.DmgUnit(damege);
    }

}
