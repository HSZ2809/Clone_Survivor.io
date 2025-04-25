using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class MonsterSpawnerClip : PlayableAsset, ITimelineClipAsset
    {
        public MonsterSpawnerBehaviour template = new MonsterSpawnerBehaviour();
        [SerializeField] int amount;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<MonsterSpawnerBehaviour>.Create(graph, template);
            MonsterSpawnerBehaviour behaviour = playable.GetBehaviour();

            behaviour.amount = amount;

            return playable;
        }
    }
}