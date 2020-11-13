using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    private static BGMController _instance;
    public static BGMController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject BGMController = GameObject.Find("BGMController");
                if (BGMController != null)
                    _instance = BGMController.GetComponent<BGMController>();
            }
            return _instance;
        }
    }

    [Header("BGM")]
    public AudioClip[] BGMs;
    [Header("Scene")]
    public bool SceneTransition;
    public bool IsChangeScene;

    AudioSource _audio;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _audio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsChangeScene)
        {
            int SceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Debug.Log("SceneIndex : " + SceneIndex);

            PlayBGM(BGMs[SceneIndex]);

            IsChangeScene = false;
        }
    }
    private void FixedUpdate()
    {
        if (SceneTransition)
        {
            if (StopBGM())
            {
                SceneTransition = false;
            }
        }
    }

    /// <summary>
    /// 停止播放BGM（渐变）
    /// </summary>
    /// <returns></returns>
    public bool StopBGM()
    {
        if (_audio.volume > 0)
        {
            _audio.volume -= 0.02f;
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 播放对应BGM
    /// </summary>
    public void PlayBGM(AudioClip targetBGM)
    {
        _audio.clip = targetBGM;
        _audio.volume = 1;
        _audio.Play();
    }
}
