using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    private PlayerController playerController;
    private CinemachineVirtualCamera thisCamera;

    private void Awake()
    {
        playerController = PlayerController.Instance;
        thisCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        ChangeDamping();
    }

    private void ChangeDamping()
    {
        if (playerController.OnGround)
        {
            thisCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1;
            // Debug.Log(thisCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping);
        }
        else
        {
            thisCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 20;
            // Debug.Log(thisCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping);
        }
    }
}
