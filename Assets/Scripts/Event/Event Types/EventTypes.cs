using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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