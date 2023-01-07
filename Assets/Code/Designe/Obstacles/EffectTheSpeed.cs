using System.Collections;
using UnityEngine;
using SpaceGame.Player;
using SpaceGame.NotImportant;

namespace SpaceGame.Designe.Obstacles
{
    public class EffectTheSpeed : MonoBehaviour, IDontDestroy
    {
        [SerializeField] private float AddToSpeed;
        private static float AreInColision = 0;

        private Player_Movement player_movement;
        private float theTrueSpeed;

        private void Awake()
        {
            player_movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
            theTrueSpeed = player_movement.speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (AreInColision < 1)
                {
                    player_movement.speed = theTrueSpeed + AddToSpeed;
                }

                AreInColision += 1;
            }
        }

        private IEnumerator OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AreInColision -= 1;

                if (AreInColision < 0)
                {
                    AreInColision = 0;
                }

                if (AreInColision == 0)
                {
                    yield return new WaitForSeconds(0.1f);
                    player_movement.speed = theTrueSpeed;
                }
            }
        }

    }
}