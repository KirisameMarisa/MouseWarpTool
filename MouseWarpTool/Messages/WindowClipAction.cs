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
    class WindowClipAction : InteractionMessageAction<Window>
    {
        protected override void InvokeAction(InteractionMessage message)
        {
            var msg = message as WindowClipMessage;
            
            if (msg != null)
            {
                System.Drawing.Rectangle bounds = System.Drawing.Rectangle.Empty;
                if (msg.IsClip)
                {
                    bounds = new System.Drawing.Rectangle
                    {
                        X = (int)this.AssociatedObject.Left,
                        Y = (int)this.AssociatedObject.Top,
                        Width = (int)this.AssociatedObject.Width,
                        Height = (int)this.AssociatedObject.Height
                    };
                }
                System.Windows.Forms.Cursor.Clip = bounds;
            }
        }
    }
}
