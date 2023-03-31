using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core.Cooldown;

namespace SpaceGame.Player.Abilites
{
    public abstract class Ability : MonoBehaviour, IHasCooldown
    {
        [Header("For Cooldown")]
        private System_Cooldown cooldownSystem;
        [SerializeField] private int id;
        [SerializeField] private float cooldownDuration = 1;

        public int Id => id;
        public float CooldownDuration => cooldownDuration;

        public virtual void Start()
        {
            cooldownSystem = GetComponent<System_Cooldown>();
        }

        public virtual void OnPerformed(InputAction.CallbackContext obj)
        {
            if (cooldownSystem.IsOnCooldown(id)) { return; }
            cooldownSystem.PutOnCooldown(this);
        }
    }
}