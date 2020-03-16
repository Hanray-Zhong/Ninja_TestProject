using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScripts : MonoBehaviour
{
    public void MoveBetweenTwoPoints(Vector2 pos_1, Vector2 pos_2, float moveSpeed, Transform self_transform, Rigidbody2D self_rigidbody) {
        Vector2 MoveDir = Vector2.zero; ;

        if (pos_1.x > pos_2.x || pos_1.y > pos_2.y) {
            Debug.LogError("Pos_1 must be lower than Pos_2.");
            return;
        }
        if (self_transform.position.x <= pos_1.x || self_transform.position.y <= pos_1.y)
            MoveDir = (pos_2 - pos_1).normalized;
        else if (self_transform.position.x >= pos_2.x || self_transform.position.y >= pos_2.y)
            MoveDir = (pos_1 - pos_2).normalized;

        self_rigidbody.velocity = MoveDir * moveSpeed;
    }
}
