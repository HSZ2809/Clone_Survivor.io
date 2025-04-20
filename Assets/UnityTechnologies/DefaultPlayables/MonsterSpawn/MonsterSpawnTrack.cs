using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using ZUN;

[TrackColor(1f, 0.4562354f, 0f)]
[TrackClipType(typeof(MonsterSpawnClip))]
[TrackBindingType(typeof(MonsterSpawn))]
public class MonsterSpawnTrack : TrackAsset
{
    // Please note this assumes only one component of type MonsterSpawn on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        MonsterSpawn trackBinding = director.GetGenericBinding(this) as MonsterSpawn;
        if (trackBinding == null)
            return;

#endif
        base.GatherProperties (director, driver);
    }
}
