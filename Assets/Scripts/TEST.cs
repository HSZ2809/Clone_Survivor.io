using UnityEngine;
using UnityEngine.Playables;

public class TEST : MonoBehaviour
{
    // public int testInt = 0;
    public PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void TestMethod1()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void TestMethod2()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}