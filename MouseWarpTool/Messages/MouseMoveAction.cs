using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace MouseWarpTool.Messages
{
    class MouseMoveAction : InteractionMessageAction<Window>
    {
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as MouseMoveMessage;
            
            if (msg != null)
            {
                SetCursorPos((int)msg.Position.X, (int)msg.Position.Y);
            }
        }
    }
}
