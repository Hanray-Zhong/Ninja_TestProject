using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCEventTrigger : MonoBehaviour
{
    public EventController eventController;
    public GameObject eventObj;
    [Header("Condition")]
    public UnityEvent EventCondition;
    public bool isMeetCondition = false;

    [Header("End event")]
    public UnityEvent CurrentEndEvent;

    private bool isActive = true;

    private void Update() {
        EventCondition.Invoke();
        if (isMeetCondition) {
            if (eventObj.tag == "Dialogue" && isActive) {
                Debug.Log("Dialogue");
                eventController.StartDialogueEvent(eventObj, CurrentEndEvent);
                isActive = false;
            }
        }
    }
}
