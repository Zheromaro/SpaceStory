using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilites/Flip")]
public class FlipAbility : Ability
{
    public override void Activate(GameObject parent)
    {
        PlayerMovement player = parent.GetComponent<PlayerMovement>();
        player.Manual = player.Manual * -1f;
    }

}
