using System;
using Api;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Api
{
    public class ApiButtonView : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text id;
        [SerializeField] private TMP_Text text;

        private Color _defaultTextColor;
        
        private ApiButton _currentButton;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _defaultTextColor = id.color;
        }

        public ApiButtonView Init(ApiButton button)
        {
            _currentButton = button;
            return this;
        }

        public void UpdateContent()
        {
            if (_currentButton == null)
            {
                Clear();
                return;
            }

            background.color = ColorUtility.TryParseHtmlString(_currentButton.color, out var color) 
                ? color 
                : Color.white;
            
            id.color = _defaultTextColor;
            id.text = _currentButton.id.ToString();
            text.text = _currentButton.text;
            text.color = _defaultTextColor;
        }

        public void Delete(DestroyParticles particles)
        {
            _rectTransform.DOShakePosition(.3f, 10f, 20,fadeOut: false).onComplete += ()=>
            {
                particles.Play(transform.position, _rectTransform.sizeDelta, background.color);
                Hide();
            };
        }
        

        public void Hide()
        {
            Clear();
            gameObject.SetActive(false);
        }

        private void Clear()
        {
            background.color = Color.white;
            id.text = string.Empty;
            text.text = string.Empty;
        }
    }
}