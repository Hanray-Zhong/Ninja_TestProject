using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : MonoBehaviour
{
    [Header("过渡用黑色背景")]
    public GameObject TransitionBGFade;
    [Header("关卡选择界面")]
    public GameObject ChooseCardPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Debug.Log("get key");
            if (ChooseCardPanel.activeSelf == false && TransitionBGFade == null)
            {
                ChooseCardPanel.SetActive(true);
            }
        }
    }
}
