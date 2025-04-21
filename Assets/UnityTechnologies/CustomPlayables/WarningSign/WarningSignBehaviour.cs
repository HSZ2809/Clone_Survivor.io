using System;
using UnityEngine;
using UnityEngine.Playables;

namespace ZUN
{
    [Serializable]
    public class WarningSignBehaviour : PlayableBehaviour
    {
        WarningSign m_TrackBinding;
        public WarningSign.WarningType type;

        bool triggered = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as WarningSign;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.StartWarning(type);
                triggered = true;
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            triggered = false;
        }
    }
}