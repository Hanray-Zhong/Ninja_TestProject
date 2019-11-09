using UnityEngine;

public class Trampoline : MonoBehaviour {
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D playerRigidbody;
    public float Force;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        
        if (other.tag == "Player") {
            playerController.AllowJump = false;
            playerController.SecJump = false;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Force);
        }
    }
}