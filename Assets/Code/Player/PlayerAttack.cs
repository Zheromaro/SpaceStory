using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;
using SpaceGame.Core.ObjectPooling;

namespace SpaceGame.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private InputAction Input_Shoot;
        private InputAction Input_ChangShootingPos;

        [Header("For Spawning")]
        public static ObjectPool<PoolObject> objectPool;
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] private int preSpawn;

        [Header ("For Firing")]
        [SerializeField] private Animator ShootingAnim;
        [SerializeField] private Transform firePoint;

        private PlayerMovement playerMovement;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();

            //create pool instance with prefab reference and pull and push actions
            objectPool = new ObjectPool<PoolObject>(objectPrefab, CallOnPull, CallOnPull, preSpawn);
        }

        private void CallOnPull(PoolObject poolObject)
        {
            ShootingAnim.SetBool("IsShooting", true);
        }

        private void CallOnPuch(PoolObject poolObject)
        {
            // Nothing yet
        }

        private void OnEnable()
        {
            Input_Shoot = InputManager.inputActions.Player.Attack_Shoot;
            Input_ChangShootingPos = InputManager.inputActions.Player.Attack_Shoot_ChangPos;

            Input_Shoot.performed += Shoot;
            Input_ChangShootingPos.performed += ChangePos;
        }

        private void ChangePos(InputAction.CallbackContext obj)
        {
            ShootingAnim.SetBool("Switch", !ShootingAnim.GetBool("Switch"));
        }

        private void Shoot(InputAction.CallbackContext obj)
        {
            objectPool.Pull(firePoint.position, firePoint.rotation);
        }

        void Update()
        {
            ShootingAnim.SetFloat("Horizontal", playerMovement.movement.x);
            ShootingAnim.SetFloat("Vertical", playerMovement.movement.y);
        }

    }
}
