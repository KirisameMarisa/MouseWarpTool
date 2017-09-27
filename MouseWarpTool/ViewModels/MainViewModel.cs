using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using System.Windows.Media;
using Livet.Messaging;
using System.Drawing;
using System.Threading;
using MouseWarpTool.Views;

namespace MouseWarpTool.ViewModels
{
    class MainViewModel : ViewModel
    {
        private WrapToolViewModel _wrapToolViewModel;

        public MainViewModel()
        {
        }

        private ListenerCommand<object> _executeCommand;
        public ListenerCommand<object> ExecuteCommand
        {
            get
            {
                if (_executeCommand == null)
                {
                    _executeCommand = new ListenerCommand<object>((object sender) =>
                    {
                        //!< Pauseなら実行しない
                        if(((App)App.Current).Notify.CurrentState == NotifyIcon.State.Pase)
                        {
                            return;
                        }

                        //!< すでに開いてるなら開かない
                        if(_wrapToolViewModel != null && _wrapToolViewModel.IsOpend)
                        {
                            return;
                        }

                        using (_wrapToolViewModel = new WrapToolViewModel())
                        {
                            Messenger.Raise(new TransitionMessage(_wrapToolViewModel, "WrapToolWindow"));
                        }
                    });
                }              
                return _executeCommand;
            }
        }
    }
}
