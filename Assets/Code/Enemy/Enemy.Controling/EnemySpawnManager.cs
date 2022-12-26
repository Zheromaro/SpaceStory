using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Controling
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemyArray;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (GameObject enemy in enemyArray)
            {
                if (enemy == null)
                    return;
                enemy.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                foreach (GameObject enemy in enemyArray)
                {
                    enemy.SetActive(false);
                }
            }
        }
    }
}
