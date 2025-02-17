using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class BGMCtrl : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        [SerializeField] AudioClip defaultClip;
        [SerializeField] AudioClip bossClip;

        private void Start()
        {
            audioSource.clip = defaultClip;
            audioSource.Play();
        }

        public void SetBossClip()
        {
            audioSource.DOFade(0f, 1f).OnComplete(() =>
            {
                audioSource.clip = bossClip;
                audioSource.Play();

                audioSource.DOFade(1f, 1f);
            });
        }

        public void SetDefaultClip()
        {
            audioSource.DOFade(0f, 1f).OnComplete(() =>
            {
                audioSource.clip = defaultClip;
                audioSource.Play();

                audioSource.DOFade(1f, 1f);
            });
        }
    }
}