using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class PausePanelController : MonoBehaviour
{
    private static PausePanelController _instance;
    public static PausePanelController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("PausePanel").GetComponent<PausePanelController>();
            }
            return _instance;
        }
    }

    [Header("是否暂停")]
    public bool isPause;
    [Header("暂停菜单元素")]
    public GameObject BackGround;
    public GameObject ContinueButton;
    public GameObject ExitButton;
    [Header("过渡背景")]
    public Animation TransitionBG;
    [Header("Scene Tool")]
    public SceneTools sceneTools;

    private PlayerController playerController;
    private PlayerUnit playerUnit;
    private BGMController bgmController;

    private void Awake()
    {
        playerController = PlayerController.Instance;
        playerUnit = PlayerUnit.Instance;
        bgmController = BGMController.Instance;
    }


    private void Update()
    {
        // Debug.Log(Input.GetAxisRaw("XBox_Cancel"));
        if (Input.GetAxisRaw("XBox_Cancel") > 0.9f)
        {
            isPause = true;
            playerController.isControlled = false;
            Time.timeScale = 0;
            SetPanelActive(true);
        }
    }

    private void SetPanelActive(bool isActive)
    {
        BackGround.SetActive(isActive);
        ContinueButton.SetActive(isActive);
        ExitButton.SetActive(isActive);
    }

    public void ContinueButtonFunc()
    {
        isPause = false;
        playerController.isControlled = true;
        Time.timeScale = 1;
        SetPanelActive(false);
    }

    public void ExitButtonFunc()
    {
        isPause = false;
        Time.timeScale = 1;
        SetPanelActive(false);
        if (TransitionBG != null)
        {
            TransitionBG.Play("TransitionFadeUp");
        }
        sceneTools.LoadScene(0, 1);
        if (bgmController != null)
        {
            Destroy(bgmController.gameObject, 1);
        }
    }
}
