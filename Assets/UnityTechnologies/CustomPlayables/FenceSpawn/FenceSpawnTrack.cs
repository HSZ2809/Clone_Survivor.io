using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0f, 0f)]
    [TrackClipType(typeof(FenceSpawnClip))]
    [TrackBindingType(typeof(FenceSpawn))]
    public class FenceSpawnTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            FenceSpawn trackBinding = director.GetGenericBinding(this) as FenceSpawn;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}