using MouseWarpTool.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MouseWarpTool
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// タスクトレイに表示するアイコン
        /// </summary>
        public NotifyIcon Notify { private set; get; }

        /// <summary>
        /// System.Windows.Application.Startup イベント を発生させます。
        /// </summary>
        /// <param name="e">イベントデータ を格納している StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.Notify = new NotifyIcon();

            ServiceLocator.Instance.InitializeServiceLocator();

            //!< プロジェクトのコンフィグファイルを読み込む
            var repo = ServiceLocator.Instance.GetInstance<ConfigRepository>();
            try
            {
                repo.Save(".config");
            }
            catch (Exception exception) when (exception is FileNotFoundException)
            {
                MessageBox.Show("Create config file and restart.", "Information");
                //!< Configファイルを自動作成してみてアプリケーションを再起動を試みる
                repo.Save(Config.DefaultConfig());
                repo.ExportXML(".config");
                System.Windows.Forms.Application.Restart();
            }
        }

        /// <summary>
        /// System.Windows.Application.Exit イベント を発生させます。
        /// </summary>
        /// <param name="e">イベントデータ を格納している ExitEventArgs</param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            this.Notify.Dispose();

            //!< 終了時に吐き出す
            var repo = ServiceLocator.Instance.GetInstance<ConfigRepository>();
            repo.ExportXML(".config");
        }
    }
}
