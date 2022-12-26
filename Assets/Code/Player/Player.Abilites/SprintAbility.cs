using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class SprintAbility : Ability
    {
        private InputAction Input_Sprint;

        [SerializeField] private float AddToSpeed;

        private void OnEnable()
        {
            Input_Sprint = InputManager.inputActions.Player.Move_Sprint;

            Input_Sprint.performed += Cast;
            Input_Sprint.canceled += Disactivate;
        }

        public override void Cast(InputAction.CallbackContext obj)
        {
            PlayerMovement.backToNormalSpeed = false;
            PlayerMovement.theTrueSpeed += AddToSpeed;
        }

        public void Disactivate(InputAction.CallbackContext obj)
        {
            PlayerMovement.backToNormalSpeed = true;
        }
    }
}
