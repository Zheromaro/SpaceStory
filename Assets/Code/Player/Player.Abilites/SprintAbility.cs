using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Abilites
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Abilites/Sprint")]
    public class SprintAbility : Ability
    {
        [SerializeField] private float AddToSpeed;
        private bool once = false;

        public override void Activate(GameObject parent)
        {
            Debug.Log("Start");
            PlayerMovement.backToNormalSpeed = false;
            if (once == false)
            {
                PlayerMovement.theTrueSpeed += AddToSpeed;
                once = true;
            }
        }

        public override void Disactivate(GameObject parent)
        {
            Debug.Log("End");
            PlayerMovement.backToNormalSpeed = true;
        }
    }
}
