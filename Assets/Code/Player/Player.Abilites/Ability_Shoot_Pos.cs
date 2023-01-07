using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core.ObjectPooling;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Shoot_Pos : Ability
    {
        private InputAction Input_ChangShootingPos;

        [Header("For Changing FiringPos")]
        [SerializeField] private Animator ShootingAnim;
        [SerializeField] private Transform firePoint;

        private Player_Movement playerMovement;

        public override void Start()
        {
            base.Start();
            playerMovement = GetComponent<Player_Movement>();
        }

        private void OnEnable()
        {
            Input_ChangShootingPos = InputManager.inputActions.Player.Attack_Shoot_ChangPos;

            Input_ChangShootingPos.performed += OnPerformed;
        }

        private void OnDisable()
        {
            Input_ChangShootingPos.performed -= OnPerformed;
        }

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);
            ShootingAnim.SetBool("Switch", !ShootingAnim.GetBool("Switch"));
        }
    }
}
