using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    private Transform currentTarget;
    private Animator anim;

    [Tooltip("StateDrivenMethod for Switching Cameras")]
    [SerializeField] private Animator cinemachineAnimator;

    [Header("Settings")]
    [SerializeField] private bool zeroVert_Look;
    [SerializeField] private float noticeZone = 10;
    [SerializeField] private float lookAtSmoothing = 2;
    [Tooltip("Angle degree")]
    [SerializeField] private float maxToticeAngle = 60;

    [SerializeField] private float crossHair_Scale = 0.01f;

    private Transform cam;
    private bool enemyLocked;
    private float currentYOffset;
    private Vector3 pos;

    [SerializeField] private Transform LockOnCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
