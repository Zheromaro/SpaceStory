using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Designe
{
    public abstract class CheckForPlayer : MonoBehaviour
    {
        //public bool touched = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //touched = true;
                Here();
            }
        }

        public abstract void Here();
    }
}
