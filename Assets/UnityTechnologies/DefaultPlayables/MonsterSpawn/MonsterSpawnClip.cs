using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MonsterSpawnClip : PlayableAsset, ITimelineClipAsset
{
    public MonsterSpawnBehaviour template = new MonsterSpawnBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MonsterSpawnBehaviour>.Create (graph, template);
        return playable;
    }
}
