using System.Collections;
using System.Collections.Generic;
using Snake;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SnakeController : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    [SerializeField] internal Transform player;

    #region Snake Info

    [Space(10)] [Header("Snake Info")] [SerializeField]
    internal Transform head;

    [SerializeField] private Transform body;
    [SerializeField] private Transform bodyFollowPoint;
    [SerializeField] internal NavMeshAgent agent;

    [FormerlySerializedAs("patrolPoint")] [SerializeField]
    internal List<Transform> patrolPoints;

    #endregion

    #region Snake Scan Info

    [Space(5)] [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask foodLayer;
    [SerializeField] private float snakeLookAngle = 80f;
    [SerializeField] private int rayCount = 8;

    #endregion

    #region Snake State Info

    [Space(10)] [Header("Snake State Info")] [SerializeField]
    internal float snakeAttackDistance = 1.5f;

    [SerializeField] internal float chaseDistance = 10f;

    [FormerlySerializedAs("scanPlayerRayDistance")] [SerializeField]
    float scanPlayerDistance = 11f;

    [SerializeField] internal float snakeEatTime = 2f;
    [SerializeField] private float scanFoodRadius = 20f;
    [SerializeField] internal Transform food;
    [SerializeField] internal TextMeshProUGUI waitText;
    [SerializeField] internal bool hasSparePlayer;
    [SerializeField] internal float sparePlayerTimer = 5f;

    #endregion

    #region Speed

    [Space(10)] [Header("Speed")] public float bodyFollowSpeed = 2f;
    [SerializeField] private float headMoveSpeed = 10f;
    [SerializeField] private float snakeTurnSpeed = 0.1f;

    #endregion

    private ISnakeState currentState;
    internal int currentPatrolPointIndex;
    private float eatTimer;

    // private enum SnakeState
    // {
    //     Patrol,
    //     Chase,
    //     Attack,
    //     GetFood,
    // }
    //
    // private SnakeState snakeState;

    private void Start()
    {
        currentPatrolPointIndex = 0;
        waitText.text = "";

        SetSnakeState(PatrolState.Instance);
    }

    private void Update()
    {
        IsMoving = !agent.isStopped;
        currentState.Execute(this);
    }

    internal void SetSnakeState(ISnakeState state)
    {
        currentState?.Exit(this);

        currentState = state;
        currentState.Enter(this);
    }


    private void Move()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        IsMoving = xMov != 0 || zMov != 0;

        Vector3 movDir = new Vector3(xMov, 0, zMov);
        movDir.Normalize();


        head.position += movDir * (headMoveSpeed * Time.deltaTime);
        head.forward = Vector3.Slerp(head.forward, movDir, snakeTurnSpeed * Time.deltaTime);

        if (IsMoving)
        {
            body.position = Vector3.Lerp(body.position, bodyFollowPoint.position,
                bodyFollowSpeed * Time.deltaTime);
            body.rotation = Quaternion.Lerp(body.rotation, bodyFollowPoint.rotation,
                bodyFollowSpeed * Time.deltaTime);
        }
    }

    internal bool HasFoundPlayer()
    {
        // float eachAngel = snakeLookAngle / rayCount;
        // for (int i = 1; i <= rayCount; i++)
        // {
        //     Vector3 dir = Quaternion.Euler(0, -snakeLookAngle / 2 + i * eachAngel, 0) * head.forward;
        //     // todo: 不能设置层级，否则射线会忽略其他碰撞体
        //     if (Physics.Raycast(head.position, dir, out RaycastHit hit, scanPlayerRayDistance, playerLayer))
        //     {
        //         Debug.DrawRay(head.position, dir * 10f, Color.green);
        //         return true;
        //     }
        //
        //     Debug.DrawRay(head.position, dir * 10f, Color.red);
        // }
        //
        // return false;

        float distance = Vector3.Distance(head.position, player.position);
        if (distance < scanPlayerDistance)
        {
            return true;
        }

        return false;
    }

    internal bool HasFoundFood()
    {
        Collider[] colliders = Physics.OverlapSphere(head.position, scanFoodRadius, foodLayer);
        if (colliders.Length > 0)
        {
            food = colliders[0].transform;
            return true;
        }

        return false;
    }

    internal void SnakeMoveTowards(Vector3 position)
    {
        agent.SetDestination(position);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(head.position, scanFoodRadius);
        // Gizmos.DrawSphere(head.position, 10f);
    }
}