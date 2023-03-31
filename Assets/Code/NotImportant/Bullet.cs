using UnityEngine;
using SpaceGame.Enemy;

namespace SpaceGame.NotImportant
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
            Iuntouchable keepBullet = hitInfo.GetComponent<Iuntouchable>();

            if (keepBullet != null)
                return;

            EnemyHealth enemy;

            if (enemy = hitInfo.GetComponent<EnemyHealth>())
            {
                enemy.GetHit(damage, gameObject);
                gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D hitInfo)
        {
            Iuntouchable keepBullet = hitInfo.GetComponent<Iuntouchable>();

            if (keepBullet == null)
            {
                gameObject.SetActive(false);
            }
        }

    }
}