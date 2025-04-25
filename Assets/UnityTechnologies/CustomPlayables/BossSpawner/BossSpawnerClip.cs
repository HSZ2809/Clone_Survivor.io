using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class BossSpawnerClip : PlayableAsset, ITimelineClipAsset
    {
        public BossSpawnerBehaviour template = new BossSpawnerBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BossSpawnerBehaviour>.Create(graph, template);

            return playable;
        }
    }
}