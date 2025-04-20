using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class WarningSignClip : PlayableAsset, ITimelineClipAsset
{
    public int testInt;
    public WarningSignBehaviour template = new WarningSignBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<WarningSignBehaviour>.Create (graph, template);
        WarningSignBehaviour ws = playable.GetBehaviour();

        return playable;
    }
}