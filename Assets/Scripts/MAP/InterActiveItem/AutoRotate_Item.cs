using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate_Item : MonoBehaviour
{
    public Vector2 Rotate_Center;
    public float Rotate_Speed;
    public bool IsClockwise;

    private Transform _transform;

    private void Awake() {
        _transform = gameObject.GetComponent<Transform>();
    }

    private void Update() {
        _transform.RotateAround(Rotate_Center, (IsClockwise ? -1 : 1) * Vector3.forward, Rotate_Speed * Time.deltaTime);
        _transform.Rotate((IsClockwise ? 1 : -1) * Vector3.forward, Rotate_Speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawSphere(Rotate_Center, 0.1f);
    }
}
