using UnityEngine;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        protected ChapterCtrl chapterCtrl;
        protected Character character;

        protected CircleCollider2D cc2D;

        public abstract void SetMonsterSpec(float MaxHp, float ap);
        protected void SetTagAndLayer()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer("Target");
        }
    }
}