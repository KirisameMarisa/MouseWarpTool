using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.Windows;
using MouseWarpTool.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseWarpTool.ViewModels
{
    class DisplayNode : Livet.NotificationObject
    {
        public DisplayNode(int displayNumber, Point location, int buttonHeight, Livet.Messaging.InteractionMessenger messenger)
        {
            _displayNumber = displayNumber;
            _location = location;
            _messenger = messenger;
            _buttonHeight = buttonHeight;
        }

        public string DisplayName
        {
            get { return "Display : " + _displayNumber; }
        }

        private int _buttonHeight;
        public int ButtonHeight
        {
            get { return _buttonHeight; }
            set { _buttonHeight = value; this.RaisePropertyChanged("ButtonHeight"); }
        }

        private Point _location;

        private Livet.Messaging.InteractionMessenger _messenger;

        private ScreenHintViewModel _screenHintViewModel;

        private int _displayNumber;

        private ViewModelCommand _command;
        public ViewModelCommand Command => _command ?? (_command = new ViewModelCommand(() =>
        {
            _messenger.Raise(new WindowClipMessage(false, "MouseClipMessage"));
            _messenger.Raise(new MouseMoveMessage("MouseMoveMessage", _location));
            _messenger.Raise(new WindowActionMessage(WindowAction.Close, "CloseMessage"));
        }));

        private ViewModelCommand _screenHintOpenCommand;
        public ViewModelCommand ScreenHintOpenCommand => _screenHintOpenCommand ?? (_screenHintOpenCommand = new ViewModelCommand(() =>
        {
            if (_screenHintViewModel == null)
            {
                _screenHintViewModel = new ScreenHintViewModel(new Point(_location.Y, _location.X), _displayNumber);
            }
            _messenger.Raise(new TransitionMessage(_screenHintViewModel, "ScreenHintOpenCommand"));
        }));

        private ViewModelCommand _screenHintCloseCommand;
        public ViewModelCommand ScreenHintCloseCommand => _screenHintCloseCommand ?? (_screenHintCloseCommand = new ViewModelCommand(() =>
        {
            if (_screenHintViewModel != null)
            {
                _screenHintViewModel.Close();
            }
        }));
    }
}
