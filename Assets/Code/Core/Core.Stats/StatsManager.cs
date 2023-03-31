using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceGame.Core.GameEvent;
using SpaceGame.Core.Cooldown;

namespace SpaceGame.Core.Stats
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager statsManager { get; private set; }

        public UnitHealth _PlayerHealth = new UnitHealth(5, 5, 1);
        public UnitStamina _PlayerStamina = new UnitStamina(99f, 99f);
        public UnitSpeed _PlayerSpeed = new UnitSpeed(1.2f);

        [Header("Events")]
        // Health
        [SerializeField] private VoidEvent onPlayerDied;
        [SerializeField] private VoidEvent onPlayerHealed;
        [SerializeField] private GameObjectEvent onPlayerDamaged;

        // Stamina
        [SerializeField] private VoidEvent onStaminaChange;
        [SerializeField] private VoidEvent onStaminaEnded;
        [SerializeField] private VoidEvent onStaminaStabel;

        [Header("Cooldown")]
        private System_Cooldown cooldownSystem;
        [SerializeField] private int id;

        private void Awake()
        {
            if (statsManager != null && statsManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                statsManager = this;
            }

            cooldownSystem = transform.GetComponent<System_Cooldown>();
        }

        private void OnEnable()
        {
            // Events
            _PlayerHealth._onPlayerDied = onPlayerDied;
            _PlayerHealth._onPlayerHealed = onPlayerHealed;
            _PlayerHealth._onPlayerDamaged = onPlayerDamaged;
            _PlayerStamina._onStaminaChange = onStaminaChange;
            _PlayerStamina._onStaminaEnded = onStaminaEnded;
            _PlayerStamina._onStaminaStabel = onStaminaStabel;

            // Cooldown
            _PlayerHealth._cooldownSystem = cooldownSystem;
            _PlayerHealth._id = id;

            SceneManager.sceneLoaded += restart;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= restart;
        }

        private void restart(Scene scene, LoadSceneMode loadSceneMode)
        {
            _PlayerHealth.Health = _PlayerHealth.MaxHealth;
        }

    }
}