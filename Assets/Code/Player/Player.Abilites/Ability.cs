using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceGame.Player.Abilites
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract void Cast(InputAction.CallbackContext obj);
    }
}