using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineActivatedMethod : MonoBehaviour
{
    public PlayerController playerController;
    private CinemachineBrain cinemachineBrain;
    private bool isPause = false;
    private void Awake() {
        cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
    }
    private void Update() {
        if (isPause) {
            if (cinemachineBrain.IsBlending) {
                Time.timeScale = 0.05f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else {
                playerController.enabled = true;
                playerController.ResetStatus();
                isPause = false;
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
    }
    public void ChangeCinemachineEvent() {
        isPause = true;
        playerController.enabled = false;
    }
}
