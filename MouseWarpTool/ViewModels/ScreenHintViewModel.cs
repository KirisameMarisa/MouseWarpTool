using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Messaging.Windows;
using System.Windows;
using MouseWarpTool.Messages;

namespace MouseWarpTool.ViewModels
{
    class ScreenHintViewModel : ViewModel
    {
        private Point _location;

        private string _number;
        public string Number
        {
            get { return _number; }
            set { _number = value; this.RaisePropertyChanged("Number"); }
        }

        public ScreenHintViewModel(Point location, int displayNumber)
        {
            _location = location;
            _number = displayNumber.ToString();
        }

        public void OnInitialize()
        {
            Messenger.Raise(new WindowStateMessage("ResizeMessage", _location, new Point(300, 300)));
        }

        public void Close()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "CloseMessage"));
        }

    }
}
