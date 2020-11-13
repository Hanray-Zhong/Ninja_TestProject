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
    [Header("玩家互动")]
    public PlayerController playerController;
    public PlayerUnit playerUnit;
    public bool canFollowPlayer = false;
    public bool lookAtPlayer;
    public GameObject floatCube;
    [Header("上下浮动")]
    public bool isFloat;
    public float freq;
    public float amplitude;

    private bool isResurrecting = false;

    private Transform _transform;
    private Transform playerTransform;
    private Vector2 oriPos;

    public bool IsDead { get; set; }

    private void Awake()
    {
        playerController = PlayerController.Instance;
        playerUnit = PlayerUnit.Instance;
        oriPos = transform.position;
    }

    private void Start() {
        _transform = gameObject.transform;
        playerTransform = playerController.gameObject.transform;
    }
    private void Update() {
        /**
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
        **/
        if (isFloat)
        {
            NPCFloat();
        }
        if (canFollowPlayer)
        {
            if (playerUnit.IsDead && !isResurrecting)
            {
                isResurrecting = true;
                Invoke("NpcResurrection", 1.4f);
            }
            if (!playerUnit.IsDead)
            {
                isResurrecting = false;
            }
            Vector2 targetPos = new Vector2();
            if (playerController.FaceRight)
            {
                targetPos = playerController.transform.position + new Vector3(-1.534f, 1.154f, 0);
            }
            else
            {
                targetPos = playerController.transform.position + new Vector3(1.534f, 1.154f, 0);
            }
            SmoothlyFollow(targetPos, new Vector2(0, 0), new Vector2(4, 4));
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

    private void SmoothlyFollow(Vector2 targetPos, Vector2 Margin, Vector2 MoveSmoothing)
    {
        var x = transform.position.x;
        var y = transform.position.y;
        if (Mathf.Abs(x - targetPos.x) > Margin.x)
        {
            x = Mathf.Lerp(x, targetPos.x, MoveSmoothing.x * Time.deltaTime);
        }
        if (Mathf.Abs(y - targetPos.y) > Margin.y)
        {
            y = Mathf.Lerp(y, targetPos.y, MoveSmoothing.y * Time.deltaTime);
        }
        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void NPCFloat()
    {
        Vector2 offset = new Vector2(0, amplitude * Mathf.Sin(Time.time * freq));
        transform.position = (Vector3)(oriPos + offset);
    }
}
