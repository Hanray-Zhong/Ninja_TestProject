using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineActivatedMethod : MonoBehaviour
{
    private CinemachineBrain cinemachineBrain;
    private bool isPause = false;
    private PlayerController playerController;
    private void Awake()
    {
        cinemachineBrain = gameObject.GetComponent<CinemachineBrain>();
        playerController = PlayerController.Instance;
    }
    private void Update()
    {
        if (isPause)
        {
            if (cinemachineBrain.IsBlending)
            {
                Time.timeScale = 0.05f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                PlayerController.Instance.enabled = true;
                isPause = false;
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
    }
    public void ChangeCinemachineEvent()
    {
        isPause = true;
        PlayerController.Instance.ResetPlayerControllerStatus();
        PlayerController.Instance.enabled = false;
    }
}
