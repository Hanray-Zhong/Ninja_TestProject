using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterEvent : EventTypes
{
    private void Start() {
        if (targetEventType == TargetEventTypes.Dialogue) {
            EventManager.GetInstance.StartListening(eventName, () => { myEvent.DialogueEvent(targetEvent_Obj, EndEvent); });
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (haveCondition) {
            EventCondition.Invoke();
            if (!isMeetCondition) {
                return;
            }
        }
        if (other.tag == "Player" && isActive) {
            EventManager.GetInstance.TriggerEvent(eventName);
            if (isActiveOnce) {
                isActive = false;
            }
        }
    }
}
