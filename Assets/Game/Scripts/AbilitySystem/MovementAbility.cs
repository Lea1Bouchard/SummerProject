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
        animator = ini.GetComponent<Animator>();
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


        RaycastHit solid;
        RaycastHit exit;
        Vector3 destination = initiator.transform.position + initiator.transform.forward * distance;


        Debug.DrawRay(initiator.transform.position + new Vector3(0, 1, 0), destination, Color.green, 100);
        if (Physics.Linecast(initiator.transform.position, destination, out solid))
        {
            Characters hitCharacter = solid.collider.gameObject.GetComponent<Characters>();
            if (hitCharacter != null)
            {
                if (Physics.Linecast(solid.point, destination + Vector3.one * 10, out exit))
                {

                    Debug.Log("Enter point : " + solid.point);
                    Debug.Log("Exit point : " + exit.point);

                    Vector3 blinkExit;

                    blinkExit = exit.point + (initiator.transform.forward * 2);

                    controller.enabled = false;
                    initiator.gameObject.transform.position = blinkExit;
                    controller.enabled = true;


                }
            }
        }

        abilityCooldownClass.Initialize(this);
    }

    private void Teleport()
    {
        initiator.SetTarget(GameObject.FindGameObjectWithTag("TeleportTarget"));

        abilityCooldownClass.Initialize(this);
    }


}
