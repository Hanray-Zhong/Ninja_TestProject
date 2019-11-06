using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMachine : MonoBehaviour
{
    public float OnGroundForce;
    public float AirForce;
    public Vector2 WindDir;

    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;

    private void Awake() {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            if (WindDir.x == 0) {
                if (playerController.OnGround) {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * WindDir.y, OnGroundForce);
                }
                else {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x * WindDir.y, AirForce);
                }
            }
            else {
                if (playerController.OnGround) {
                    playerController.MoveSpeed = playerController.NormalSpeed + OnGroundForce * playerController.MoveDir.x * WindDir.x;
                }
                else {
                    playerController.MoveSpeed = playerController.NormalSpeed + AirForce * playerController.MoveDir.x * WindDir.x;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        playerController.MoveSpeed = playerController.NormalSpeed;
    }
}
