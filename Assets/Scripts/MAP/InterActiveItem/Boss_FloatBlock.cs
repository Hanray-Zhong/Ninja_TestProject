using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FloatBlock : MonoBehaviour
{
    public float DownSpeed;
    public float distance_toGround;
    public Vector2 x_offset;
    private Vector3 oringinPos;

    public bool isFollowingPlayer;

    private bool isRising = false;
    private bool onGround = false;
    private bool canInvoke = true;
    private int timmer = 0;

    private Rigidbody2D father_rigidbody2D;
    private Transform father_transform;
    private PlayerUnit playerUnit;

    private void Start() {
        oringinPos = transform.parent.position;
        father_rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        father_transform = transform.parent.GetComponent<Transform>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
    }

    private void Update() {
        if (playerUnit.IsDead)
            Destroy(transform.parent.gameObject);
        if (canInvoke) {
            Invoke("DropDown", 3);
            canInvoke = false;
        }
        FollowPlayer();
        IsOnGround();
        if (onGround) {
            if (timmer >= 3)
                Destroy(transform.parent.gameObject);
            isFollowingPlayer = true;
            canInvoke = true;
            DownSpeed *= 1.1f;
        }
    }
    private void DropDown() {
        timmer++;
        isFollowingPlayer = false;
        father_rigidbody2D.velocity = new Vector2(0, -DownSpeed);
    }


    private void FollowPlayer() {
        if (isFollowingPlayer) {
            father_rigidbody2D.velocity = new Vector2(father_rigidbody2D.velocity.x, 0);
            father_transform.position = new Vector3(playerUnit.gameObject.transform.position.x, oringinPos.y, 0);
        }
    }
    private void IsOnGround() {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground")) ||
                    Physics2D.Raycast(transform.position + (Vector3)x_offset, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground")) ||
                    Physics2D.Raycast(transform.position - (Vector3)x_offset, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -distance_toGround, 0));
        Gizmos.DrawLine(transform.position + (Vector3)x_offset, transform.position + new Vector3(0, -distance_toGround, 0));
    }
}
