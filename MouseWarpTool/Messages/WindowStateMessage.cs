using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet.Messaging;
using System.Windows;

namespace MouseWarpTool.Messages
{
    class WindowStateMessage : InteractionMessage
    {
        public WindowStateMessage() : base()
        {
        }

        public WindowStateMessage(string MessageKey) : base(MessageKey)
        {
        }

        public WindowStateMessage(string MessageKey, Point location, Point windowSize) : base(MessageKey)
        {
            this.Location = location;
            this.WindowSize = windowSize;
        }

        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new WindowStateMessage(MessageKey);
        }


        public Point Location
        {
            get { return (Point)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty LocationProperty = System.Windows.DependencyProperty.Register(nameof(Location), typeof(Point), typeof(WindowStateMessage), new System.Windows.PropertyMetadata(null));


        public Point WindowSize
        {
            get { return (Point)GetValue(WindowSizeProperty); }
            set { SetValue(WindowSizeProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty WindowSizeProperty = System.Windows.DependencyProperty.Register(nameof(WindowSize), typeof(Point), typeof(WindowStateMessage), new System.Windows.PropertyMetadata(null));

    }
}
