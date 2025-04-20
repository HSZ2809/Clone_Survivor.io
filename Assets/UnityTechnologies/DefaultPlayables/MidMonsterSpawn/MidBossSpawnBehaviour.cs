using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ZUN
{
    [Serializable]
    public class MidBossSpawnBehaviour : PlayableBehaviour
    {
        MidBossSpawn m_TrackBinding;
        bool triggered;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as MidBossSpawn;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.SetMidBoss();
                triggered = true;
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            triggered = false;
        }
    }
}