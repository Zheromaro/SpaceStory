using UnityEngine;
using SpaceGame.Core;

namespace SpaceGame.Random
{
    public class itemFollow : MonoBehaviour
    {
        [SerializeField] private float Modifier;
        [SerializeField] private bool heal;
        [SerializeField] private bool stamina;

        Vector2 _velocity = Vector2.zero;
        private Transform target;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            transform.position = Vector2.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Modifier);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (stamina == true)
                {
                    GameManager.gameManager._PlayerStamina.RegenStamina(33f);
                }

                if (heal == true)
                {
                    GameManager.gameManager._PlayerHealth.HealUnit(20);
                }
                gameObject.SetActive(false);
            }
        }
    }
}