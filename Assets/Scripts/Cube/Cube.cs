using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
    public float ActiveTime;
    public float rotateSpeed;
    public GameObject Player;
    public bool HitGround = false;
    public bool HitEnemy = false;
    public bool HitBoss = false;
    public GameObject target = null;
    public bool HitInteractiveItem = false;
    public GameObject InteractiveItem;
    public float timeScale;

    private PlayerUnit playerUnit;
    private Rigidbody2D cube_rigidbody;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = Player.GetComponent<PlayerUnit>();
        cube_rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.World);
        StartCoroutine(Disappear());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (playerUnit.IsDead) Destroy(gameObject);
        if (other.tag == "Ground" || other.tag == "BossThorns" || other.tag == "BossRoomGround") {
            HitGround = true;
            cube_rigidbody.velocity = Vector2.zero;
            Destroy(gameObject, 1);
        }
        if (other.tag == "Enemy") {
            HitEnemy = true;
            target = other.gameObject;
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (other.tag == "InterActiveItem") {
            HitInteractiveItem = true;
            InteractiveItem = other.gameObject;
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        HitEnemy = false;
        HitInteractiveItem = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator Disappear() {
        yield return new WaitForSeconds(ActiveTime);
        cube_rigidbody.velocity = Vector2.zero;
        Destroy(gameObject, 1);
    }
}