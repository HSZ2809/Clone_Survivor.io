using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class WarningSignClip : PlayableAsset, ITimelineClipAsset
    {
        public WarningSignBehaviour template = new WarningSignBehaviour();
        [SerializeField] WarningSign.WarningType type;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<WarningSignBehaviour>.Create(graph, template);
            WarningSignBehaviour behaviour = playable.GetBehaviour();

            behaviour.type = type;

            return playable;
        }
    }
}