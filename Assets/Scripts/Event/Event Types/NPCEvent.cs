using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCEvent : EventTypes
{
    [Header("Player Input")]
    public GameInput PlayerGameInput;
    private float delta_JumpInteraction;
    private float lastJumpInteraction;
    private float inputBuffer = 0;
    [Space()]
    [Range(0, 10)]
    public float triggerRange;
    private void Start() {
        if (targetEventType == TargetEventTypes.NPC_Dialogue) {
            EventManager.GetInstance.StartListening(eventName, () => { myEvent.DialogueEvent(targetEvent_Obj, EndEvent); });
            return;
        }
    }

    private void Update() {
        if (haveCondition) {
            EventCondition.Invoke();
            if (!isMeetCondition) {
                return;
            }
        }
        
        if (isActive) {
            NPCTrigger();
        }
    }
    private void NPCTrigger() {
        delta_JumpInteraction = PlayerGameInput.GetJumpInteraction() - lastJumpInteraction;
        lastJumpInteraction = PlayerGameInput.GetJumpInteraction();
        float distanc2player = Vector2.Distance(gameObject.transform.position, playerController.gameObject.transform.position);
        if (delta_JumpInteraction > 0 && inputBuffer >= 10 && distanc2player <= triggerRange) {
            if (playerController.gameObject.transform.position.x >= gameObject.transform.position.x) {
                playerController.FaceRight = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else {
                playerController.FaceRight = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            EventManager.GetInstance.TriggerEvent(eventName);
            if (isActiveOnce) {
                isActive = false;
            }
        }
        inputBuffer++;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}
