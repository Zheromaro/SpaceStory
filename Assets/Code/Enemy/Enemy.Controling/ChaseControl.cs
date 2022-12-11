using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Logic;

namespace Enemy.Controling
{
    public class ChaseControl : MonoBehaviour
    {
        [SerializeField] private ChaseEnemy[] enemyArray;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (ChaseEnemy enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                foreach (ChaseEnemy enemy in enemyArray)
                {
                    enemy.chase = false;
                }
            }
        }

    }
}
