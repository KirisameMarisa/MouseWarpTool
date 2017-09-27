using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseWarpTool.Views
{
    /// <summary>
    /// ScreenHintWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ScreenHintWindow : Window
    {
        public ScreenHintWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var vm = DataContext as ViewModels.ScreenHintViewModel;
            //!< イベント奪い取ると発火されないのでこちらで明示的に呼ぶ
            vm.OnInitialize();
        }
    }
}
