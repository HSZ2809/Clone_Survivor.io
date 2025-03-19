using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class BGMCtrl : MonoBehaviour
    {
        Manager_Audio manager_audio;

        [SerializeField] AudioClip defaultClip;
        [SerializeField] AudioClip bossClip;

        private void Awake()
        {
            manager_audio = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Audio>();
        }

        private void Start()
        {
            manager_audio.MusicPlayer.clip = defaultClip;
            manager_audio.MusicPlayer.Play();
        }

        public void SetBossClip()
        {
            manager_audio.MusicPlayer.DOFade(0f, 1f).OnComplete(() =>
            {
                manager_audio.MusicPlayer.clip = bossClip;
                manager_audio.MusicPlayer.Play();

                manager_audio.MusicPlayer.DOFade(1f, 1f);
            });
        }

        public void SetDefaultClip()
        {
            manager_audio.MusicPlayer.DOFade(0f, 1f).OnComplete(() =>
            {
                manager_audio.MusicPlayer.clip = defaultClip;
                manager_audio.MusicPlayer.Play();

                manager_audio.MusicPlayer.DOFade(1f, 1f);
            });
        }
    }
}