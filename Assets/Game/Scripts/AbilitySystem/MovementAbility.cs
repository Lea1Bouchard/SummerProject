using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MovementAbility")]
public class MovementAbility : Ability
{
    public float distance;
    public bool rendersImmortal;
    public float speed;
    public float timeImmortal;
    public GameObject target;
    private CharacterController controller;
    public bool isTeleport;

    //TODO : Render player immortal **MIGHT PUT THIS IN PLAYER SCRIPT

    public override void Initialize(Characters ini)
    {
        initiator = ini;
        isActive = false;
        controller = initiator.GetComponent<CharacterController>();
        abilityCooldownClass = initiator.gameObject.AddComponent<AbilityCooldown>();
    }

    public override void TriggerAbility()
    {
        if (CheckState())
        {
            isActive = true;
            if (!isTeleport)
                Move();
            else
                Teleport();

            Animate();
        }
    }

    private void Move()
    {
        controller.Move(initiator.transform.forward * (speed * Time.deltaTime));

        abilityCooldownClass.Initialize(this);
    }

    private void Teleport()
    {
        initiator.SetTarget(GameObject.FindGameObjectWithTag("TeleportTarget"));

        abilityCooldownClass.Initialize(this);
    }


}
