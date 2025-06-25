using UnityEngine;

namespace ZUN
{
    public class Btn_GetMeat : MonoBehaviour
    {
        public ChapterCtrl chapterCtrl;

        public void Select()
        {
            chapterCtrl.GetMeat();
        }
    }
}