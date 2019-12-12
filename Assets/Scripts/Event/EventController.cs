using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public GameObject Player;
    public DialogueController dialogueController;

    private UnityEvent currentEndEvent;

    private void Start() {
        if (Player == null) {
            Debug.LogError("EventController : Player is null.");
        }
        if (dialogueController == null) {
            Debug.LogError("EventController : DialogueController is null.");
        }
    }
    // dialogue event
    public void StartDialogueEvent(GameObject dialogue, UnityEvent currentEndEvent) {
        if (dialogueController.gameObject.activeSelf == true) return;
        Player.GetComponent<PlayerController>().isControlled = false;
        dialogueController.gameObject.SetActive(true);
        dialogueController.SetTextsActive(dialogue);
        this.currentEndEvent = currentEndEvent;
    }
    public void DialogueEnd() {
        Invoke("SetPlayerActive", 0.2f);
    }
    private void SetPlayerActive() {
        Player.GetComponent<PlayerController>().isControlled = true;
        if (currentEndEvent != null)
            currentEndEvent.Invoke();
    }
}
