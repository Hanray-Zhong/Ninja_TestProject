using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventObj;
    public EventController eventController;
    public UnityEvent CurrentEndEvent;


    private bool isActive = true;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (eventObj.tag == "Dialogue" && isActive) {
                Debug.Log("Dialogue");
                eventController.StartDialogueEvent(eventObj, CurrentEndEvent);
                isActive = false;
            }
        }
    }
}
