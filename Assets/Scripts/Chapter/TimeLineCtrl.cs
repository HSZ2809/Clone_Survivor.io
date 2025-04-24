using UnityEngine;
using UnityEngine.Playables;

public class TimeLineCtrl : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public void Pause()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void Play()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}