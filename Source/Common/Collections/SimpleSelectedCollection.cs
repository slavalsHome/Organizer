using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Common.Collections
{
    public class SimpleSelectedCollection<T> : SimpleCollection<T>, ISelectedCollection
        where T : class, ISelectedCollectionItem, new()
    {
        public SimpleSelectedCollection()
        {
            CollectionChanged += OnSimpleSelectedCollectionChanged;
        }

        private void OnSimpleSelectedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && Current == null)
            {
                foreach (var newItem in e.NewItems)
                {
                    Current = (T)newItem;
                }
            }
        }

        private ISelectedCollectionItem _current;

        public ISelectedCollectionItem Current
        {
            get { return _current; }
            set
            {
                if (_current != value)
                {
                    if (_current != null)
                        _current.IsSelected = false;
                    
                    _current = value;

                    if (_current != null)
                        _current.IsSelected = true;

                    OnPropertyChanged(new PropertyChangedEventArgs("Current"));
                }
            }
        }

        protected override void OnAdd(object obj)
        {
            var item = new T();
            this.Add(item);
            Current = item;
        }

        protected override void OnRemove(object obj)
        {
            var item = obj as T;
            if (item != null)
            {
                this.Remove(item);
                if (this.Count > 0)
                    Current = this.First();
                else
                    Current = null;
            }
        }
    }
}
