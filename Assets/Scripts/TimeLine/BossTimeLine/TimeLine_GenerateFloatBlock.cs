using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLine_GenerateFloatBlock : PlayableAsset {
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var scriptPlayable = ScriptPlayable<GenerateFloatBlock_Playable>.Create(graph);
        return scriptPlayable;
    }
}

class GenerateFloatBlock_Playable : PlayableBehaviour {
    BossSkills bossSkills;
    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);
        bossSkills = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSkills>();
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
        bossSkills.GenerateFloatBlock_TimeLine();
    }
}
