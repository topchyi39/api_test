using DG.Tweening;
using UnityEngine;

namespace UI.Tweens
{
    public class UITween : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeInTime = 0.2f;
        [SerializeField] private float fadeOutTime = 0.2f;
        
        private Vector2 _defaultSizeDelta;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _defaultSizeDelta = _rectTransform.sizeDelta;
        }

        public void Show()
        {
            canvasGroup.DOFade(1, fadeInTime);
            canvasGroup.blocksRaycasts = true;
            transform.DOScale(Vector3.one, fadeInTime);
        }

        public void Hide()
        {
            canvasGroup.DOFade(0, fadeOutTime);
            canvasGroup.blocksRaycasts = false;
            transform.DOScale(Vector3.one * 1.5f, fadeOutTime);
        }

        public void HideImmediately()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            transform.localScale = Vector3.one * 1.5f;
        }
    }
}