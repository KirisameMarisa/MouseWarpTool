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
    /// WrapToolWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WrapToolWindow : Window
    {
        public WrapToolWindow()
        {
            InitializeComponent();


        }

        protected override void OnContentRendered(EventArgs e)
        {
            var vm = DataContext as ViewModels.WrapToolViewModel;
            //!< Clone対象のVisualを渡す
            vm.CloneVisualDesign = this.Window;
            //!< イベント奪い取ると発火されないのでこちらで明示的に呼ぶ
            vm.OnInitialize();
        }
    }
}
