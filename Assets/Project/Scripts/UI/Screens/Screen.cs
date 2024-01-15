using System;
using System.Collections.Generic;
using System.Linq;
using UI.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster), typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        private Dictionary<Type, IView> _views;

        private void Awake()
        {
            _views = GetComponentsInChildren<IView>().ToDictionary(item => item.GetType(), item => item);
        }

        public void Bind(IPresenter presenter)
        {
            if (!_views.TryGetValue(presenter.ViewType, out var view)) return;
            
            presenter.Bind(view);
            view.Bind(presenter);
        }
    }
}