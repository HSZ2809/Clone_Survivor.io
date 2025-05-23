using System;
using UnityEngine.Playables;

namespace ZUN
{
    [Serializable]
    public class MonsterSpawnerBehaviour : PlayableBehaviour
    {
        MonsterSpawner m_TrackBinding;
        public int amount;
        bool triggered;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as MonsterSpawner;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.SetAmount(amount);
                triggered = true;
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (m_TrackBinding != null)
            {
                m_TrackBinding.SetAmount(0);
            }

            base.OnBehaviourPause(playable, info);
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (m_TrackBinding != null)
            {
                m_TrackBinding.SetAmount(0);
            }

            triggered = false;
        }
    }
}