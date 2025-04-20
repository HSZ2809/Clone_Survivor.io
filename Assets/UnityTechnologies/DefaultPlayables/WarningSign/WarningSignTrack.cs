using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using ZUN;

[TrackColor(1f, 0f, 0f)]
[TrackClipType(typeof(WarningSignClip))]
[TrackBindingType(typeof(WarningSign))]
public class WarningSignTrack : TrackAsset
{
    //public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    //{
    //    return ScriptPlayable<WarningSignMixerBehaviour>.Create (graph, inputCount);
    //}

    // Please note this assumes only one component of type WarningSign on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        WarningSign trackBinding = director.GetGenericBinding(this) as WarningSign;
        if (trackBinding == null)
            return;

#endif
        base.GatherProperties (director, driver);
    }
}
