using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutoMove_Item : MonoBehaviour
{
    public Vector2 Pos_start;
    public Vector2 Pos_end;
    public float MoveSpeed;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 MoveDir = Vector2.zero;

    private void Awake() {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _transform = transform;
        MoveDir = (Pos_end - Pos_start).normalized;

    }

    private void Update() {
        MoveBetweenTwoPoints(Pos_start, Pos_end);
    }

    private void MoveBetweenTwoPoints(Vector2 pos_1, Vector2 pos_2) {
        if (pos_1.x > pos_2.x || pos_1.y > pos_2.y) {
            Debug.LogError("Pos_1 must be lower than Pos_2.");
            return;
        }
        if (_transform.position.x < pos_1.x || _transform.position.y < pos_1.y)
            MoveDir = (pos_2 - pos_1).normalized;
        if (_transform.position.x > pos_2.x || _transform.position.y > pos_2.y)
            MoveDir = (pos_1 - pos_2).normalized;

        _rigidbody2D.velocity = MoveDir * MoveSpeed;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(Pos_start, 0.1f);
        Gizmos.DrawSphere(Pos_end, 0.1f);
    }
}
