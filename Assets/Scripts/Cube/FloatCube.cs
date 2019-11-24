using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCube : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject Player;
    [Header("Follow")]
    public Vector2 Margin;
    public Vector2 MoveSmoothing;
    public Vector2 targetPos;

    private PlayerController playerController;
    private PlayerUnit playerUnit;
    private SpriteRenderer spriteRenderer;
    private Color invisibleColor = new Color(0, 0, 0, 0);
    private Color originColor = new Color(1, 1, 1, 1);
    private float oriPos_x;
    private float oriPos_y;
    private float x_offset;
    private float y_offset;

    private void Awake() {
        oriPos_x = transform.position.x;
        oriPos_y = transform.position.y;
        x_offset = Mathf.Abs(oriPos_x - Player.transform.position.x);
        y_offset = Mathf.Abs(oriPos_y - Player.transform.position.y);
        playerController = Player.GetComponent<PlayerController>();
        playerUnit = Player.GetComponent<PlayerUnit>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.World);
        if (playerController != null) {
            if (playerController.FaceRight) {
                targetPos = Player.transform.position + new Vector3(-x_offset, y_offset, 0);
            }
            else {
                targetPos = Player.transform.position + new Vector3(x_offset, y_offset, 0);
            }
            SmoothlyFollow(targetPos, Margin, MoveSmoothing);
            if (playerController.CubeCDTimer < 150 || playerUnit.IsDead) {
                spriteRenderer.color = invisibleColor;
            }
            else {
                spriteRenderer.color = originColor;
            }
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
}
