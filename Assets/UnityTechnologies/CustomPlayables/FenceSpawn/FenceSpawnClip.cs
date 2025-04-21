using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class FenceSpawnClip : PlayableAsset, ITimelineClipAsset
    {
        public FenceSpawnBehaviour template = new FenceSpawnBehaviour();
        // 팬스 종류 선택 enum

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<FenceSpawnBehaviour>.Create(graph, template);

            return playable;
        }
    }
}