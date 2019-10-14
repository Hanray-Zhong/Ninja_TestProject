using UnityEngine;

public class Cube : MonoBehaviour {
    public float ActiveTime;
    public GameObject Player;

    // private PlayerController controller;

    // private void Awake() {
    //     controller = Player.GetComponent<PlayerController>();
    // }

    private void Update() {
        Destroy(gameObject, ActiveTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            Destroy(gameObject);
        }
        if (other.tag == "Enemy") {
            Destroy(other.gameObject);
            Player.transform.position = gameObject.transform.position;
            Player.GetComponent<PlayerController>().CD_Time = 150;
            Destroy(gameObject);
        }
    }
}