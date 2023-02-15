namespace Service
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
            this.IPCServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.IPCServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // IPCServiceProcessInstaller
            // 
            this.IPCServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.IPCServiceProcessInstaller.Password = null;
            this.IPCServiceProcessInstaller.Username = null;
            // 
            // IPCServiceInstaller
            // 
            this.IPCServiceInstaller.DisplayName = "IPCService (Intelligent Payment Center)";
            this.IPCServiceInstaller.ServiceName = "IPCSERVER";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.IPCServiceProcessInstaller,
            this.IPCServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller IPCServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller IPCServiceInstaller;
    }
}