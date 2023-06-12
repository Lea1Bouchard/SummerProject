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


        RaycastHit solid = new RaycastHit();
        RaycastHit exit = new RaycastHit();
        RaycastHit safeDistance = new RaycastHit();

        Debug.DrawRay(initiator.transform.position + new Vector3(0, 1, 0), initiator.transform.forward, Color.green, 100);
        if (Physics.Raycast(initiator.transform.position, initiator.transform.forward, maxDistance: distance, hitInfo: out solid))
        {
            Characters hitCharacter = solid.collider.gameObject.GetComponent<Characters>();
            if (hitCharacter != null)
            {
                Debug.Log("Character : " + hitCharacter.name);
                //AFTER THIS IS WHERE IT'S NOT WORKING
                Debug.DrawRay((solid.point + initiator.transform.forward * 5) + new Vector3(0, 1, 0), initiator.transform.position - solid.point, Color.red, 100);
                Debug.DrawRay(solid.point + new Vector3(0, 1.5f, 0), initiator.transform.position - solid.point , Color.cyan, 100);
                if (Physics.Raycast(solid.point + initiator.transform.forward * 5, initiator.transform.position - solid.point, hitInfo: out exit))
                    for (int x = 0; x <= 5; x++)
                    {
                        Debug.Log("Point : " + exit.point);
                        if (exit.collider.gameObject.GetComponent<Characters>() != null)
                        {
                            if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter)
                            {
                                Debug.Log("Enter point : " + solid.point);
                                Debug.Log("Exit point : " + exit.point);
                                break;
                            }

                        }
                        Physics.Raycast(exit.point, solid.point, maxDistance: 20, hitInfo: out exit);
                    }
                
                if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter)
                {
                    Vector3 blinkExit;

                    blinkExit = exit.point + (initiator.transform.forward);

                    Physics.Raycast(exit.point, blinkExit - exit.point, out safeDistance, maxDistance: 2);
                    Debug.Log(safeDistance.point);
                    Debug.DrawRay(exit.point, exit.point - blinkExit, Color.yellow, 100);
                    if (safeDistance.point == Vector3.zero)
                    {
                        controller.enabled = false;
                        initiator.gameObject.transform.position = blinkExit;
                        controller.enabled = true;
                    }

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

    /* if (Physics.Linecast(initiator.transform.position, destination, out solid))
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
        }*/
}
