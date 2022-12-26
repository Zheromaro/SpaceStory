using UnityEngine;

namespace SpaceGame.Random
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private int damage = 40;
        [SerializeField] private Rigidbody2D rb;


        private void FixedUpdate()
        {
            rb.velocity = transform.right * speed;
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            IDamagable enemy = hitInfo.GetComponent<IDamagable>();
            IDontDestroy dontDestroy = hitInfo.GetComponent<IDontDestroy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                gameObject.SetActive(false);
            }

            if (dontDestroy == null)
            {
                gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D hitInfo)
        {
            IDontDestroy dontDestroy = hitInfo.GetComponent<IDontDestroy>();

            if (dontDestroy == null)
            {
                gameObject.SetActive(false);
            }
        }

    }
}