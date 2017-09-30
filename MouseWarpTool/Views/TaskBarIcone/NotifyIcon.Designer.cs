namespace MouseWarpTool
{
    partial class NotifyIcon
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIcon));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ControlCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingOpenCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MouseWarpTool";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ControlCommand,
            this.SettingOpenCommand,
            this.ExitCommand});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ControlCommand
            // 
            this.ControlCommand.Name = "ControlCommand";
            this.ControlCommand.Size = new System.Drawing.Size(179, 22);
            this.ControlCommand.Text = "toolStripMenuItem1";
            // 
            // ExitCommand
            // 
            this.ExitCommand.Name = "ExitCommand";
            this.ExitCommand.Size = new System.Drawing.Size(179, 22);
            this.ExitCommand.Text = "Exit";
            // 
            // SettingOpenCommand
            // 
            this.SettingOpenCommand.Name = "SettingOpenCommand";
            this.SettingOpenCommand.Size = new System.Drawing.Size(179, 22);
            this.SettingOpenCommand.Text = "Setting";
            this.contextMenuStrip1.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitCommand;
        private System.Windows.Forms.ToolStripMenuItem ControlCommand;
        private System.Windows.Forms.ToolStripMenuItem SettingOpenCommand;
    }
}
