using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class FenceSpawn : MonoBehaviour
    {
        [Header("Component")]
        //[SerializeField] GameObject circleGlow;
        [SerializeField] SquareWarning squareWarning;

        //[SerializeField] GameObject circleFence;
        //[SerializeField] GameObject squareFence;

        Character character;


        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public void SetCircleFence()
        {
            
        }

        public void SetSquareFence()
        {
            Vector2 spawnPos = character.transform.position + new Vector3(0, 16.45f);

            Instantiate(squareWarning, spawnPos, character.transform.rotation);
            
            //sr.sr = squareGlow;
            //Vector2 spawnPos = character.transform.position + new Vector3(0, 16.45f);

            //Sequence blinkSequence = DOTween.Sequence();
            //blinkSequence.Append(sr.DOFade(0f, 0.2f).SetLoops(10, LoopType.Yoyo));
            //blinkSequence.AppendCallback(() =>
            //{
            //    Instantiate(squareFence, spawnPos, character.transform.rotation);
            //});
            //blinkSequence.Append(sr.DOFade(0f, 0.2f).SetLoops(10, LoopType.Yoyo));

            //blinkSequence.Play();
        }
    }
}