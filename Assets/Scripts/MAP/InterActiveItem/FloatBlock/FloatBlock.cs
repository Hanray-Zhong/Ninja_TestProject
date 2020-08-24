using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBlock : MonoBehaviour
{
    [Header("下降，上升速度")]
    public float DownSpeed;
    public float RiseSpeed;
    [Header("触地判定")]
    public float distance_toGround;
    public Vector2 x_offset;
    [Header("贴图")]
    public Sprite sprite_Active;
    public Sprite sprite_sleep;


    private Vector3 oringinPos;
    private bool isRising = false;
    private bool onGround = false;
    private bool canInvoke = true;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite;
    private PlayerUnit playerUnit;

    private void Start() {
        GameObject parent = transform.parent.gameObject;

        oringinPos = parent.transform.position;
        _rigidbody2D = parent.GetComponent<Rigidbody2D>();
        _sprite = parent.GetComponent<SpriteRenderer>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
    }

    private void Update() {
        if (!playerUnit.IsDead) canInvoke = true;
        if (playerUnit.IsDead && canInvoke) {
            Invoke("SetOringinPos", 1f);
            canInvoke = false;
        }
        IsOnGround();
        if (onGround) {
            isRising = true;
        }
        if (isRising) {
            _sprite.sprite = sprite_sleep;
            if (transform.position.y < oringinPos.y) {
                _rigidbody2D.velocity = new Vector2(0, RiseSpeed);
            }
            else {
                SetOringinPos();
            }
        }
    }
    private void IsOnGround() {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground")) || 
                    Physics2D.Raycast(transform.position + (Vector3)x_offset, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground")) || 
                    Physics2D.Raycast(transform.position - (Vector3)x_offset, Vector2.down, distance_toGround, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!other.GetComponent<PlayerUnit>().IsDead && !isRising)
            {
                _rigidbody2D.velocity = new Vector2(0, -DownSpeed);
                _sprite.sprite = sprite_Active;
            }
        }
    }
    private void SetOringinPos() {
        transform.parent.transform.position = oringinPos;
        _rigidbody2D.velocity = Vector2.zero;
        isRising = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -distance_toGround, 0));
        Gizmos.DrawLine(transform.position + (Vector3)x_offset, transform.position + new Vector3(0, -distance_toGround, 0));
    }
}
