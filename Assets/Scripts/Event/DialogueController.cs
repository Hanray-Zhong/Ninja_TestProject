using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public EventController eventController;
    [Header("Game Input")]
    public GameInput PlayerGameInput;

    private float delta_JumpInteraction;
    private float lastJumpInteraction;
    private float inputBuffer = 10;

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
                eventController.DialogueEnd();
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

    public void SetTextsActive(GameObject dialogue) {
        foreach (Transform text in dialogue.transform) {
            texts.Add(text.gameObject);
        }
        dialogue.SetActive(true);
        currentTextIndex = 0;
        currentDIalogue = dialogue;
    }
}
