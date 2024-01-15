using System;
using UI;
using UI.Api;
using UI.MVP;
using UI.Screens;
using UnityEngine;
using Utils;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Api
{
    public class ButtonsApi : MonoBehaviour, IModel
    {
        [SerializeField] private string secret;
        [SerializeField] private string resources;
        
        public IReadProperty<List<ApiButton>> Buttons => _buttons;
        
        private ButtonsApiPresenter _presenter;
        private Request _request;
        private ReactiveProperty<List<ApiButton>> _buttons;
        
        private void Awake()
        {
            _request = new Request(secret, resources);
            
            _buttons = new ReactiveProperty<List<ApiButton>>
            {
                Value = new List<ApiButton>(100)
            };

            _presenter = new ButtonsApiPresenter(this);
            
        }

        private void Start()
        {
            UIManager.Instance.Bind<ButtonApiScreen>(_presenter);
            
            RefreshRequest();
        }

        public async UniTask CreateRequest()
        {
            var button = await _request.CreateAsync();
            _buttons.Value.Add(button);
            _presenter.UpdateItem(_buttons.Value.Count - 1, button);
        }

        public async UniTask DeleteRequest(int id)
        {
            await _request.DeleteAsync(id);

            var index = _buttons.Value.FindIndex(item => item.id == id);
            if (index < 0) return;
            
            _buttons.Value.Remove(_buttons.Value[index]);
            _presenter.RemoveItem(index);
        }

        public async UniTask UpdateRequest(int id)
        {
            var index = _buttons.Value.FindIndex(item => item.id == id);
            var currentValue = _buttons.Value[index];
            
            var apibutton = new ApiButton
            {
                id = id,
                color = ApiButton.GetRandomColor(),
                text = currentValue.text
            };
            
            var button = await _request.PatchAsync(id, apibutton);
            
            if (index < 0) return;
            _buttons.Value[index] = button;
            _presenter.UpdateItem(index, button);
        }

        public async UniTask RefreshRequest()
        {
            var array = await _request.GetAsync();

            _buttons.Value.Clear();
            _buttons.Value.AddRange(array);
            _buttons.Invoke();
        }
        
        public async UniTask RefreshRequest(int id)
        {
            var button = await _request.GetAsync(id);
            
            var index = _buttons.Value.FindIndex(item => item.id == id);

            if (index < 0) return;
            _buttons.Value[index] = button;
            _presenter.UpdateItem(index, button);
        }
    }
}