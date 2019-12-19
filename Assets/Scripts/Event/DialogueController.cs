using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("Events")]
    private UnityEvent endEvent;
    [Header("PlayerController")]
    public PlayerController playerController;
    [Header("Game Input")]
    public GameInput PlayerGameInput;
    private float delta_JumpInteraction;
    private float lastJumpInteraction;
    private float inputBuffer = 0;

    private List<GameObject> texts = new List<GameObject>();
    private int currentTextIndex;
    private GameObject currentDIalogue;
    private void Update() {
        delta_JumpInteraction = PlayerGameInput.GetJumpInteraction() - lastJumpInteraction;
        lastJumpInteraction = PlayerGameInput.GetJumpInteraction();
        if (delta_JumpInteraction > 0 && inputBuffer >= 10) {
            if (currentTextIndex == texts.Count - 1) {
                currentDIalogue.SetActive(false);
                texts.RemoveAll(it => it is GameObject);

                Invoke("RunEndEvent", 0.2f);

                gameObject.SetActive(false);
                return;
            }
            texts[currentTextIndex].SetActive(false);
            currentTextIndex++;
            texts[currentTextIndex].SetActive(true);
            inputBuffer = 0;
        }
        inputBuffer++;
    }

    public void SetTextsActive(GameObject dialogue, UnityEvent endEvent) {
        this.endEvent = endEvent;
        foreach (Transform text in dialogue.transform) {
            texts.Add(text.gameObject);
        }
        dialogue.SetActive(true);
        currentTextIndex = 0;
        currentDIalogue = dialogue;
    }
    private void RunEndEvent() {
        // end event
        if (endEvent != null) 
            endEvent.Invoke();
        endEvent = null;
        SetPlayerActive();
    }
    private void SetPlayerActive() {
        playerController.ResetStatus();
        playerController.isControlled = true;
    }
}
