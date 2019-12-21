using UnityEngine;

public class EnemyUnit : MonoBehaviour {
    public bool lookAtPlayer;
    public bool canBeResurrected;
    public int MaxHealth;
    public int health { get; set; }
    [HideInInspector]
    public bool IsReviving;

    public Transform playerTransform;

    private void Start() {
        health = MaxHealth;
        if (lookAtPlayer)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        if (lookAtPlayer) {
            if (playerTransform.position.x >= gameObject.transform.position.x) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public virtual void GetHurt(int damage) {
        health -= damage;
        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }
}