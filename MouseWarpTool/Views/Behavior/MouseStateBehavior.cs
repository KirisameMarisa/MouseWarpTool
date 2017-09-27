using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace MouseWarpTool.Views.Behavior
{
    class MouseStateBehavior : Behavior<Button>
    {
        public ICommand EnterCommand
        {
            get { return (ICommand)GetValue(EnterCommandProperty); }
            set { SetValue(EnterCommandProperty, value); }
        }
        public static readonly DependencyProperty EnterCommandProperty =
            DependencyProperty.RegisterAttached("EnterCommand", typeof(ICommand), typeof(MouseStateBehavior), new UIPropertyMetadata(null));

        public ICommand LeaveCommand
        {
            get { return (ICommand)GetValue(LeaveCommandProperty); }
            set { SetValue(LeaveCommandProperty, value); }
        }
        public static readonly DependencyProperty LeaveCommandProperty =
            DependencyProperty.RegisterAttached("LeaveCommand", typeof(ICommand), typeof(MouseStateBehavior), new UIPropertyMetadata(null));


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeave += OnMouseLeave;
            this.AssociatedObject.MouseEnter += OnMouseEnter;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeave += OnMouseLeave;
            this.AssociatedObject.MouseEnter -= OnMouseEnter;
        }

        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            var control = sender as Button;
            if (control == null)
            {
                return;
            }

            if (LeaveCommand != null && LeaveCommand.CanExecute(sender))
            {
                LeaveCommand.Execute(e);
            }   
        }

        public void OnMouseEnter(object sender, MouseEventArgs e)
        {
            var control = sender as Button;
            if (control == null)
            {
                return;
            }

            if (EnterCommand != null && EnterCommand.CanExecute(sender))
            {
                EnterCommand.Execute(e);
            }
        }
    }
}
