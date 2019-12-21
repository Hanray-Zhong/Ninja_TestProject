using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

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
[CustomEditor(typeof(NPCEvent))]
public class NpcEventInspector : Editor {
    public override void OnInspectorGUI() {
        this.serializedObject.Update();
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("myEvent"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("playerController"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("eventName"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetEvent_Obj"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("targetEventType"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("haveCondition"));
        if ((target as EventTypes).haveCondition) {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("EventCondition"));
            /*EditorGUILayout.PropertyField(this.serializedObject.FindProperty("isMeetCondition"));*/
        }
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("haveEndEvent"));
        if ((target as EventTypes).haveEndEvent) {
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("EndEvent"));
        }
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("isActiveOnce"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("PlayerGameInput"));
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("triggerRange"));
        this.serializedObject.ApplyModifiedProperties();
    }
}
