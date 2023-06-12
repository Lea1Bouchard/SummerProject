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
              
                if (Physics.Raycast(solid.point + initiator.transform.forward * 5, initiator.transform.position - solid.point, hitInfo: out exit))
                    for (int x = 0; x <= 5; x++)
                    {
                        
                        if (exit.collider.gameObject.GetComponent<Characters>() != null)
                        {
                            if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter)
                            {
                                break;
                            }

                        }
                        Physics.Raycast(exit.point, solid.point, maxDistance: 20, hitInfo: out exit);
                    }
                
                if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter)
                {
                    Vector3 blinkExit;

                    blinkExit = exit.point + (initiator.transform.forward);

                    Physics.Raycast(exit.point + new Vector3(0, 1, 0), blinkExit - exit.point, out safeDistance, maxDistance: 2);
                    Debug.Log("Safedistance hit : " + safeDistance.point);
                    Debug.DrawRay(exit.point + new Vector3(0, 1, 0), blinkExit - exit.point, Color.yellow, 100);
                    if (safeDistance.point == Vector3.zero)
                    {
                        controller.enabled = false;
                        initiator.gameObject.transform.position = blinkExit;
                        controller.enabled = true;
                    }

                }
                

            }

            Debug.Log("Hit : " + solid.point);
        }

        abilityCooldownClass.Initialize(this);
    }

    private void Teleport()
    {
        initiator.SetTarget(GameObject.FindGameObjectWithTag("TeleportTarget"));

        abilityCooldownClass.Initialize(this);
    }

}
