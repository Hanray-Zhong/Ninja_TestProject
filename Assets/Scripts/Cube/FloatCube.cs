using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCube : MonoBehaviour
{
    public float rotateSpeed;
    public PlayerController playerController;

    private SpriteRenderer spriteRenderer;
    private Color invisibleColor = new Color(0, 0, 0, 0);
    private Color originColor = new Color(1, 1, 1, 1);
    private float oriPos_x;
    private float oriPos_y;

    private void Awake() {
        oriPos_x = transform.localPosition.x;
        oriPos_y = transform.localPosition.y;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.World);
        if (playerController != null) {
            if (playerController.FaceRight) {
                transform.localPosition = new Vector2(oriPos_x, oriPos_y);
            }
            else {
                transform.localPosition = new Vector2(-oriPos_x, oriPos_y);
            }
            if (playerController.CubeCDTimer < 150) {
                spriteRenderer.color = invisibleColor;
            }
            else {
                spriteRenderer.color = originColor;
            }
        }
    }
}
