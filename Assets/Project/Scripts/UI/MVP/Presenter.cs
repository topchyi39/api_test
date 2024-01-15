using System;

namespace UI.MVP
{
    public abstract class Presenter<TM, TV> : IPresenter where TM : IModel where TV : IView
    {
        public Type ViewType => typeof(TV);
        
        protected TM Model { get; private set; }
        protected TV View { get; private set; }
        
        public Presenter(TM model)
        {
            Model = model;
        }

        
        public void Bind(IView view)
        {
            View = (TV)view;
        }
    }
}