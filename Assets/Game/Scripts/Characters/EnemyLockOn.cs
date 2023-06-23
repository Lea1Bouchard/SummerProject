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
    private List<Characters> nearbyEnemies;

    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;
    [SerializeField] private Canvas lockOnCanvas;

    void Start()
    {
        player = Player.Instance;
        cam = Camera.main.transform;
        lockOnCanvas.gameObject.SetActive(false);

        nearbyEnemies = new List<Characters>();

    }

    void Update()
    {

        if (player.target != null)
        {
            if (BlockCheck(player.target.targetLocation) || RangeCheck(player.target.targetLocation))
            {
                Unfocus();
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


        foreach (Collider collider in nearbyTargets)
        {
            Vector3 direction = collider.transform.position - cam.position;
            direction.y = 0;
            float angle = Vector3.Angle(cam.forward, direction);

            nearbyEnemies.Add(collider.gameObject.GetComponent<Characters>());

            if (angle < closestAngle)
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

    public void Unfocus()
    {
        cinemachineAnimator.SetBool("Targeted", false);
        lockOnCanvas.gameObject.SetActive(false);
        lockOnCamera.LookAt = null;
    }

    public void NextTarget()
    {
        int currentIndex = 0;
        AddCloseEnemies();

        foreach (Characters enemy in nearbyEnemies)
        {
            if (enemy == player.target)
            {
                Debug.Log("Current enemy index : " + currentIndex);

                break;
            }
            currentIndex++;
        }

        currentIndex++;

        if (nearbyEnemies.Count <= currentIndex)
            currentIndex = 0;



        lockOnCanvas.gameObject.SetActive(false);
        Debug.Log("New index : " + EnemiesInFov(currentIndex));
        player.SetTarget(nearbyEnemies[EnemiesInFov(currentIndex)]);
        TargetChanged();
    }

    private void AddCloseEnemies()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(player.transform.position, noticeZone, targetLayers);

        foreach (Collider enemies in nearbyTargets)
        {
            Debug.Log("Current enemies in range : " + nearbyTargets.Length);

            if (!nearbyEnemies.Find(x => x.GetInstanceID() == enemies.GetComponent<Characters>().GetInstanceID()))
            {
                nearbyEnemies.Add(enemies.GetComponent<Characters>());
                Debug.Log("New enemy in range");
            }
        }
    }

    public void RemoveCloseEnemies(Characters characterToRemove)
    {
        foreach (Characters enemies in nearbyEnemies)
        {
            if (nearbyEnemies.Find(x => x.GetInstanceID() == enemies.GetComponent<Characters>().GetInstanceID()))
            {
                nearbyEnemies.Remove(characterToRemove);
            }
        }
        
    }

    private void TargetChanged()
    {
        lockOnCamera.LookAt = player.target.targetLocation;
    }

    private int EnemiesInFov(int curIndex)
    {
        int checkIndex = curIndex;
        do
        {
            Vector3 direction = nearbyEnemies[checkIndex].transform.position - lockOnCamera.transform.position;
            direction.y = 0;
            float angle = Vector3.Angle(cam.forward, direction);

            if (angle < 45)
            {
                if (BlockCheck(nearbyEnemies[checkIndex].transform) && RangeCheck(nearbyEnemies[checkIndex].transform))
                    return checkIndex;
            }

            checkIndex++;
            if (nearbyEnemies.Count <= checkIndex)
                checkIndex = 0;

        } while (checkIndex != curIndex);

        return curIndex;
    }
}
