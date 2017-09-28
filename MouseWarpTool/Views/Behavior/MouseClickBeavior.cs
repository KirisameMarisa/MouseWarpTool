using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MouseWarpTool.Views.Behavior
{
    class MouseClickBeavior : Behavior<FrameworkElement>
    {
        private IKeyboardMouseEvents _globalHook = Hook.GlobalEvents();

        private int _count = 0;

        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// マウスクリックした時に実行されるコマンド
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseClickBeavior), new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this._globalHook.MouseUpExt += OnMouseUp;
            this._globalHook.MouseDownExt += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this._globalHook.MouseUpExt -= OnMouseUp;
            this._globalHook.MouseDownExt -= OnMouseDown;
        }

        public void OnMouseUp(object sender, MouseEventExtArgs args)
        {
            
            if (System.Windows.Forms.MouseButtons.Middle != args.Button)
            {
                return;
            }

            _count = 0;
            _timer.Stop();
        }

        public void OnMouseDown(object sender, MouseEventExtArgs args)
        {
            if (System.Windows.Forms.MouseButtons.Middle != args.Button)
            {
                return;
            }

            _timer.Interval = 1;
            _timer.Start();
            _timer.Tick += (s, t) =>
            {
                _count++;
                if (_count >= 1000)
                {
                    _count = 0;
                    _timer.Stop();
                    if (Command != null && Command.CanExecute(sender))
                    {
                        Command.Execute(args);
                    }
                }
            };
        }
    }
}
