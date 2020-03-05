using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimeLine_BossDash : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var scriptPlayable = ScriptPlayable<BossDash_Playable>.Create(graph);
        return scriptPlayable;
    }
}

class BossDash_Playable : PlayableBehaviour  {
    BossSkills bossSkills;
    float timmer;
    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);
        bossSkills = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSkills>();
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
        bossSkills.warningLR.gameObject.SetActive(false);
        bossSkills.canDash = true;
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        base.ProcessFrame(playable, info, playerData);
        bossSkills.BossDash(timmer);
        timmer++;
    }
}

class PlayableTest : PlayableBehaviour {
    /// <summary>
    /// 当该PlayableBehaviour的PlayableGraph启动时调用
    /// </summary>
    /// <param name="playable"></param>
    public override void OnGraphStart(Playable playable) {
        base.OnGraphStart(playable);
    }

    /// <summary>
    /// 当该PlayableBehaviour的PlayState转换为PlayState.Play时调用
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
        Debug.Log("Play");
    }

    /// <summary>
    /// 该函数与ProcessFrame函数功能相同，都是在该PlayableBehaviour播放的每一帧中调用，相当于Update函数的功能
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void PrepareFrame(Playable playable, FrameData info) {
        base.PrepareFrame(playable, info);
    }

    /// <summary>
    /// 该函数与PrepareFrame函数功能相同，都是在该PlayableBehaviour播放的每一帧中调用，相当于Update函数的功能
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        base.ProcessFrame(playable, info, playerData);
    }

    /// <summary>
    /// 该函数在PlayableBehaviour片段的PlayState转换为Pause时调用
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info) {
        base.OnBehaviourPause(playable, info);
    }

    /// <summary>
    /// 该函数在PlayableBehaviour片段停止播放时调用
    /// </summary>
    /// <param name="playable"></param>
    public override void OnGraphStop(Playable playable) {
        base.OnGraphStop(playable);
    }

    /// <summary>
    /// 该函数在PlayableBehaviour片段创建时调用
    /// </summary>
    /// <param name="playable"></param>
    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);
    }

    /// <summary>
    /// 该函数在PlayableBehaviour片段销毁时调用
    /// </summary>
    /// <param name="playable"></param>
    public override void OnPlayableDestroy(Playable playable) {
        base.OnPlayableDestroy(playable);
    }

    /// <summary>
    /// 该函数在PlayableGraph的PrepareData阶段被调用
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void PrepareData(Playable playable, FrameData info) {
        base.PrepareData(playable, info);
    }
}
