using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class BossSpawnClip : PlayableAsset, ITimelineClipAsset
    {
        public BossSpawnBehaviour template = new BossSpawnBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BossSpawnBehaviour>.Create(graph, template);

            return playable;
        }
    }
}