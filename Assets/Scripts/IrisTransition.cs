using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class IrisTransition : MonoBehaviour
    {
        // [SerializeField] RawImage transitionImage;
        [SerializeField] string irisIn = "In";
        Animator animator;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void IrisIn()
        {
            animator.SetTrigger(irisIn);
        }

        private void SetGameObjectFalse()
        {
            gameObject.SetActive(false);
        }
    }
}