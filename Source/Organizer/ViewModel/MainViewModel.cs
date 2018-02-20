using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;
using Common.MvvmBase;

namespace Organizer.ViewModel
{
    public class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            StickerBoards = new ObservableCollection<StickerBoardViewModel>();
        }

#region SavingState


        public List<StickerBoardViewModel> StickerBoardsSaving { get; set; }

        public void Save()
        {
            StickerBoardsSaving = new List<StickerBoardViewModel>(StickerBoards);
            foreach (var stickerBoardViewModel in StickerBoardsSaving)
            {
                stickerBoardViewModel.Save();
            }
        }

        public void Load()
        {
            foreach (var stickerBoardViewModel in StickerBoardsSaving)
            {
                stickerBoardViewModel.Parent = this;
                StickerBoards.Add(stickerBoardViewModel);
                stickerBoardViewModel.Load();
            }

            if (StickerBoards.Count > 0)
                CurrentStickerBoard = StickerBoards.First();

        }

#endregion

        [XmlIgnore]
        public ObservableCollection<StickerBoardViewModel> StickerBoards { get; private set; }

        private StickerBoardViewModel _currentStickerBoard;

        [XmlIgnore]
        public StickerBoardViewModel CurrentStickerBoard
        {
            get { return _currentStickerBoard; }
            set
            {
                if (_currentStickerBoard != value)
                {
                    if (_currentStickerBoard != null)
                        _currentStickerBoard.IsSelected = false;

                    _currentStickerBoard = value;
                    
                    if (_currentStickerBoard != null)
                        _currentStickerBoard.IsSelected = true;
                    
                    OnPropertyChanged("CurrentStickerBoard");
                }
            }
        }

        #region AddBoard

        private ICommand _addBoardCommand;

        [XmlIgnore]
        public ICommand AddBoard
        {
            get
            {
                if (_addBoardCommand == null)
                {
                    _addBoardCommand = new RelayCommand(OnAddBoard, CanAddBoard);
                }
                return _addBoardCommand;
            }
        }

        private bool CanAddBoard(object obj)
        {
            return true;
        }

        private void OnAddBoard(object obj)
        {
            //Trace.WriteLine("OnAddSticker");
            var board = new StickerBoardViewModel();
            board.Parent = this;
            board.Name = "new board";
            StickerBoards.Add(board);
            CurrentStickerBoard = board;
        }

        #endregion

        #region RemoveBoard

        private ICommand _removeBoardCommand;

        [XmlIgnore]
        public ICommand RemoveBoard
        {
            get
            {
                if (_removeBoardCommand == null)
                {
                    _removeBoardCommand = new RelayCommand(OnRemoveBoard, CanRemoveBoard);
                }
                return _removeBoardCommand;
            }
        }

        private bool CanRemoveBoard(object obj)
        {
            return true;
        }

        private void OnRemoveBoard(object obj)
        {
            //Trace.WriteLine("OnAddSticker");
            var board = obj as StickerBoardViewModel;
            if (board != null)
            {
                StickerBoards.Remove(board);
                if (StickerBoards.Count > 0)
                    CurrentStickerBoard = StickerBoards.First();
                else
                    CurrentStickerBoard = null;
            }
        }

        #endregion
    }
}
