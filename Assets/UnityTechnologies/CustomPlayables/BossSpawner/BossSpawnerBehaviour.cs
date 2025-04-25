using System;
using UnityEngine.Playables;

namespace ZUN
{
    [Serializable]
    public class BossSpawnerBehaviour : PlayableBehaviour
    {
        BossSpawner m_TrackBinding;
        bool triggered;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as BossSpawner;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.SetReady();
                triggered = true;
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            triggered = false;
        }
    }
}