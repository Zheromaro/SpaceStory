using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Core;

namespace SpaceGame.Enemy
{
    public class Health_Obstacle : EnemyHealth
    {
        public void OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (hitInfo.CompareTag("Player"))
            {
                isAlive = false;
                GameManager.gameManager._PlayerHealth.DmgUnit(damage);
                _EnemyHealth.DmgUnit(damage);
            }
        }
    }
}
