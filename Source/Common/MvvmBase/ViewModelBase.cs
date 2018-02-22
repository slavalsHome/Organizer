using System;

namespace Common.MvvmBase
{
    public abstract class ViewModelBase : BindableObject
    {
        protected ViewModelBase(/*IDataProvider model*/)
        {
           // Model = model;
        }

        //protected IDataProvider Model { get; private set; }

        public bool IsClosed { get; private set; }

        public virtual void Close()
        {
            IsClosed = true;
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        /// <summary>
        /// Дата вью модель закрыта
        /// </summary>
        public event EventHandler<EventArgs> Closed;
    }
}
