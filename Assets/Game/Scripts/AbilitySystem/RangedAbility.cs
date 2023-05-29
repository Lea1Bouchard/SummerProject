using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/RangedAbility")]
public class RangedAbility : Ability
{
    public float range = 50f;
    public float speed = 10f;
    public GameObject projectile;

    public override void Initialize()
    {
        //Instantiate the projectile

        //Set projectile damage to ability damage
        //Set projectile range to ability range
        //Set projectile speed to ability speed

        //Set projectile owner to initiator

        //MABE :: Set material here?
        throw new System.NotImplementedException();
    }

    public override void TriggerAbility()
    {
        //TODO : shoot the projectile
        throw new System.NotImplementedException();
    }


}
