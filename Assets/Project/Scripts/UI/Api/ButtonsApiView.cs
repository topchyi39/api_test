using System;
using Api;
using UI.MVP;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UI.Views;
using Utility.SLayout;

namespace UI.Api
{
    public class ButtonsApiView : View<ButtonsApiPresenter>
    {
        [Header("Request Buttons")]
        [SerializeField] private Button create;
        [SerializeField] private Button delete;
        [SerializeField] private Button update;
        [SerializeField] private Button refresh;
        
        [Header("Content")] 
        [SerializeField] private ApiButtonView buttonPrefab;
        [SerializeField] private RectTransform contentParent;
        
        [SerializeField] private int capacity;

        [Header("Popups")]
        [SerializeField] private IDFieldPopup idFieldPopup;
        [SerializeField] private LoadingPopup loadingPopup;
        
        [Space] 
        [SerializeField] private DestroyParticles particles;
        
        

        private List<ApiButtonView> _buttons;

        private void Awake()
        {
            _buttons = new List<ApiButtonView>(capacity);

            for (var i = 0; i < capacity; i++)
            {
                var button = Instantiate(buttonPrefab, contentParent);
                button.Init(null).Hide();
                _buttons.Add(button);
            }
        }

        public override void Bind(IPresenter presenter)
        {
            base.Bind(presenter);
        
            create.onClick.AddListener(Presenter.Create);
            delete.onClick.AddListener(Presenter.Delete);
            update.onClick.AddListener(Presenter.Update);
            refresh.onClick.AddListener(Presenter.Refresh);
        }

        public async Task<int?> OpenIdField() => await idFieldPopup.Open();
        public async Task<(int?, PopupResult)> OpenIdField(string submitText = "", string cancelText = "") 
            => await idFieldPopup.Open(submitText, cancelText);

        public void UpdateContent(List<ApiButton> values)
        {
            foreach (var button in _buttons)
            {
                button.Hide();
            }
            
            for (var i = 0; i < values.Count; i++)
            {
                var button = GetOrCreateButton(i);
                button.Init(values[i]).UpdateContent();
            }
        }

        private ApiButtonView GetOrCreateButton(int i)
        {
            ApiButtonView button = null; 
            
            if (i >= _buttons.Count)
            {
                button = Instantiate(buttonPrefab, contentParent);
                _buttons.Add(button);
            }
            else
            {
                button = _buttons[i];
            }
            
            button.transform.SetSiblingIndex(i);
            button.gameObject.SetActive(true);
            
            return button;
        }

        public void UpdateItem(int index, ApiButton button)
        {
            GetOrCreateButton(index)
                .Init(button)
                .UpdateContent();
        }

        public void RemoveItem(int index)
        {
            if (_buttons.Count > index)
            {
                var button = _buttons[index];
                _buttons.Remove(button);
                _buttons.Add(button);
                button.Delete(particles);
            }
        }

        public void ShowLoading() => loadingPopup.Show();
        public void HideLoading() => loadingPopup.Hide();

    }
}