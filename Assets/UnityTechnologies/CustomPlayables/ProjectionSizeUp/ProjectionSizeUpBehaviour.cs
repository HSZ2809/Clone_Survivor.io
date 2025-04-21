using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ZUN
{
    [Serializable]
    public class ProjectionSizeUpBehaviour : PlayableBehaviour
    {
        ProjectionSizeUp m_TrackBinding;

        bool triggered = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as ProjectionSizeUp;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.CamProjectionSizeUp();
                triggered = true;
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            triggered = false;
        }
    }
}