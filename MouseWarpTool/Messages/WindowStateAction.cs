using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;

namespace MouseWarpTool.Messages
{
    class WindowStateAction : InteractionMessageAction<Window>
    {
        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as WindowStateMessage;
            
            if (msg != null)
            {
                this.AssociatedObject.Top = msg.Location.X;
                this.AssociatedObject.Left = msg.Location.Y;

                this.AssociatedObject.Width = msg.WindowSize.X;
                this.AssociatedObject.Height = msg.WindowSize.Y;
            }
        }
    }
}
