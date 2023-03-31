using SpaceGame.Core.GameEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Designe
{
    public class CheckForPlayer : MonoBehaviour
    {
        [SerializeField] private VoidEvent onPlayerIn;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                onPlayerIn.Raise();
            }
        }
    }
}
