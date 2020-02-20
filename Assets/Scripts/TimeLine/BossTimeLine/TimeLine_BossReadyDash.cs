using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLine_BossReadyDash : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var scriptPlayable = ScriptPlayable<ReadyDash_Playable>.Create(graph);
        return scriptPlayable;
    }
}

class ReadyDash_Playable : PlayableBehaviour {
    BossSkills bossSkills;
    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);
        bossSkills = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSkills>();
    }
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        base.ProcessFrame(playable, info, playerData);
        bossSkills.ReadyDash();
    }
}
