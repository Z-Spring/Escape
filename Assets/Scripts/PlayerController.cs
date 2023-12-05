using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // todo: 单例有问题
    public static PlayerController Instance { get; private set; }
    public bool HasThrownFood { get; set; }
    [SerializeField] private bool isGrounded;

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    public event Action<PlayerController> SnakeCloseSound;

    public Transform holdCollectionPoint;
    [SerializeField] private LayerMask snakeLayer;
    [SerializeField] private float detectSnakeRadius = 5f;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform interactUI;

    [Space(10)] [Header("Ground Check")] [SerializeField]
    private float groundDistance = 0.1f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Space(10)] [Header("Jump")] [SerializeField]
    private float gravityMult = 1f;

    [SerializeField] private float jumpHeight = 2f;

    private Rigidbody rb;
    private float gravity;

    [FormerlySerializedAs("collection")] [SerializeField]
    private Transform food;

    [FormerlySerializedAs("snakeHead")] [SerializeField]
    private Transform snake;

    [Space(10)] [Header("Heart Beat")] [SerializeField]
    private Transform heartBeatImage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = Physics.gravity.y;
        heartBeatImage.gameObject.SetActive(false);
        // heartBeatFastLine.gameObject.SetActive(false);
        // heartBeatDeadLine.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        Move();
        Jump();
    }

    void Update()
    {
        ThrowCollection();
        DetectSnake();
    }


    private void Move()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * xMov + transform.forward * zMov;
        moveDir.Normalize();
        
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
    }

    private void Jump()
    {
        Vector3 velocity = rb.velocity;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            // gravityMult
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMult);
            rb.velocity = velocity;
        }
    }

    private void ThrowCollection()
    {
        if (Input.GetKeyDown(KeyCode.F) && HasCollection())
        {
            food.GetComponent<Rigidbody>().isKinematic = false;
            food.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
            food.transform.SetParent(null);
            food = null;
            HasThrownFood = true;
        }
    }

    private void DetectSnake()
    {
        float distance = Vector3.Distance(transform.position, snake.position);
        if (distance < detectSnakeRadius)
        {
            SnakeCloseSound?.Invoke(this);
            Debug.Log("Detect Snake");
            heartBeatImage.gameObject.SetActive(true);
        }
        else
        {
            heartBeatImage.gameObject.SetActive(false);
        }
    }


    public void SetCollection(Transform target)
    {
        food = target;
    }

    public Transform GetCollection()
    {
        return food;
    }

    public bool HasCollection()
    {
        return food != null;
    }

    private void OnDrawGizmos()
    {
        // color red
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectSnakeRadius);
    }

    public bool IsWalking()
    {
        return rb.velocity != Vector3.zero;
    }
}