using UnityEngine;

namespace ZUN
{
    public class IrisTransition : MonoBehaviour
    {
        // [SerializeField] string irisOut = "Out";
        // [SerializeField] string irisIn = "In";

        [SerializeField] string irisOut = "IrisOut";
        [SerializeField] string irisIn = "IrisIn";

        [SerializeField] AnimationClip irisOutClip;
        [SerializeField] AnimationClip irisInClip;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void IrisOut()
        {
            // animator.SetTrigger(irisOut);
            animator.Play(irisOut);
        }

        public void IrisIn()
        {
            // animator.SetTrigger(irisIn);
            animator.Play(irisIn);
        }

        public float GetIrisOutClipLength()
        {
            return irisOutClip.length;
        }

        public float GetIrisInClipLength()
        {
            return irisInClip.length;
        }

        private void SetGameObjectFalse()
        {
            gameObject.SetActive(false);
        }
    }
}