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
    private CameraFollow cameraFollow;
    // private bool allowChangeMap_x_Croutine = false;
    // private bool allowChangeMap_y_Croutine = false;

    private void Awake() {
        max = CameraBounds.bounds.max;//初始化边界最大值(边界右上角)
        cameraHeight = 2 * max.y;
        cameraWidth = 2 * max.x;
        Debug.Log(cameraHeight + " " + cameraWidth);
        if (Player != null) {
            player_transform = Player.GetComponent<Transform>();
        }
        cameraFollow = gameObject.GetComponent<CameraFollow>();
    }

    private void Update() {
        Vector2 CameraToPlayer = player_transform.position - Target.transform.position;
        if (CameraToPlayer.x > cameraWidth / 2) {
            Target.transform.SetPositionAndRotation(Target.transform.position + new Vector3(cameraWidth, 0, 0), Quaternion.identity);
            // allowChangeMap_x_Croutine = true;
        }
        if (CameraToPlayer.x < -cameraWidth / 2) {
            Target.transform.SetPositionAndRotation(Target.transform.position + new Vector3(-cameraWidth, 0, 0), Quaternion.identity);
            // allowChangeMap_x_Croutine = true;
        }
        if (CameraToPlayer.y > cameraHeight / 2) {
            Target.transform.SetPositionAndRotation(Target.transform.position + new Vector3(0, cameraHeight, 0), Quaternion.identity);
            // allowChangeMap_y_Croutine = true;
        }
        if (CameraToPlayer.y < -cameraHeight / 2) {
            Target.transform.SetPositionAndRotation(Target.transform.position + new Vector3(0, -cameraHeight, 0), Quaternion.identity);
            // allowChangeMap_y_Croutine = true;
        }
        cameraFollow.UpdateBounds();
        // if (allowChangeMap_x_Croutine) {
        //     StartCoroutine(ChangeMap_x());
        //     allowChangeMap_x_Croutine = false;
        // }
        // if (allowChangeMap_y_Croutine) {
        //     StartCoroutine(ChangeMap_y());
        //     allowChangeMap_y_Croutine = false;
        // }
    }

    // private IEnumerator ChangeMap_x() {
    //     cameraFollow.SmoothlyUpdateBounds_x = true;
    //     yield return new WaitForSeconds(0.1f);
    //     cameraFollow.SmoothlyUpdateBounds_x = false;
    // }
    // private IEnumerator ChangeMap_y() {
    //     cameraFollow.SmoothlyUpdateBounds_y = true;
    //     yield return new WaitForSeconds(0.1f);
    //     cameraFollow.SmoothlyUpdateBounds_y = false;
    // }
}
