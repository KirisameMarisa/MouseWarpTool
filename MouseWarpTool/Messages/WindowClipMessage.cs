using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet.Messaging;
using System.Drawing;

namespace MouseWarpTool.Messages
{
    class WindowClipMessage : InteractionMessage
    {
        public bool IsClip;

        public WindowClipMessage() : base()
        {
        }

        public WindowClipMessage(string MessageKey) : base(MessageKey)
        {
        }

        public WindowClipMessage(bool isClip, string MessageKey) : base(MessageKey)
        {
            IsClip = isClip;
        }

        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new WindowClipMessage(MessageKey);
        }
    }
}
