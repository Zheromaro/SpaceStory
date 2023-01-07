using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Enemy;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Wave : Ability
    {
        private InputAction Input_Flip;

        [Header("For TheAttack")]
        [SerializeField] private float ShildingTime = 1;
        [SerializeField] private float attackRange;
        [SerializeField] private float StaminaUses;
        [SerializeField] private int waveAttackDamege = 20;
        [SerializeField] private LayerMask enemyLayer;
        private Animator waveAnimator;
        private Transform player;

        public override void Start()
        {
            base.Start();

            GameObject Line = transform.Find("The line").gameObject;
            waveAnimator = Line.GetComponent<Animator>();
            player = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            Input_Flip = InputManager.inputActions.Player.Attack_Wave;

            Input_Flip.performed += OnPerformed;
        }

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);
            if (GameManager.gameManager._PlayerStamina.Stamina > 0 && waveAnimator.GetBool("Do waveAttack") == false)
            {
                StartCoroutine(Flip());
                GameManager.gameManager._PlayerStamina.UseStamina(StaminaUses);
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
            yield return new WaitForSeconds(ShildingTime);
            GameManager.gameManager._PlayerHealth.InDefence = false;
        }

    }
}