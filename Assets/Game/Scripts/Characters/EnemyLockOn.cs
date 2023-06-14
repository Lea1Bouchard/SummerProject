using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

public class EnemyLockOn : MonoBehaviour
{
    private Animator anim;
    private Player player;

    [SerializeField] private LayerMask targetLayers;

    [Tooltip("StateDrivenMethod for Switching Cameras")]
    [SerializeField] private Animator cinemachineAnimator;

    [Header("Settings")]
    [SerializeField] private bool zeroVert_Look;
    [SerializeField] private float noticeZone = 10;
    [Tooltip("Angle degree")]
    [SerializeField] private float maxAngle = 60;

    [SerializeField] private float crossHair_Scale = 0.01f;

    private Transform cam;

    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    [SerializeField] private Transform lockOnCanvas;

    void Start()
    {
        player = Player.Instance;
        cam = Camera.main.transform;
        //lockOnCanvas.gameObject.SetActive(false);

    }

    void Update()
    {
        if(player.target != null)
        {
            if (BlockCheck(player.target.targetLocation) || RangeCheck(player.target.targetLocation))
            {
                cinemachineAnimator.SetBool("Targeted", false);
                player.target = null;
            }
        }
    }

    public Characters GetTarget()
    {
        Debug.Log("Trying to get a target");

        Collider[] nearbyTargets = Physics.OverlapSphere(player.transform.position, noticeZone, targetLayers);
        float closestAngle = maxAngle;
        Characters currentClosest = null;

        if (nearbyTargets.Length <= 0)
        {
            Debug.Log("No enemies D: ");
            return null;
        }
            

        foreach(Collider collider in nearbyTargets)
        {
            Vector3 direction = collider.transform.position - cam.position;
            direction.y = 0;
            float angle = Vector3.Angle(cam.forward, direction);

            if(angle < closestAngle)
            {
                Debug.Log("angle ok");
                if (!BlockCheck(collider.gameObject.GetComponent<Characters>().targetLocation))
                {
                    Debug.Log("new closest");
                    closestAngle = angle;
                    currentClosest = collider.gameObject.GetComponent<Characters>();
                }
                
            }

        }

        if (!currentClosest)
        {
            Debug.Log("No closest, fk u");
            return null;
        }
            

        cinemachineAnimator.SetBool("Targeted", true);
        return currentClosest;
    }

    private bool BlockCheck(Transform thingToCheck)
    {
        //TODO : check the layers to ignore enemy layer
        if (Physics.Linecast(player.transform.position, thingToCheck.position, targetLayers))
            return true;

        return false;
    }

    private bool RangeCheck(Transform thingToCheck)
    {
        float distance = (player.transform.position - thingToCheck.position).magnitude;
        if (distance / 2 > noticeZone) 
            return false; 
        else 
            return true;
    }
}
