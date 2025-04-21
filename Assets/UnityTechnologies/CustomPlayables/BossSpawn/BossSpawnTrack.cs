using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.454902f, 0f)]
    [TrackClipType(typeof(BossSpawnClip))]
    [TrackBindingType(typeof(BossSpawn))]
    public class BossSpawnTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            BossSpawn trackBinding = director.GetGenericBinding(this) as BossSpawn;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}