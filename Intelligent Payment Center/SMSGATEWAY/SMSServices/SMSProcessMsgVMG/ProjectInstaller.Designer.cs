namespace SMSProcessMsgMT
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MTMsgProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MTMsgInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MTMsgProcessInstaller
            // 
            this.MTMsgProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.MTMsgProcessInstaller.Password = null;
            this.MTMsgProcessInstaller.Username = null;
            // 
            // MTMsgInstaller
            // 
            this.MTMsgInstaller.ServiceName = "SMSSendMTMsg";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MTMsgInstaller,
            this.MTMsgProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MTMsgProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MTMsgInstaller;
    }
}