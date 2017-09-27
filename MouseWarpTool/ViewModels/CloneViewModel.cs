using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Livet;
using MouseWarpTool.Messages;

namespace MouseWarpTool.ViewModels
{
    class CloneViewModel : ViewModel
    {
        struct InitializeDesc
        {
            public Point Location;
            public Point WindowSize;
        }
        InitializeDesc _initializeDesc;

        public CloneViewModel(Visual visual, Point location, Point windowSize)
        {
            this.VisualDesign = visual;
            _initializeDesc.Location = location;
            _initializeDesc.WindowSize = windowSize;
        }

        private Visual _visualDesign;
        public Visual VisualDesign
        {
            get { return _visualDesign; }
            set { _visualDesign = value; }
        }

        public void OnInitialize()
        {
            Messenger.Raise(new WindowStateMessage("ResizeMessage", _initializeDesc.Location, _initializeDesc.WindowSize));
        }
    }
}
