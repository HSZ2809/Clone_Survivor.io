using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class MidBossSpawnerClip : PlayableAsset, ITimelineClipAsset
    {
        public MidBossSpawnerBehaviour template = new MidBossSpawnerBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MidBossSpawnerBehaviour>.Create(graph, template);

            return playable;
        }
    }
}