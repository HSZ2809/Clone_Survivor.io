using System;
using UnityEngine.Playables;

namespace ZUN
{
    [Serializable]
    public class FenceSpawnBehaviour : PlayableBehaviour
    {
        FenceSpawn m_TrackBinding;
        // 팬스 종류 선택 enum

        bool triggered = false;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as FenceSpawn;

            if (m_TrackBinding == null)
                return;

            if (!triggered)
            {
                m_TrackBinding.SetSquareFence();
                triggered = true;
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            triggered = false;
        }
    }
}