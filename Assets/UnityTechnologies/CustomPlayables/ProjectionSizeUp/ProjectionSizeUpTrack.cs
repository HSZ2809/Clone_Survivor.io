using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [TrackColor(1f, 0f, 0.4936571f)]
    [TrackClipType(typeof(ProjectionSizeUpClip))]
    [TrackBindingType(typeof(ProjectionSizeUp))]
    public class ProjectionSizeUpTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            ProjectionSizeUp trackBinding = director.GetGenericBinding(this) as ProjectionSizeUp;
            if (trackBinding == null)
                return;
#endif
            base.GatherProperties(director, driver);
        }
    }
}