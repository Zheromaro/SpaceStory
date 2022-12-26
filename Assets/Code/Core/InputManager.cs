using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SpaceGame.Core
{
    public class InputManager : MonoBehaviour
    {
        public static PlayerInput inputActions;
        public static event Action<InputActionMap> actionMapChange;

        private void Awake()
        {
            inputActions = new PlayerInput();

            // start with the Important controller enabled
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                ToggeleActionMap(inputActions.UI);
            }
            else
            {
                ToggeleActionMap(inputActions.Player);
            }
        }

        public static void ToggeleActionMap(InputActionMap actionMap)
        {
            if (actionMap.enabled)
                return;

            inputActions.Disable();
            actionMapChange?.Invoke(actionMap);
            actionMap.Enable();
        }

    }
}
