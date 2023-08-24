using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MovementAbility")]

//Class is used both for dodge and teleport (with projectile)
public class MovementAbility : Ability
{
    #region variables
    public float maxDistance;
    public bool rendersImmortal;
    public float speed;
    public float timeImmortal;
    public GameObject target;
    private CharacterController controller;
    public bool isTeleport;
    private Vector3 blinkExit;
    #endregion

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
            initiator.RenderImmortal(timeImmortal);
        }
    }
    //Dodge part
    private void Move()
    {
        //Verify if there is an enemy in the dodge trajectory
        //and teleports behind it if there is one
        if (EnemyStep())
        {
            GoTo();
        }
        else
        {
            if(initiator.Animator.GetBool("FreeFall"))
            {
                controller.Move(initiator.transform.forward * speed / 10);
            }
            else
            { 
                controller.Move(initiator.transform.forward * speed / 2);
            }
        }

        abilityCooldownClass.Initialize(this);
    }
    //Teleport part
    private void Teleport()
    {
        initiator.SetTeleportTarget(GameObject.FindGameObjectWithTag("TeleportTarget"));

        if (initiator.teleportTarget != null)
        {
            if (TeleportLocation() != Vector3.zero)
                GoTo();
        }

        abilityCooldownClass.Initialize(this);
    }

    //TODO : Might need to rework this later

    //Used to verify if there is an enemy 
    //and if it's possible to teleport behind it
    private bool EnemyStep()
    {
        RaycastHit solid = new RaycastHit();
        RaycastHit exit = new RaycastHit();
        RaycastHit safeDistance = new RaycastHit();
        bool foundExit = false;

        //Check if an enemy is in range
        if (Physics.Raycast(initiator.transform.position, initiator.transform.forward, maxDistance: maxDistance, hitInfo: out solid))
        {
            Characters hitCharacter = solid.collider.gameObject.GetComponent<Characters>();
            if (hitCharacter != null)
            {
                //Check the exit point of the previous raycast
                if (Physics.Raycast(solid.point + initiator.transform.forward * 5, initiator.transform.position - solid.point, hitInfo: out exit))
                    for (int x = 0; x <= 5; x++)
                    {
                        //Check if the exitpoint correspond to the character 
                        if (exit.collider.gameObject.GetComponent<Characters>() != null)
                        {
                            if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter)
                            {
                                foundExit = true;
                                break;
                            }
                        }
                        Physics.Raycast(exit.point, solid.point, maxDistance: 20, hitInfo: out exit);
                    }
                if (!foundExit)
                    return false;

                //Checking if the point where the character teleports is occupied
                blinkExit = exit.point + (initiator.transform.forward);

                Physics.Raycast(exit.point + new Vector3(0, 1, 0), blinkExit - exit.point, out safeDistance, maxDistance: 2);

                if (safeDistance.point == Vector3.zero)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Used during the teleport to verify if teleporting
    // is doable and where the player should land
    private Vector3 TeleportLocation()
    {

        RaycastHit solid = new RaycastHit();
        RaycastHit exit = new RaycastHit();
        bool foundExit = false;
        Vector3 direction = (initiator.teleportTarget.transform.position - initiator.transform.position).normalized;

        //Check if an enemy is in range
        if (Physics.Linecast(initiator.transform.position, initiator.teleportTarget.transform.position, hitInfo: out solid))
        {
            Characters hitCharacter = solid.collider.gameObject.GetComponent<Characters>();
            if (hitCharacter != null)
            {
                //Check the exit point of the previous raycast
                if (Physics.Raycast(solid.point + direction * 5, -direction, hitInfo: out exit))
                {

                    for (int x = 0; x <= 5; x++)
                    {
                        //Check if the exitpoint correspond to the character 
                        if (exit.collider.gameObject.GetComponent<Characters>() != null)
                        {
                            if (exit.collider.gameObject.GetComponent<Characters>() == hitCharacter && exit.point.y >= initiator.transform.position.y)
                            {
                                foundExit = true;
                                break;
                            }
                        }
                        Physics.Raycast(exit.point, -direction, maxDistance: 20, hitInfo: out exit);
                    }
                }

                return SafeDistance(exit, solid, direction, foundExit);
            }
        }
        //Try to teleport to the weapon (not in an enemy)
        else
        {
            return FrontSafeDistanceCheck(initiator.teleportTarget.transform.position, direction);
        }
        return Vector3.zero;
    }

    //Verify if there is enough space for the player to teleport behind the enemy
    private Vector3 SafeDistance(RaycastHit exit, RaycastHit solid, Vector3 direction, bool foundExit)
    {
        RaycastHit safeDistance = new RaycastHit();

        if (foundExit)
        {
            blinkExit = exit.point + (direction * 2);
            Physics.Raycast(exit.point, blinkExit - exit.point, out safeDistance, maxDistance: 2);

            if (safeDistance.point == Vector3.zero)
            {
                return blinkExit;
            }
        }
        //This part won't be reached if the backside exit point works
        return FrontSafeDistanceCheck(solid.point, direction);
    }

    //Verify if there is enough space for the player to teleport in fron of the enemy (or weapon)
    private Vector3 FrontSafeDistanceCheck(Vector3 objectPosition, Vector3 direction)
    {
        RaycastHit safeDistance = new RaycastHit();
        blinkExit = objectPosition - (direction * 2);
        Physics.Raycast(objectPosition, blinkExit - objectPosition, out safeDistance, maxDistance: 2);
        if (safeDistance.point == Vector3.zero)
        {
            return blinkExit;
        }

        return Vector3.zero;
    }

    //"actual" teleport. Brings the player to the safe teleport poin
    private void GoTo()
    {
        controller.enabled = false;
        initiator.gameObject.transform.position = blinkExit;
        controller.enabled = true;
    }
}
