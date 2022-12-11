using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    using Abilites;

    public class PlayerAbilitys : MonoBehaviour
    {
        [SerializeField] private Ability[] abilites;
        [HideInInspector] public bool inSpeedUp = false;

        void Update()
        {
            foreach (var ability in abilites)
            {
                ability.DoAbility(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("SpeedUp"))
            {
                inSpeedUp = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("SpeedUp"))
            {
                inSpeedUp = false;
            }
        }

    }
}
