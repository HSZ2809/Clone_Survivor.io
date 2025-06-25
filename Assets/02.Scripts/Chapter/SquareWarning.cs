using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class SquareWarning : MonoBehaviour
    {
        [SerializeField] GameObject squareFence;

        [SerializeField] SpriteRenderer sr;

        Character character;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            sr = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            Vector2 spawnPos = character.transform.position + new Vector3(0, 16.45f);

            Sequence blinkSequence = DOTween.Sequence();
            blinkSequence.Append(sr.DOFade(0.5f, 0.2f).SetLoops(10, LoopType.Yoyo));
            blinkSequence.AppendCallback(() =>
            {
                Instantiate(squareFence, spawnPos, character.transform.rotation);
            });
            blinkSequence.Append(sr.DOFade(0.5f, 0.2f).SetLoops(10, LoopType.Yoyo));
            blinkSequence.OnComplete(() =>
            {
                Destroy(this.gameObject);
            });

            blinkSequence.Play();
        }
    }
}