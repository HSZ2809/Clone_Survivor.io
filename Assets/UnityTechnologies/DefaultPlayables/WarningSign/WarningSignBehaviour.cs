using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using ZUN;

[Serializable]
public class WarningSignBehaviour : PlayableBehaviour
{
    WarningSign m_TrackBinding;

    bool triggered = false;
    // 워닝 사인 종류 구분 enum 

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as WarningSign;

        if (m_TrackBinding == null)
            return;

        if (!triggered)
        {
            m_TrackBinding.BossWarning();
            triggered = true;
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        triggered = false;
    }
}
