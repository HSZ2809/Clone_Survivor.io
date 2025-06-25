using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class ProjectionSizeUp : MonoBehaviour
    {
        [SerializeField] float changeTime;
        [SerializeField] float addSize;

        public void CamProjectionSizeUp()
        {
            StartCoroutine(SizeUp(changeTime, addSize));
        }

        IEnumerator SizeUp(float changeTime, float value)
        {
            float time = 0.0f;

            while (time < changeTime)
            {
                Camera.main.orthographicSize += value * Time.deltaTime / changeTime;
                time += Time.deltaTime;

                yield return null;
            }
        }
    }
}