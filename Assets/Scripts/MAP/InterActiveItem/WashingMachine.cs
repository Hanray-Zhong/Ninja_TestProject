using UnityEngine;

public class WashingMachine : InterActiveItem {
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D playerRigidbody;
    public float UpSpeed;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }
    private void Update() {
        // InterAction();
    }
    public override void InterAction() {
        // playerController.AllowJump = false;
        // playerController.SecJump = false;
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, UpSpeed);
    }
}