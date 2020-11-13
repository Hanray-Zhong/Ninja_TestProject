using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControllerTrigger : MonoBehaviour
{
    [Header("停止播放BGM")]
    public bool isStoppingPlayerBGM;
    [Header("播放对应BGM")]
    public bool isPlayingTargetBGM;
    public AudioClip TargetBGM;

    private BGMController bgmController;

    private void Awake()
    {
        bgmController = BGMController.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bgmController == null)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            if (isStoppingPlayerBGM)
            {
                bgmController.SceneTransition = true;
            }
            if (isPlayingTargetBGM)
            {
                bgmController.PlayBGM(TargetBGM);
            }
        }
    }

    public void PlayTargetBGM()
    {
        if (bgmController != null)
            bgmController.PlayBGM(TargetBGM);
    }
}
