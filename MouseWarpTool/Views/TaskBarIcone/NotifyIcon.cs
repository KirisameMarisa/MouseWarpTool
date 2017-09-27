using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseWarpTool
{
    public partial class NotifyIcon : Component
    {
        public enum State
        {
            Pase,
            Restart,
        }

        private State _currentState;
        public State CurrentState
        {
            get { return _currentState; }
        }


        public NotifyIcon()
        {
            InitializeComponent();

            _currentState = State.Restart;

            this.ExitCommand.Click += OnExitCommand;
            this.ControlCommand.Click += OnControlCommand;
            this.ControlCommand.Text = "Pause";
        }

        public NotifyIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void OnExitCommand(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OnControlCommand(object sender, EventArgs e)
        {
            switch (_currentState)
            {
                case State.Pase:
                    this.ControlCommand.Text = "Pause";
                    _currentState = State.Restart;
                    break;
                case State.Restart:
                    this.ControlCommand.Text = "Restart";
                    _currentState = State.Pase;
                    break;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
