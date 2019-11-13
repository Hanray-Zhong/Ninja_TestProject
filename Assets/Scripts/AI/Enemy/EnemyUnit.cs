using UnityEngine;

public class EnemyUnit : MonoBehaviour {
    private bool isReviving;
    public bool IsReviving {
        get {return this.isReviving;}
        set {this.isReviving = value;}
    }
}