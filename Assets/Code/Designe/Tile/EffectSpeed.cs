using System.Collections;
using UnityEngine;
using SpaceGame.Core.Stats;
using SpaceGame.NotImportant;

namespace SpaceGame.Designe.Tile
{
    public class EffectSpeed : MonoBehaviour, Iuntouchable
    {
        [Range(-100, 100)]
        [SerializeField] private float Persantig;

        private static int i = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                i++;
                StatsManager.statsManager._PlayerSpeed.AddToSpeed(AddToSpeedBy());
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StatsManager.statsManager._PlayerSpeed.AddToSpeed(AddToSpeedBy());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                i--;
                if (i == 0)
                {
                    StatsManager.statsManager._PlayerSpeed.BackToOriSpeed();
                }
                else if (i < 0)
                {
                    i = 0;
                }
            }
        }

        private float AddToSpeedBy()
        {
            return Persantig * StatsManager.statsManager._PlayerSpeed.normalSpeed / 100;
        }

    }
}