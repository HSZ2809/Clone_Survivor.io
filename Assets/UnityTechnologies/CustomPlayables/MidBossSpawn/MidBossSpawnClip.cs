using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class MidBossSpawnClip : PlayableAsset, ITimelineClipAsset
    {
        public MidBossSpawnBehaviour template = new MidBossSpawnBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MidBossSpawnBehaviour>.Create(graph, template);

            return playable;
        }
    }
}