using SpaceGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Designe.Tile
{
    public class EffectDirection : MonoBehaviour
    {
        [SerializeField] private bool up;
        [SerializeField] private bool down;
        [SerializeField] private bool right;
        [SerializeField] private bool left;

        private int speedEffect;
        private bool Direction;

        private void Start()
        {
            if (up)
            {
                
            }
            else if (down)
            {

            }
            else if (right)
            {

            }
            else if (left)
            {

            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player_Movement player = collision.GetComponent<Player_Movement>();

            if (player == null)
                return;

            player.freeMovement = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Player_Movement player = collision.GetComponent<Player_Movement>();

            if (player == null)
                return;

            player.freeMovement = false;
        }
    }
}
