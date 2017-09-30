using Gma.System.MouseKeyHook;
using MouseWarpTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace MouseWarpTool.Views.Behavior
{
    class MouseClickBeavior : Behavior<FrameworkElement>
    {
        private IKeyboardMouseEvents _globalHook = Hook.GlobalEvents();
        private System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Normal);

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

            _timer.Stop();
            _stopWatch.Reset();
        }

        //!< 1s = 1000ms
        //!< IgnitionTime = Seconds
        public void OnMouseDown(object sender, MouseEventExtArgs args)
        {
            var config = ServiceLocator.Instance.GetInstance<ConfigRepository>().GetConfig();

            if (System.Windows.Forms.MouseButtons.Middle != args.Button)
            {
                return;
            }

            var designatedTime = TimeSpan.FromSeconds(config.IgnitionTime).TotalMilliseconds;
            //ストップウォッチを開始する
            _stopWatch.Start();
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Start();                       
            _timer.Tick += (s, t) =>
            {
                if (_stopWatch.Elapsed.TotalMilliseconds >= designatedTime)
                {
                    _stopWatch.Reset();
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
