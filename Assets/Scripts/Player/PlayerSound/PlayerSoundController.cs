using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlayerSoundType 
{
    run,
    jump,
    falldown,
    throwcube,
    flash,
    hook,
    stoptime,
    hit,
    trampoline,
    dead,
}


public class PlayerSoundController : MonoBehaviour
{
    private static PlayerSoundController _instance;
    public static PlayerSoundController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject playerSoundController = GameObject.Find("PlayerSoundController");
                if (playerSoundController != null)
                    _instance = playerSoundController.GetComponent<PlayerSoundController>();
            }
            return _instance;
        }
    }


    [Header("音效列表")]
    public AudioSource[] AudioSources;

    /// <summary>
    /// 播放对应的音效
    /// </summary>
    /// <param name="name"></param>
    public void Play(PlayerSoundType name)
    {
        switch (name)
        {
            case PlayerSoundType.run:
                AudioSources[0].Play();
                break;
            case PlayerSoundType.jump:
                AudioSources[1].Play();
                break;
            case PlayerSoundType.falldown:
                AudioSources[2].Play();
                break;
            case PlayerSoundType.throwcube:
                AudioSources[3].Play();
                break;
            case PlayerSoundType.flash:
                AudioSources[4].Play();
                break;
            case PlayerSoundType.hook:
                AudioSources[5].Play();
                break;
            case PlayerSoundType.stoptime:
                AudioSources[6].Play();
                break;
            case PlayerSoundType.hit:
                AudioSources[7].Play();
                break;
            case PlayerSoundType.trampoline:
                AudioSources[8].Play();
                break;
            case PlayerSoundType.dead:
                AudioSources[9].Play();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 停止播放对应音效
    /// </summary>
    /// <param name="name"></param>
    public void StopPlay(PlayerSoundType name)
    {
        switch (name)
        {
            case PlayerSoundType.run:
                AudioSources[0].Stop();
                break;
            case PlayerSoundType.jump:
                AudioSources[1].Stop();
                break;
            case PlayerSoundType.falldown:
                AudioSources[2].Stop();
                break;
            case PlayerSoundType.throwcube:
                AudioSources[3].Stop();
                break;
            case PlayerSoundType.flash:
                AudioSources[4].Stop();
                break;
            case PlayerSoundType.hook:
                AudioSources[5].Stop();
                break;
            case PlayerSoundType.stoptime:
                AudioSources[6].Stop();
                break;
            case PlayerSoundType.hit:
                AudioSources[7].Stop();
                break;
            case PlayerSoundType.trampoline:
                AudioSources[8].Stop();
                break;
            case PlayerSoundType.dead:
                AudioSources[9].Stop();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 停止播放所有持续性音效
    /// </summary>
    public void StopAll()
    {
        AudioSources[0].Stop();
        AudioSources[6].Stop();
    }

    /// <summary>
    /// 检测该音效是否正在播放
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsPlaying(PlayerSoundType name)
    {
        switch (name)
        {
            case PlayerSoundType.run:
                return AudioSources[0].isPlaying;
            case PlayerSoundType.jump:
                return AudioSources[1].isPlaying;
            case PlayerSoundType.falldown:
                return AudioSources[2].isPlaying;
            case PlayerSoundType.throwcube:
                return AudioSources[3].isPlaying;
            case PlayerSoundType.flash:
                return AudioSources[4].isPlaying;
            case PlayerSoundType.hook:
                return AudioSources[5].isPlaying;
            case PlayerSoundType.stoptime:
                return AudioSources[6].isPlaying;
            case PlayerSoundType.hit:
                return AudioSources[7].isPlaying;
            case PlayerSoundType.trampoline:
                return AudioSources[8].isPlaying;
            case PlayerSoundType.dead:
                return AudioSources[9].isPlaying;
        }

        return false;
    }


}
