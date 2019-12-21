using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossFSM : MonoBehaviour
{
    public bool currentStateEnd;

    private UnityAction currentState;

    public void FSM_Update() {
        if (currentStateEnd) {
            RunState();
        }
    }

    public void RunState() {
        currentState.Invoke();
    }
    public void ChangeState(UnityAction nextState, UnityAction transition) {
        transition.Invoke();
        currentState = nextState;
    }
}
