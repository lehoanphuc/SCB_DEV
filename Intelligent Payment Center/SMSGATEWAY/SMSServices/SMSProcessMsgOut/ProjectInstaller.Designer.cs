namespace SMSProcessMsgOut
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
            this.SMSOutProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SMSProcessInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SMSOutProcessInstaller
            // 
            this.SMSOutProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.SMSOutProcessInstaller.Password = null;
            this.SMSOutProcessInstaller.Username = null;
            // 
            // SMSProcessInstaller
            // 
            this.SMSProcessInstaller.DisplayName = "SMSProcessOut";
            this.SMSProcessInstaller.ServiceName = "SMSProcessOut";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SMSOutProcessInstaller,
            this.SMSProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SMSOutProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SMSProcessInstaller;
    }
}