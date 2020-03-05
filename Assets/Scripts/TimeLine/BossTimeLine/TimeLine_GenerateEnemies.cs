using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLine_GenerateEnemies : PlayableAsset {
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var scriptPlayable = ScriptPlayable<GenerateEnemies_Playable>.Create(graph);
        return scriptPlayable;
    }
}

class GenerateEnemies_Playable : PlayableBehaviour {
    BossSkills bossSkills;
    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);
        bossSkills = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSkills>();
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
        bossSkills.GenerateEnemies_TimeLine();
    }
}
