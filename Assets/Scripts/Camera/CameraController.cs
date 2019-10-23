using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class CameraController : MonoBehaviour
{
    public BoxCollider2D CameraBounds;
    public GameObject Target;
    public GameObject Player;
    private Vector3 min;
    private Vector3 max;
    private float cameraHeight;
    private float cameraWidth;
    private Transform player_transform;
    private Transform target_rigidbody;

    private void Awake() {
        min = CameraBounds.bounds.min;//初始化边界最小值(边界左下角)
        max = CameraBounds.bounds.max;//初始化边界最大值(边界右上角)
        Vector3 Screen_max = new Vector3(Screen.width, Screen.height, 0);
        cameraHeight = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).y;
        cameraWidth = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).x;
        Debug.Log(cameraHeight + " " + cameraWidth);
        if (Player != null) {
            player_transform = Player.GetComponent<Transform>();
        }
    }

    private void Update() {
        Vector2 CameraToPlayer = player_transform.position - transform.position;
        if (CameraToPlayer.x > cameraWidth / 2) {
            Target.transform.SetPositionAndRotation(transform.position + new Vector3(cameraWidth, 0, 0), Quaternion.identity);
        }
        if (CameraToPlayer.x < -cameraWidth / 2) {
            Target.transform.SetPositionAndRotation(transform.position + new Vector3(-cameraWidth, 0, 0), Quaternion.identity);
        }
        if (CameraToPlayer.y > cameraHeight / 2) {
            Target.transform.SetPositionAndRotation(transform.position + new Vector3(0, cameraHeight, 0), Quaternion.identity);
        }
        if (CameraToPlayer.y < -cameraWidth / 2) {
            Target.transform.SetPositionAndRotation(transform.position + new Vector3(0, -cameraHeight, 0), Quaternion.identity);
        }
    }
    
}
