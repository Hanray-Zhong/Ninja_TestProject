using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMachine : MonoBehaviour
{
    public float OnGroundForce;
    public float AirForce;
    public Vector2 WindDir;

    [Header("Timmer")]
    public float On_Time;
    public float Off_Time;
    public float on_timer = 0;
    public float off_timer = 0;
    public bool haveWind = false;

    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;

    

    private void Awake() {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate() {
        if (!haveWind) {
            on_timer++;
        }
        else {
            off_timer++;
        }
        if (on_timer >= On_Time) {
            haveWind = true;
            on_timer = 0;
        }
        if (off_timer >= Off_Time) {
            haveWind = false;
            off_timer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player" && haveWind) {
            if (WindDir.x == 0) {
                if (playerController.OnGround) {
                    playerRigidbody.AddForce(WindDir * OnGroundForce);
                }
                else {
                    playerRigidbody.AddForce(WindDir * OnGroundForce);
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
