using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRegion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !other.GetComponent<PlayerController>().isInvincible) {
            other.GetComponent<PlayerUnit>().IsDead = true;
        }
        if (other.tag == "NPC") {
            other.GetComponent<NPCUnit>().IsDead = true;
        }
    }
}
