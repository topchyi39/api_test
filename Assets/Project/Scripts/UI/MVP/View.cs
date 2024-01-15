using UnityEngine;

namespace UI.MVP
{
    public abstract class View<TP> : MonoBehaviour, IView where TP : IPresenter
    {
        protected TP Presenter { get; private set; }
        
        public virtual void Bind(IPresenter presenter)
        {
            Presenter = (TP)presenter;
        }
    }
}