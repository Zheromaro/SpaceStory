using SpaceGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Designe.Tile
{
    public class EffectMovement : MonoBehaviour
    {
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
