using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

public class EnemyLockOn : MonoBehaviour
{
    private Animator anim;
    private Player player;

    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private LayerMask ignoreLayer;

    [Tooltip("StateDrivenMethod for Switching Cameras")]
    [SerializeField] private Animator cinemachineAnimator;

    [Header("Settings")]
    [SerializeField] private bool zeroVert_Look;
    [SerializeField] private float noticeZone = 10;
    [Tooltip("Angle degree")]
    [SerializeField] private float maxAngle = 60;

    private Transform cam;

    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    [SerializeField] private Canvas lockOnCanvas;

    void Start()
    {
        player = Player.Instance;
        cam = Camera.main.transform;
        lockOnCanvas.gameObject.SetActive(false);

    }

    void Update()
    {
        
        if(player.target != null)
        {
            if (BlockCheck(player.target.targetLocation) || RangeCheck(player.target.targetLocation))
            {
                cinemachineAnimator.SetBool("Targeted", false);
                lockOnCanvas.gameObject.SetActive(false);
                lockOnCamera.LookAt = null;
                player.target = null;
            }
        }
        
    }

    public Characters GetTarget()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(player.transform.position, noticeZone, targetLayers);
        float closestAngle = maxAngle;
        Characters currentClosest = null;

        if (nearbyTargets.Length <= 0)
        {
            return null;
        }
            

        foreach(Collider collider in nearbyTargets)
        {
            Vector3 direction = collider.transform.position - cam.position;
            direction.y = 0;
            float angle = Vector3.Angle(cam.forward, direction);

            if(angle < closestAngle)
            {
                if (!BlockCheck(collider.gameObject.GetComponent<Characters>().targetLocation))
                {
                    closestAngle = angle;
                    currentClosest = collider.gameObject.GetComponent<Characters>();
                }
                
            }

        }

        if (!currentClosest)
        {
            return null;
        }

        lockOnCamera.LookAt = currentClosest.targetLocation;
        cinemachineAnimator.SetBool("Targeted", true);
        return currentClosest;
    }

    private bool BlockCheck(Transform thingToCheck)
    {
        if (Physics.Linecast(player.transform.position + new Vector3(0, 1, 0), thingToCheck.position, ~ignoreLayer))
            return true;

        return false;
    }

    private bool RangeCheck(Transform thingToCheck)
    {
        float distance = (player.transform.position - thingToCheck.position).magnitude;
        if (distance / 2 > noticeZone) 
            return true; 
        else 
            return false;
    }

    public void ActivateLockonCanvas()
    {
        lockOnCanvas.gameObject.SetActive(true);
    }
}
