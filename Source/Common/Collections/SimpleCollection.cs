using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.MvvmBase;
using Common.MvvmBase.Dialogs;

namespace Common.Collections
{
    public class SimpleCollection<T> : ObservableCollection<T>, ICollection
        where T : class, ICollectionItem, new()
    {
        public SimpleCollection()
        {
            CollectionChanged += OnSimpleCollectionChanged;
        }

        [XmlIgnore]
        public INotifyPropertyChanged ParentViewModel { get; set; }

        private void OnSimpleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    ((ICollectionItem) newItem).ParentCollection = this;
                }
            }
        }

        #region Add Item

        private ICommand _addCommand;

        [XmlIgnore]
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(OnAdd);
                }
                return _addCommand;
            }
        }


        protected virtual void OnAdd(object obj)
        {
            var item = new T();
            this.Add(item);
        }

        #endregion

        #region Remove Item

        private ICommand _removeCommand;

        [XmlIgnore]
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(OnRemove);
                }
                return _removeCommand;
            }
        }

        protected virtual void OnRemove(object obj)
        {
            var dialogFactory = ParentViewModel as IDialogFactory;
            if (dialogFactory != null)
            {
                IDialogService dialogService;
                INotifyPropertyChanged vmodel;
                INotifyPropertyChanged parent;
                if (dialogFactory.GetDialog("OnRemove", obj, out dialogService, out vmodel, out parent))
                {
                    bool? res = dialogService.ShowDialog(parent, vmodel);
                    if (!res.HasValue || !res.Value)
                        return;
                }
            }

            var item = obj as T;
            if (item != null)
                this.Remove(item);
        }

        #endregion


    }
}
