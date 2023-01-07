using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Core.Cooldown;
using SpaceGame.Core.ObjectPooling;
using System.Collections;

namespace SpaceGame.Player
{
    public class Player_Attack : MonoBehaviour, IHasCooldown
    {
        private InputAction Input_Shoot;
        private InputAction Input_ChangShootingPos;

        [Header("For Cooldown")]
        [SerializeField] private System_Cooldown cooldownSystem;
        [SerializeField] private int id;
        [SerializeField] private float cooldownDuration = 1;

        public int Id => id;
        public float CooldownDuration => cooldownDuration;

        [Header("ForSpawning")]
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int preSpawn;
        public static ObjectPool<PoolObject> objectPool;

        [Header ("For Firing")]
        [SerializeField] private SoundEffectSO sfx_Shooting;
        [SerializeField] private Animator ShootingAnim;
        [SerializeField] private Transform firePoint;

        private Player_Movement playerMovement;

        #region The start

        private void Awake()
        {
            //create pool instance with prefab reference and pull and push actions
            objectPool = new ObjectPool<PoolObject>(objectPrefab, preSpawn);
        }

        private void Start()
        {
            playerMovement = GetComponent<Player_Movement>();
            sfx_Shooting.Prepare();
        }

        private void OnEnable()
        {
            Input_Shoot = InputManager.inputActions.Player.Attack_Shoot;
            Input_ChangShootingPos = InputManager.inputActions.Player.Attack_Shoot_ChangPos;

            Input_Shoot.performed += Shoot;
            Input_ChangShootingPos.performed += ChangePos;
        }
        #endregion

        void Update()
        {
            ShootingAnim.SetFloat("Horizontal", playerMovement.movement.x);
            ShootingAnim.SetFloat("Vertical", playerMovement.movement.y);
        }

        private void ChangePos(InputAction.CallbackContext obj)
        {
            ShootingAnim.SetBool("Switch", !ShootingAnim.GetBool("Switch"));
        }

        private void Shoot(InputAction.CallbackContext obj)
        {
            if (cooldownSystem.IsOnCooldown(id)) { return; }

            ShootingAnim.SetBool("IsShooting", true);
            sfx_Shooting.Play();

            objectPool.Pull(firePoint.position, firePoint.rotation);

            cooldownSystem.PutOnCooldown(this);
            StartCoroutine(IsShootingFalse());
        }

        private IEnumerator IsShootingFalse()
        {
            yield return new WaitForSeconds(0.2f);

            ShootingAnim.SetBool("IsShooting", false);
        }

    }
}
