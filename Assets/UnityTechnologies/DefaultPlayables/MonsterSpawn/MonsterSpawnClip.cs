using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class MonsterSpawnClip : PlayableAsset, ITimelineClipAsset
    {
        public MonsterSpawnBehaviour template = new MonsterSpawnBehaviour();
        [SerializeField] int amount;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MonsterSpawnBehaviour>.Create(graph, template);
            MonsterSpawnBehaviour behaviour = playable.GetBehaviour();

            behaviour.amount = amount;

            return playable;
        }
    }
}