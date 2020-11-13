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

    [Header("NPC Event")]
    public EnemyGrounps[] enemyGrounps;
    public NPCEvent[] NPCEvents;
    public Animation TransitionBGAnim;

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
        NPCEvents[index].isMeetCondition = true;
    }
    public void DialogueEvent(GameObject dialogue, UnityEvent EndEvent) 
    {
        if (dialogueController.gameObject.activeSelf == true) return;
        playerController.isControlled = false;
        dialogueController.gameObject.SetActive(true);
        dialogueController.SetTextsActive(dialogue, EndEvent);
    }
    /**
    public void NpcCanBeCarried(int index) 
    {
        NPCEvents[index].gameObject.GetComponent<NPCUnit>().canBeCarried = true;
    }
    **/
    public void NPCCanFollowPlayer(int index)
    {
        NPCUnit unit = NPCEvents[index].gameObject.GetComponent<NPCUnit>();
        unit.canFollowPlayer = true;
        unit.isFloat = false;
        unit.floatCube.SetActive(false);
        playerController.allowThrowCube = false;
    }

    public void NpcDisappear(int index)
    {
        NPCUnit unit = NPCEvents[index].gameObject.GetComponent<NPCUnit>();
        playerController.allowThrowCube = true;
        unit.floatCube.SetActive(true);
        // NPCEvents[index].gameObject.SetActive(false);
        Destroy(NPCEvents[index].gameObject, 1);

        if (TransitionBGAnim != null)
        {
            TransitionBGAppear();
            Invoke("TransitionBGDisappear", 1);
        }
    }
    private void TransitionBGAppear()
    {
        TransitionBGAnim.Play("TransitionFadeUp");
    }
    private void TransitionBGDisappear()
    {
        TransitionBGAnim.Play("TransitionFadeDown");
    }
}
