using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ZUN
{
    public class Btn_GetMeat : MonoBehaviour
    {
        public MissionCtrl missionCtrl;

        public void Select()
        {
            missionCtrl.GetMeat();
        }
    }
}