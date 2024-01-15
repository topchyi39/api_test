using System;
using UI.Tweens;
using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;

namespace UI.Views
{
    public class LoadingPopup : MonoBehaviour
    {
        [SerializeField] private UITween tween;
        [SerializeField] private Transform loadingElement;
        [SerializeField] private float rotatingSpeed;
        
        private IEnumerator _loadingRoutine;
        
        private void Awake() => tween.HideImmediately();
        
        public void Show()
        {
            tween.Show();
            _loadingRoutine = LoadingRoutine();
            StartCoroutine(_loadingRoutine);
        }
        public void Hide()
        {
            tween.Hide();
            StopCoroutine(_loadingRoutine);
        }

        private IEnumerator LoadingRoutine()
        {
            while (enabled)
            {
                loadingElement.Rotate(Vector3.forward, rotatingSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}