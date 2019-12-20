using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class EventTypes : MonoBehaviour
{
    public MyEvent myEvent;
    [Header("Player")]
    public PlayerController playerController;


    [Header("Condition")]
    public bool haveCondition;
    public UnityEvent EventCondition;
    public bool isMeetCondition = false;

    [Header("Target Event")]
    public string eventName;
    public GameObject targetEvent_Obj;
    public TargetEventTypes targetEventType;

    [Header("End Event")]
    public bool haveEndEvent;
    public UnityEvent EndEvent;

    [Header("EventProperties")]
    public bool isActiveOnce = true;
    public bool isActive = true;
}

[CustomEditor(typeof(EventTypes), true)]
public class EventTypesInspector : Editor {
    public override void OnInspectorGUI() {
        this.serializedObject.Update();
        // DrawDefaultInspector();
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
        this.serializedObject.ApplyModifiedProperties();
    }
}
