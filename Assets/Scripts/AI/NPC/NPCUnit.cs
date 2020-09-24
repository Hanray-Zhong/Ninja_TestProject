using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class NPCUnit : MonoBehaviour
{
    private static NPCUnit instance;
    public static NPCUnit Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("NPC").GetComponent<NPCUnit>();
            }
            return instance;
        }
    }


    public float Max_Velocity;
    public float interactRadius;
    [Header("Player Interact")]
    public PlayerController playerController;
    public PlayerUnit playerUnit;
    public bool canBeCarried = false;
    public bool lookAtPlayer;

    private bool isResurrecting = false;

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Transform playerTransform;

    public bool IsDead { get; set; }

    private void Awake()
    {
        playerController = PlayerController.Instance;
        playerUnit = PlayerUnit.Instance;
    }

    private void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.transform;
        playerTransform = playerController.gameObject.transform;
    }
    private void Update() {
        if (_rigidbody.velocity.magnitude > Max_Velocity) {
            _rigidbody.velocity = _rigidbody.velocity.normalized * Max_Velocity;
        }
        if (canBeCarried) {
            if (Mathf.Abs(playerTransform.position.x - _transform.position.x) < interactRadius && Mathf.Abs(playerTransform.position.y - _transform.position.y) < interactRadius) 
            {
                playerController.allowThrowCube = false;
                playerController.canCarryNPC = true;
                playerController.carriedNPC = gameObject;
            }
            else
            {
                playerController.allowThrowCube = true;
                playerController.canCarryNPC = false;
            }
            if (playerUnit.IsDead && !isResurrecting)
            {
                isResurrecting = true;
                Invoke("NpcResurrection", 1.2f);
            }
            if (!playerUnit.IsDead)
            {
                isResurrecting = false;
            }
        }
        if (lookAtPlayer)
        {
            if (playerTransform.position.x >= gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void NpcResurrection()
    {
        if (playerUnit.ResurrectionPoint != null)
        {
            _transform.position = (Vector2)playerUnit.ResurrectionPoint.transform.position;
        }
    }
}
