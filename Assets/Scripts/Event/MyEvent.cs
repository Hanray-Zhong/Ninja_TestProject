using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TargetEventTypes {
    Dialogue,
    NPC_Dialogue,
}


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
    private float delta_JumpInteraction;// event interaction
    private float lastJumpInteraction;
    private float inputBuffer = 0;
    public PlayerController playerController;

    [Header("Condition-Enemy Disappear")]
    public EnemyGrounps[] enemyGrounps;
    public NPCEvent[] NPCs;

    [Header("Dialogue")]
    public DialogueController dialogueController;

    // Event
    public void LinkActive() {
        Debug.Log("link active");
        playerController.allowLink = true;
    }
    public void Condition_EnemyDisappear(int index) {
        for (int i = 0; i < enemyGrounps[index].enemies.Length; i++) {
            if (enemyGrounps[index].enemies[i].activeSelf) return;
        }
        NPCs[index].isMeetCondition = true;
    }
    public void DialogueEvent(GameObject dialogue, UnityEvent EndEvent) {
        if (dialogueController.gameObject.activeSelf == true) return;
        playerController.isControlled = false;
        dialogueController.gameObject.SetActive(true);
        dialogueController.SetTextsActive(dialogue, EndEvent);
    }
}
