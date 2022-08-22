using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 40;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private bool dontDie = false;

    private ObjectPooler objectPooler;
    private Animator animator;
    private bool isAlive;

    public UnitStats _EnemyHealth = new UnitStats(1, 1);

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerBehaviour health = hitInfo.GetComponent<PlayerBehaviour>();
        if (health != null & isAlive)
        {
            GameManager.gameManager._PlayerHealth.DmgUnit(damage);
            _EnemyHealth.DmgUnit(damage);
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

    public void TakeDamage (int damege)
    {
        _EnemyHealth.DmgUnit(damege);
    }

}
