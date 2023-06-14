using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI.Core;

public class EnemyLockOn : MonoBehaviour
{
    private EnemyController currentTarget;
    private Animator anim;
    private Player player;

    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private Transform enemyTarget_Locator;

    [Tooltip("StateDrivenMethod for Switching Cameras")]
    [SerializeField] private Animator cinemachineAnimator;

    [Header("Settings")]
    [SerializeField] private bool zeroVert_Look;
    [SerializeField] private float noticeZone = 10;
    [SerializeField] private float lookAtSmoothing = 2;
    [Tooltip("Angle degree")]
    [SerializeField] private float maxAngle = 60;

    [SerializeField] private float crossHair_Scale = 0.01f;

    private Transform cam;
    private bool enemyLocked;
    private float currentYOffset;
    private Vector3 pos;

    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    [SerializeField] private Transform lockOnCanvas;

    void Start()
    {
        player = Player.Instance;

        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
        lockOnCanvas.gameObject.SetActive(false);

    }

    void Update()
    {
        if(player.target != null)
        {
            if (BlockCheck(player.target.targetLocation) || RangeCheck(player.target.targetLocation))
            {
                anim.Play("FollowCamera");
                player.target = null;
            }
        }
    }

    public Transform GetTarget()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(player.transform.position, noticeZone, targetLayers);
        float closestAngle = maxAngle;
        Transform currentClosest = null;

        if (nearbyTargets.Length <= 0) 
            return null;

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
                    currentClosest = collider.gameObject.GetComponent<Characters>().targetLocation;
                }
                
            }

        }

        if (!currentClosest) 
            return null;

        return currentClosest;
    }

    private bool BlockCheck(Transform thingToCheck)
    {

        if (Physics.Linecast(player.transform.position, thingToCheck.position, targetLayers))
            return true;

        return false;
    }

    private bool RangeCheck(Transform thingToCheck)
    {
        float dis = (player.transform.position - thingToCheck.position).magnitude;
        if (dis / 2 > noticeZone) 
            return false; 
        else 
            return true;
    }
}
