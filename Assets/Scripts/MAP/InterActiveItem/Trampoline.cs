using UnityEngine;

public class Trampoline : MonoBehaviour {
    private Rigidbody2D playerRigidbody;
    public float Force;
    private void Awake() {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Force);
        }
    }
}