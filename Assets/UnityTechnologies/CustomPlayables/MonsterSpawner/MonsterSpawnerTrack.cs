using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.4562354f, 0f)]
    [TrackClipType(typeof(MonsterSpawnerClip))]
    [TrackBindingType(typeof(MonsterSpawner))]
    public class MonsterSpawnerTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            MonsterSpawner trackBinding = director.GetGenericBinding(this) as MonsterSpawner;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}