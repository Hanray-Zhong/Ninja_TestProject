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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (IsChangeScene)
        {
            int SceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Debug.Log("SceneIndex : " + SceneIndex);

            AudioSource Audio = gameObject.GetComponent<AudioSource>();
            
            Audio.clip = BGMs[SceneIndex];
            Audio.Play();
            IsChangeScene = false;
        }
    }
    private void FixedUpdate()
    {
        if (SceneTransition)
        {
            AudioSource Audio = gameObject.GetComponent<AudioSource>();
            if (Audio.volume > 0)
            {
                Audio.volume -= 0.02f;
            }
            else
            {
                SceneTransition = false;
                Audio.volume = 1;
            }
        }
    }
}
