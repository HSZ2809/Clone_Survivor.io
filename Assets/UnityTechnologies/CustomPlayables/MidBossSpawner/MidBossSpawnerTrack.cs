using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.454902f, 0f)]
    [TrackClipType(typeof(MidBossSpawnerClip))]
    [TrackBindingType(typeof(MidBossSpawner))]
    public class MidBossSpawnerTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            MidBossSpawner trackBinding = director.GetGenericBinding(this) as MidBossSpawner;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}