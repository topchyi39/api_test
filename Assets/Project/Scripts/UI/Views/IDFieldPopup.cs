using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UI.Tweens;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Views
{
    public enum PopupResult
    {
        None,
        Submitted,
        Canceled
    }
    
    public class IDFieldPopup : MonoBehaviour
    {
        [SerializeField] private UITween tween;
        
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submit;
        [SerializeField] private TMP_Text submitTMP;
        [SerializeField] private Button cancel;
        [SerializeField] private TMP_Text cancelTMP;

        private string _defaultSubmitText;
        private string _defaultCancelText;
        
        
        private PopupResult popupResult;

        private void Awake()
        {
            _defaultSubmitText = submitTMP.text;
            _defaultCancelText = cancelTMP.text;
            
            submit.onClick.AddListener(SubmitClicked); 
            cancel.onClick.AddListener(CloseClicked); 
            tween.HideImmediately();
        }

        public async Task<int?> Open()
        {
            ValidateButtonTexts(string.Empty, string.Empty);

            return await WaitForUser();
        }

        private async Task<int?> WaitForUser()
        {
            popupResult = PopupResult.None;
            inputField.text = string.Empty;

            Show();

            await UniTask.WaitUntil(() => popupResult != PopupResult.None);

            if (popupResult == PopupResult.Submitted)
            {
                if (int.TryParse(inputField.text, out var value))
                {
                    return value;
                }

                return null;
            }
            else
            {
                return null;
            }
        }

        public async Task<(int?, PopupResult)> Open(string submitText = "", string cancelText = "")
        {
            ValidateButtonTexts(submitText, cancelText);
            var value = await WaitForUser();
            return (value, popupResult);
        }
        

        private void ValidateButtonTexts(string submitText, string cancelText)
        {
            submitTMP.text = !string.IsNullOrEmpty(submitText)
                ? submitText
                : _defaultSubmitText;
            
            cancelTMP.text = !string.IsNullOrEmpty(cancelText)
                ? cancelText
                : _defaultCancelText;
        }

        private void SubmitClicked()
        {
            popupResult = PopupResult.Submitted;
            Hide();
        }
        
        private void CloseClicked()
        {
            popupResult = PopupResult.Canceled;
            Hide();
        }

        private void Show()
        {
            tween.Show();
        }

        private void Hide()
        {
            tween.Hide();
        }
    }
}