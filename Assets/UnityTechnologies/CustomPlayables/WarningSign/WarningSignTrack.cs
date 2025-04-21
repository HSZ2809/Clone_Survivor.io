using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0f, 0f)]
    [TrackClipType(typeof(WarningSignClip))]
    [TrackBindingType(typeof(WarningSign))]
    public class WarningSignTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            WarningSign trackBinding = director.GetGenericBinding(this) as WarningSign;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}