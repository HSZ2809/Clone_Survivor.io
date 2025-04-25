using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.454902f, 0f)]
    [TrackClipType(typeof(BossSpawnerClip))]
    [TrackBindingType(typeof(BossSpawner))]
    public class BossSpawnerTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            BossSpawner trackBinding = director.GetGenericBinding(this) as BossSpawner;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}