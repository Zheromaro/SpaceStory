using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilites/Wave")]
public class WaveAbility : Ability
{
    [SerializeField] private float attackRange;
    [SerializeField] private int waveAttackDamege = 20;
    [SerializeField] private LayerMask enemyLayer;
    private Animator waveAnimator;
    private Transform player;

    public override void Activate(GameObject parent)
    {
        waveAnimator = parent.transform.Find("Fire").GetComponent<Animator>();
        player = parent.transform;

        if (GameManager.gameManager._PlayerStamina.Stamina > 0 && waveAnimator.GetBool("Do waveAttack") == false)
        {
            GameManager.gameManager.runCoroutine(WaveAttack());
        }
    }

    private IEnumerator WaveAttack()
    {
        GameManager.gameManager._PlayerHealth.InDefence = true;
        waveAnimator.SetBool("Do waveAttack", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<HealthEnemy>().TakeDamage(waveAttackDamege);
        }

        yield return new WaitForSeconds(1f);
        GameManager.gameManager._PlayerHealth.InDefence = false;
    }

}
