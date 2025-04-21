using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class ProjectionSizeUpClip : PlayableAsset, ITimelineClipAsset
    {
        public ProjectionSizeUpBehaviour template = new ProjectionSizeUpBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<ProjectionSizeUpBehaviour>.Create(graph, template);

            return playable;
        }
    }
}