using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BossFSM : MonoBehaviour
{
    [System.Serializable]
    public struct BossSkillEvents {
        public UnityEvent bossSkill;
    }


    public BossSkillEvents[] bossSkillEvents;
    
    private List<UnityEvent> BossSkillsList = new List<UnityEvent>();

    public bool FSMStart = false;
    public bool currentStateEnd = false;
    public bool isRunning = false;

    private UnityEvent currentState;
    public int BossSkillsListIndex;

    private void Start() {
        for (int i = 0; i < bossSkillEvents.Length; i++) {
            BossSkillsList.Add(bossSkillEvents[i].bossSkill);
        }
        currentState = BossSkillsList[BossSkillsListIndex];
        
    }

    private void Update() {
        if (FSMStart) {
            Invoke("RunState", 2);
            FSMStart = false;
            isRunning = true;
        }
        if (isRunning) {
            FSM_Update();
        }
    }

    public void FSM_Update() {
        if (currentStateEnd) {
            if (!ChangeState()) {
                return;
            }
            RunState();
            currentStateEnd = false;
        }
    }
    public bool ChangeState() {
        if (BossSkillsListIndex < BossSkillsList.Count - 1) {
            currentState = BossSkillsList[++BossSkillsListIndex];
            return true;
        }
        else {
            isRunning = false;
            return false;
        }
    }

    public void RunState() {
        currentState.Invoke();
        Debug.Log("RunState");
    }
}
