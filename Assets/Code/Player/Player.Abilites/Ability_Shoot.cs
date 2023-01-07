using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core.ObjectPooling;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Shoot : Ability
    {
        private InputAction Input_Shoot;

        [Header("ForSpawning")]
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int preSpawn;
        public static ObjectPool<PoolObject> objectPool;

        [Header("For Firing")]
        [SerializeField] private SoundEffectSO sfx_Shooting;
        [SerializeField] private Animator ShootingAnim;
        [SerializeField] private Transform firePoint;

        private Player_Movement playerMovement;

        private void Awake()
        {
            //create pool instance with prefab reference and pull and push actions
            objectPool = new ObjectPool<PoolObject>(objectPrefab, preSpawn);
        }

        public override void Start()
        {
            base.Start();
            playerMovement = GetComponent<Player_Movement>();
            sfx_Shooting.Prepare();
        }

        private void OnEnable()
        {
            Input_Shoot = InputManager.inputActions.Player.Attack_Shoot;

            Input_Shoot.performed += OnPerformed;
        }

        private void OnDisable()
        {
            Input_Shoot.performed -= OnPerformed;
        }

        void Update()
        {
            ShootingAnim.SetFloat("Horizontal", playerMovement.movement.x);
            ShootingAnim.SetFloat("Vertical", playerMovement.movement.y);
        }

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);

            ShootingAnim.SetBool("IsShooting", true);
            sfx_Shooting.Play();

            objectPool.Pull(firePoint.position, firePoint.rotation);
            StartCoroutine(IsShootingFalse());
        }

        private IEnumerator IsShootingFalse()
        {
            yield return new WaitForSeconds(0.2f);

            ShootingAnim.SetBool("IsShooting", false);
        }
    }
}
