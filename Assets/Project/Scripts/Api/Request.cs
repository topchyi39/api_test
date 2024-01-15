using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

namespace Api
{
    public class Request
    {
        private readonly string _apiUrl;
        private readonly string _secret;
        private readonly string _resource;
        
        private const string UrlFormat = "https://{0}.mockapi.io/api/v1/{1}";
        
        public Request(string secret, string resource)
        {
            _secret = secret;
            _resource = resource;
            _apiUrl = string.Format(UrlFormat, secret, resource);
        }

        public async UniTask<ApiButton[]> GetAsync()
        {
            var completed = false;
            var result = Array.Empty<ApiButton>();
            
            RestClient.GetArray<ApiButton>(_apiUrl, (exception, helper, array) =>
            {
                result = array;
                completed = true;
            });

            await UniTask.WaitUntil(() => completed);
            return result;
        }

        public async UniTask<ApiButton> GetAsync(int id)
        {
            var completed = false;
            ApiButton result = null;
            
            RestClient.Get<ApiButton>($"{_apiUrl}/{id}", (exception, helper, array) =>
            {
                result = array;
                completed = true;
            });

            await UniTask.WaitUntil(() => completed);
            return result;
        }

        public async UniTask<ApiButton> CreateAsync()
        {
            var completed = false;
            ApiButton result = null;
            
            RestClient.Post<ApiButton>(_apiUrl, string.Empty, (exception, helper, button) =>
            {
                result = button;
                completed = true;
            });
                
            await UniTask.WaitUntil(() => completed);
            return result;
        }

        public async UniTask<ApiButton> PatchAsync(int id, ApiButton value)
        {
            var completed = false;
            ApiButton result = null;
            
            RestClient.Put<ApiButton>($"{_apiUrl}/{id}", value, (exception, helper, button) =>
            {
                Debug.Log(helper.Text);
                result = button;
                completed = true;
            });
            
            await UniTask.WaitUntil(() => completed);
            return result;
        }

        public async UniTask DeleteAsync(int id)
        {
            var completed = false;
            
            RestClient.Delete($"{_apiUrl}/{id}", (exception, helper) =>
            {
                completed = true;
            });
            
            await UniTask.WaitUntil(() => completed);
        }
    }
}