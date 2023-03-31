using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core.Stats;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Sprint : Ability
    {
        [SerializeField] private SoundEffectSO sfx_Sprinting;
        [SerializeField] private float AddToSpeed;
        [SerializeField] private float StaminaUses;

        private InputAction Input_Sprint;
        private Animator animator;

        public override void Start()
        {
            base.Start();
            GameObject Line = transform.Find("The line").gameObject;
            animator = Line.GetComponent<Animator>();
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

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);
            if(StatsManager.statsManager._PlayerStamina.Stamina <= 0) { return; }

            // Mechanics
            StatsManager.statsManager._PlayerSpeed.AddToSpeed(AddToSpeed);
            StatsManager.statsManager._PlayerStamina.startUseStaminaByTime(StaminaUses);

            // Effects
            ParticalsManager.particalsManager.Play("SpeedLines");
            animator.SetBool("Sprinting", true);
            sfx_Sprinting.Play();
        }

        public void OnCanceled(InputAction.CallbackContext obj)
        {
            // Mechanics
            StatsManager.statsManager._PlayerSpeed.BackToOriSpeed();
            StatsManager.statsManager._PlayerStamina.stopUseStaminaByTime();


            // Effects
            ParticalsManager.particalsManager.Stop("SpeedLines");
            animator.SetBool("Sprinting", false);
        }
    }
}
