using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    private BoxCollider2D currentMap;
    public BoxCollider2D CurrentMap {
        get {return currentMap;}
        set {currentMap = value;}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MapBounds") {
            currentMap = other.gameObject.GetComponent<BoxCollider2D>();
        }
    }
}
