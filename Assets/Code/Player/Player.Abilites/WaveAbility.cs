using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Enemy;

namespace SpaceGame.Player.Abilites
{
    public class WaveAbility : Ability
    {
        private InputAction Input_Flip;

        [SerializeField] private float attackRange;
        [SerializeField] private int waveAttackDamege = 20;
        [SerializeField] private LayerMask enemyLayer;
        private Animator waveAnimator;
        private Transform player;

        private void Awake()
        {
            GameObject fire = GameObject.Find("Fire");
            waveAnimator = fire.GetComponent<Animator>();
            player = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            Input_Flip = InputManager.inputActions.Player.Attack_Wave;

            Input_Flip.performed += Cast;
        }

        public override void Cast(InputAction.CallbackContext obj)
        {

            if (GameManager.gameManager._PlayerStamina.Stamina > 0 && waveAnimator.GetBool("Do waveAttack") == false)
            {
                StartCoroutine(Flip());
            }
        }

        private IEnumerator Flip()
        {
            GameManager.gameManager._PlayerHealth.InDefence = true;
            waveAnimator.SetBool("Do waveAttack", true);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(waveAttackDamege);
            }

            yield return new WaitForSeconds(1f);
            GameManager.gameManager._PlayerHealth.InDefence = false;
        }

    }
}