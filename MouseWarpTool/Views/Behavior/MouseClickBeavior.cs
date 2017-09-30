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
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Send);
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Start();                       
            timer.Tick += (s, t) =>
            {
                if (sw.Elapsed.TotalMilliseconds >= designatedTime)
                {
                    sw.Stop();
                    timer.Stop();
                    if (Command != null && Command.CanExecute(sender))
                    {
                        Command.Execute(args);
                    }
                }
            };
        }
    }
}
