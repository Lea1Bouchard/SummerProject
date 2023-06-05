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
            //animate();
            Move();
        }
    }

    private void Move()
    {
        if (rendersImmortal)
        {
            //TODO : Render player immortal
        }

        SetTarget(GameObject.FindGameObjectWithTag("TeleportTarget"));

        if (target != null)
        {

            controller.enabled = false;

            initiator.transform.position = target.transform.position;

            controller.enabled = true;

        }
        else
        {
            controller.Move(initiator.transform.forward * (speed * Time.deltaTime));
        }

        abilityCooldownClass.Initialize(this);
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }

}
