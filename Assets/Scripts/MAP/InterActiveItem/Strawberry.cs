using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : InterActiveItem
{
    [Header("平滑跟随")]
    public Vector2 MoveSmoothing;
    public Vector2 Margin;
    public float x_offset;
    public float y_offset;
    [Header("特效")]
    public GameObject DisappearEffect;


    private bool IsFollowingPlayer;
    private GameObject target;
    private Vector2 targetPos;
    private Vector2 originPos;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = PlayerController.Instance;
    }

    private void Start() {
        originPos = new Vector2(transform.position.x, transform.position.y);
        targetPos = originPos;
    }

    private void Update() {
        if (IsFollowingPlayer && target != null) {
            if (playerController.FaceRight)
            {
                targetPos = playerController.transform.position + new Vector3(-x_offset, y_offset, 0);
            }
            else
            {
                targetPos = playerController.transform.position + new Vector3(x_offset, y_offset, 0);
            }
            if (target.GetComponent<PlayerUnit>().IsOnSafeRegion) {
                // effect & log
                Instantiate(DisappearEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (target.GetComponent<PlayerUnit>().IsDead) {
                targetPos = originPos;
                IsFollowingPlayer = false;
                target = null;
            }

            if (playerController.transform.position.x >= gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        
        SmoothlyFollow(targetPos, Margin, MoveSmoothing);
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            target = other.gameObject;
            IsFollowingPlayer = true;
        }
    }
    private void SmoothlyFollow(Vector2 targetPos, Vector2 Margin, Vector2 MoveSmoothing) {
        var x = transform.position.x;
        var y = transform.position.y;
        if (Mathf.Abs(x - targetPos.x) > Margin.x) {
            x = Mathf.Lerp(x, targetPos.x, MoveSmoothing.x * Time.deltaTime);
        }
        if (Mathf.Abs (y - targetPos.y)> Margin.y) {
            y = Mathf.Lerp(y, targetPos.y, MoveSmoothing.y * Time.deltaTime);
        }
        transform.position = new Vector3(x, y, transform.position.z);
    }
    public override void InterAction() {
        
    }
}
