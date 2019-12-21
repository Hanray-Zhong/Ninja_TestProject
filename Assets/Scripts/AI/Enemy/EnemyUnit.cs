using UnityEngine;

public class EnemyUnit : MonoBehaviour {
    private bool isReviving;
    public bool IsReviving {
        get {return this.isReviving;}
        set {this.isReviving = value;}
    }
    public bool canBeResurrected;
    public int MaxHealth;
    public int health { get; set; }

    private void Start() {
        health = MaxHealth;
    }

    public void GetHurt(int damage) {
        health -= damage;
        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }
}