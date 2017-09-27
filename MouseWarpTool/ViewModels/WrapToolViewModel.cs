using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.Commands;
using System.Windows.Media;
using System.Collections.ObjectModel;
using MouseWarpTool.Messages;
using MouseWarpTool.Views;
using Livet.Messaging;
using Livet.Messaging.Windows;
using System.Windows;
using MouseWarpTool.Models;

namespace MouseWarpTool.ViewModels
{
    class WrapToolViewModel : ViewModel
    {
        private static int ButtonHeight = 30;

        private ObservableCollection<DisplayNode> _listCollection = new ObservableCollection<DisplayNode>();
        public ObservableCollection<DisplayNode> ListCollection
        {
            get { return this._listCollection; }
        }

        public Visual CloneVisualDesign { get; set; }

        public bool IsOpend { private set; get; } = false;

        public WrapToolViewModel()
        {
            IsOpend = true;
        }

        public void OnInitialize()
        {
            //!< メインディスプレイ情報
            var mainDisplay = Display.GetMainDisplay();
            Messenger.Raise(new WindowStateMessage("ResizeMessage", Display.GetCenterPoint(mainDisplay.Bounds), new Point(300, System.Windows.Forms.Screen.AllScreens.Length * ButtonHeight)));
            Messenger.Raise(new WindowClipMessage(true, "WindowClipMessage"));
            _listCollection.Add(new DisplayNode(0, new Point { X = (int)mainDisplay.Bounds.X, Y = (int)mainDisplay.Bounds.Y }, ButtonHeight, Messenger));

            // サブディスプレイ情報
            for (int i = 0; i < Display.GetSubDisplay().Count(); ++i)
            {
                System.Windows.Forms.Screen subDisplay = Display.GetSubDisplay().ElementAt(i);

                using (var vm = new CloneViewModel(CloneVisualDesign, Display.GetCenterPoint(subDisplay.Bounds), new Point(300, System.Windows.Forms.Screen.AllScreens.Length * ButtonHeight)))
                {
                    Messenger.Raise(new TransitionMessage(vm, "CloneWindow"));
                }
                _listCollection.Add(new DisplayNode(i + 1, new Point { X = (int)subDisplay.Bounds.X, Y = (int)subDisplay.Bounds.Y }, ButtonHeight, Messenger));
            }
        }

        public void OnClose()
        {
            IsOpend = false;
        }
    }
}
