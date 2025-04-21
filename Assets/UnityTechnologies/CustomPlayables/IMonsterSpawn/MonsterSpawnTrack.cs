using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.4562354f, 0f)]
    [TrackClipType(typeof(MonsterSpawnClip))]
    [TrackBindingType(typeof(MonsterSpawn_Circle))]
    public class MonsterSpawnTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            MonsterSpawn_Circle trackBinding = director.GetGenericBinding(this) as MonsterSpawn_Circle;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}