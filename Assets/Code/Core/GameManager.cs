using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceGame.Core.GameEvent;

namespace SpaceGame.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager { get; private set; }

        public UnitHealth _PlayerHealth = new UnitHealth(100, 100);
        public UnitStamina _PlayerStamina = new UnitStamina(99f, 99f);

        [SerializeField] private VoidEvent onStaminaChanged;

        private void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                gameManager = this;
            }
        }

        private void OnEnable()
        {
            _PlayerStamina.onStaminaChanged = onStaminaChanged;
            SceneManager.sceneLoaded += restart;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= restart;
        }

        private void restart(Scene scene, LoadSceneMode loadSceneMode)
        {
            _PlayerHealth.Health = 100;
        }
    }
}