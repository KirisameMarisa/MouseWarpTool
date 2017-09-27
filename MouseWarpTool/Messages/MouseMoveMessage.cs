using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet.Messaging;
using System.Windows;

namespace MouseWarpTool.Messages
{
    class MouseMoveMessage : InteractionMessage
    {
        public MouseMoveMessage() : base()
        {
        }

        public MouseMoveMessage(string MessageKey) : base(MessageKey)
        {
        }

        public MouseMoveMessage(string MessageKey, Point position) : base(MessageKey)
        {
            this.Position = position;
        }

        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new MouseMoveMessage(MessageKey);
        }

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public static readonly System.Windows.DependencyProperty PositionProperty = System.Windows.DependencyProperty.Register(nameof(Position), typeof(Point), typeof(MouseMoveMessage), new System.Windows.PropertyMetadata(null));

    }
}
