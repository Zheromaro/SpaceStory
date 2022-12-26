using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class FlipAbility : Ability
    {
        private InputAction Input_Flip;

        private PlayerMovement player;

        private void Awake()
        {
            player = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            Input_Flip = InputManager.inputActions.Player.Move_Flip;

            Input_Flip.performed += Cast;
        }

        public override void Cast(InputAction.CallbackContext obj)
        {
            player.Manual = player.Manual * -1f;
        }

    }
}
