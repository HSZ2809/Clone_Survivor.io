using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZUN
{
    public class AlertPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tmp_message;

        readonly float scaleUpDuration = 0.2f;
        readonly float scaleDownDuration = 0.5f;
        readonly float moveUpDuration = 0.5f;
        readonly float moveDistance = 70f;

        private void Start()
        {
            ShowPopup();
        }

        public void SetMessage(string message)
        {
            tmp_message.text = message;
        }

        void ShowPopup()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.DOScale(new Vector3(1f, 1f, 1f), scaleUpDuration)
                .OnComplete(() =>
                {
                    transform.DOMove(transform.position + Vector3.up * moveDistance, moveUpDuration)
                        .SetEase(Ease.Linear);
                    transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), scaleDownDuration)
                        .SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            Destroy(gameObject);
                        });
                });
        }
    }
}