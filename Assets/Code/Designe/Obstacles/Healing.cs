using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Core;

namespace SpaceGame.Designe.Obstacles
{
    public class Healing : MonoBehaviour
    {
        [SerializeField] private int healAmount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.gameManager._PlayerHealth.HealUnit(healAmount);
            }
        }
    }
}