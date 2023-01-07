using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.Player.Abilites
{
    public class Ability_Flip : Ability
    {
        private InputAction Input_Flip;

        private Player_Movement player;

        private void Awake()
        {
            player = GetComponent<Player_Movement>();
        }

        private void OnEnable()
        {
            Input_Flip = InputManager.inputActions.Player.Move_Flip;

            Input_Flip.performed += OnPerformed;
        }

        public override void OnPerformed(InputAction.CallbackContext obj)
        {
            base.OnPerformed(obj);  
            player.Manual = player.Manual * -1f;
        }

    }
}
