using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MyEvent : MonoBehaviour
{
    // Data struct
    [System.Serializable]
    public struct EnemyGrounps {
        public GameObject[] enemies;
    }

    // Data
    [Header("Player")]
    public GameInput PlayerGameInput;
    private float delta_JumpInteraction;
    private float lastJumpInteraction;
    private float inputBuffer = 10;
    public PlayerController playerController;
    
    [Header("Condition-Enemy Disappear")]
    public EnemyGrounps[] enemyGrounps;
    public NPCEventTrigger[] NPCEventTriggers;
    public float dialogueRange;

    // Event
    public void LinkActive() {
        Debug.Log("link active");
        playerController.allowLink = true;
    }

    public void Condition_EnemyDisappear(int index) {
        for (int i = 0; i < enemyGrounps[index].enemies.Length; i++) {
            if (enemyGrounps[index].enemies[i].activeSelf) return;
        }
        // Debug.Log("enemy done");
        delta_JumpInteraction = PlayerGameInput.GetJumpInteraction() - lastJumpInteraction;
        lastJumpInteraction = PlayerGameInput.GetJumpInteraction();
        float distanc2player = Vector2.Distance(NPCEventTriggers[index].gameObject.transform.position, playerController.gameObject.transform.position);
        if (delta_JumpInteraction > 0 && inputBuffer >= 10 && distanc2player <= dialogueRange) {
            Debug.Log("Get input");
            if (playerController.gameObject.transform.position.x >= NPCEventTriggers[index].gameObject.transform.position.x) {
                playerController.FaceRight = false;
                NPCEventTriggers[index].gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                playerController.FaceRight = true;
                NPCEventTriggers[index].gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            NPCEventTriggers[index].isMeetCondition = true;
        }
        inputBuffer++;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        foreach (NPCEventTrigger NPCEventTrigger in NPCEventTriggers) {
            Gizmos.DrawWireSphere(NPCEventTrigger.gameObject.transform.position, dialogueRange);
        }
    }
}
