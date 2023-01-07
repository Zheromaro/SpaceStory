using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Sprint : Ability
    {
        private InputAction Input_Sprint;
        private Animator animator;
        private Player_Movement player_movement;
        private float theTrueSpeed;
        private bool isSprinting = false;

        [SerializeField] private SoundEffectSO sfx_Sprinting;
        [SerializeField] private float AddToSpeed;
        [SerializeField] private float StaminaUses;

        public override void Start()
        {
            base.Start();
            GameObject Line = transform.Find("The line").gameObject;
            animator = Line.GetComponent<Animator>();
            player_movement = GetComponent<Player_Movement>();

            theTrueSpeed = player_movement.speed;
            sfx_Sprinting.Prepare();
        }

        private void OnEnable()
        {
            Input_Sprint = InputManager.inputActions.Player.Move_Sprint;

            Input_Sprint.performed += OnPerformed;
            Input_Sprint.canceled += OnCanceled;
        }

        private void OnDisable()
        {
            Input_Sprint.performed -= OnPerformed;
            Input_Sprint.canceled -= OnCanceled;
        }

        private void Update()
        {
            if(isSprinting == false) { return; }

            GameManager.gameManager._PlayerStamina.UseStaminaByTime(StaminaUses);
        }

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);
            if(GameManager.gameManager._PlayerStamina.Stamina <= 0) { return; }

            EffectManager.effectManager.Play("SpeedLines");

            isSprinting = true;
            animator.SetBool("Sprinting", true);
            sfx_Sprinting.Play();
            player_movement.speed = theTrueSpeed + AddToSpeed;
        }

        public void OnCanceled(InputAction.CallbackContext obj)
        {
            if (GameManager.gameManager._PlayerStamina.Stamina <= 0) { return; }

            EffectManager.effectManager.Stop("SpeedLines");

            isSprinting = false;
            animator.SetBool("Sprinting", false);
            player_movement.speed = theTrueSpeed;
        }
    }
}
