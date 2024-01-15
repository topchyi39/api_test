using System;
using System.Collections.Generic;
using System.Linq;
using UI.MVP;
using UnityEngine;
using Screen = UI.Screens.Screen;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Dictionary<Type, Screen> _screens;
        
        protected override void Init()
        {
            _screens = GetComponentsInChildren<Screen>().ToDictionary(item => item.GetType(), item => item);
            Application.targetFrameRate = 60;
        }

        public void Bind<T>(IPresenter presenter) where T : Screen
        {
            var type = typeof(T);
            if (_screens.TryGetValue(type, out var screen))
            {
                screen.Bind(presenter);
            }
        }
    }
}