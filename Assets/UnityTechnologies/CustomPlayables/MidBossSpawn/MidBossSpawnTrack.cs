using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0.454902f, 0f)]
    [TrackClipType(typeof(MidBossSpawnClip))]
    [TrackBindingType(typeof(MidBossSpawn))]
    public class MidBossSpawnTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            MidBossSpawn trackBinding = director.GetGenericBinding(this) as MidBossSpawn;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}