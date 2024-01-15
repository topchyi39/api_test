using System;
using System.Collections.Generic;
using Api;
using Cysharp.Threading.Tasks;
using UI.MVP;
using UI.Views;
using UnityEditor.VersionControl;
using Random = UnityEngine.Random;

namespace UI.Api
{
    public class ButtonsApiPresenter : Presenter<ButtonsApi, ButtonsApiView>
    {
        public ButtonsApiPresenter(ButtonsApi model) : base(model)
        {
            model.Buttons.Subscribe(UpdateViewContent);
        }

        public async void Create()
        {
            View.ShowLoading();
            await Delay();
            await Model.CreateRequest();
            View.HideLoading();
        }

        public async void Delete()
        {
            var id = await View.OpenIdField(); // wait for ID

            if (id != null)
            {
                View.ShowLoading();
                await Delay();
                await Model.DeleteRequest(id.Value);
                View.HideLoading();
            }
        }

        public async void Update()
        {
            var id = await View.OpenIdField();

            if (id != null)
            {
                View.ShowLoading();
                await Delay();
                await Model.UpdateRequest(id.Value);
                View.HideLoading();
            }
        }

        public async void Refresh()
        {
            var (id, result)= await View.OpenIdField("Refresh By ID", "Refresh All");

            View.ShowLoading();
            await Delay();

            var task = new UniTask();
            switch (result)
            {
                case PopupResult.Submitted when id != null:
                    task = Model.RefreshRequest(id.Value);
                    break;
                case PopupResult.Canceled:
                    task = Model.RefreshRequest();
                    break;
            }
            await task;
            
            View.HideLoading();
        }

        public void UpdateItem(int index, ApiButton button)
        {
            View.UpdateItem(index, button);
        }

        public void RemoveItem(int index)
        {
            View.RemoveItem(index);
        }
        
        private void UpdateViewContent(List<ApiButton> values)
        {
            View.UpdateContent(values);
        }

        private UniTask Delay() => UniTask.Delay(TimeSpan.FromSeconds(Random.Range(1f,2f)));
        
    }
}