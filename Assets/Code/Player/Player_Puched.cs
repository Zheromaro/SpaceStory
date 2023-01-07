using UnityEngine;
using SpaceGame.Core;

namespace SpaceGame.Player
{
    public class Player_Puched : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dust;
        [SerializeField] private float thrust = 5;
        private Rigidbody2D rb;
        private int startHealth;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            dust.Play();

            if (collision.CompareTag("Enemy"))
            {
                if (GameManager.gameManager._PlayerHealth.Health < startHealth)
                {
                    startHealth = GameManager.gameManager._PlayerHealth.Health;
                    Vector2 difference = transform.position - collision.transform.position;
                    difference = difference.normalized * thrust;
                    rb.AddForce(difference, ForceMode2D.Impulse);
                }
            }
        }
    }
}
