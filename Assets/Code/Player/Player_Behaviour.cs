using System.Collections;
using UnityEngine;
using SpaceGame.Core;
using SpaceGame.Core.GameEvent;

namespace SpaceGame.Player
{
    public class Player_Behaviour : MonoBehaviour
    {
        [SerializeField] private VoidEvent onCharacterDied;
        [SerializeField] private VoidEvent onCharacterHealed;
        [SerializeField] private VoidEvent onCharacterDamaged;

        private float health;

        private void Start()
        {
            health = GameManager.gameManager._PlayerHealth.Health;
        }

        private void Update()
        {
            CheckingForHealth();
        }

        private void CheckingForHealth()
        {
            if (health > GameManager.gameManager._PlayerHealth.Health)
            {
                health = GameManager.gameManager._PlayerHealth.Health;
                onCharacterHealed.Raise();
            }
            else if (health < GameManager.gameManager._PlayerHealth.Health)
            {
                health = GameManager.gameManager._PlayerHealth.Health;
                onCharacterDamaged.Raise();
            }

            if (GameManager.gameManager._PlayerHealth.Health <= 0)
            {
                onCharacterDied.Raise();
                gameObject.SetActive(false);
            }
        }
    }
}