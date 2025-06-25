using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class BGMCtrl : MonoBehaviour
    {
        [SerializeField] AudioSource bgm;

        [SerializeField] AudioClip defaultClip;
        [SerializeField] AudioClip bossClip;

        private void Start()
        {
            bgm.clip = defaultClip;
            bgm.Play();
        }

        public void SetBossClip()
        {
            bgm.DOFade(0f, 1f).OnComplete(() =>
            {
                bgm.clip = bossClip;
                bgm.Play();

                bgm.DOFade(1f, 1f);
            });
        }

        public void SetDefaultClip()
        {
            bgm.DOFade(0f, 1f).OnComplete(() =>
            {
                bgm.clip = defaultClip;
                bgm.Play();

                bgm.DOFade(1f, 1f);
            });
        }
    }
}