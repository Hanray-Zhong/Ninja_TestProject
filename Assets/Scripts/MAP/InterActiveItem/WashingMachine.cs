using UnityEngine;

public class WashingMachine : InterActiveItem {
    private Rigidbody2D playerRigidbody;
    public float Force;
    private void Awake() {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Update() {
        // InterAction();
    }
    public override void InterAction() {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Force);
    }
}