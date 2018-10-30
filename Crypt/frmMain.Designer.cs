namespace Crypt
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPayload = new System.Windows.Forms.Label();
            this.txtPayload = new System.Windows.Forms.TextBox();
            this.btnBrowsePayload = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnBrowseHost = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPayload
            // 
            this.lblPayload.AutoSize = true;
            this.lblPayload.Location = new System.Drawing.Point(13, 13);
            this.lblPayload.Name = "lblPayload";
            this.lblPayload.Size = new System.Drawing.Size(45, 13);
            this.lblPayload.TabIndex = 0;
            this.lblPayload.Text = "Payload";
            // 
            // txtPayload
            // 
            this.txtPayload.Location = new System.Drawing.Point(16, 30);
            this.txtPayload.Name = "txtPayload";
            this.txtPayload.Size = new System.Drawing.Size(296, 20);
            this.txtPayload.TabIndex = 1;
            // 
            // btnBrowsePayload
            // 
            this.btnBrowsePayload.Location = new System.Drawing.Point(318, 29);
            this.btnBrowsePayload.Name = "btnBrowsePayload";
            this.btnBrowsePayload.Size = new System.Drawing.Size(75, 22);
            this.btnBrowsePayload.TabIndex = 2;
            this.btnBrowsePayload.Text = "Browse";
            this.btnBrowsePayload.UseVisualStyleBackColor = true;
            this.btnBrowsePayload.Click += new System.EventHandler(this.btnBrowsePayload_Click);
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(13, 54);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(70, 13);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host Process";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(16, 71);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(296, 20);
            this.txtHost.TabIndex = 3;
            // 
            // btnBrowseHost
            // 
            this.btnBrowseHost.Location = new System.Drawing.Point(318, 70);
            this.btnBrowseHost.Name = "btnBrowseHost";
            this.btnBrowseHost.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseHost.TabIndex = 4;
            this.btnBrowseHost.Text = "Browse";
            this.btnBrowseHost.UseVisualStyleBackColor = true;
            this.btnBrowseHost.Click += new System.EventHandler(this.btnBrowseHost_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(318, 109);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 5;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 144);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnBrowseHost);
            this.Controls.Add(this.btnBrowsePayload);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.txtPayload);
            this.Controls.Add(this.lblPayload);
            this.Name = "frmMain";
            this.Text = "Crypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPayload;
        private System.Windows.Forms.TextBox txtPayload;
        private System.Windows.Forms.Button btnBrowsePayload;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnBrowseHost;
        private System.Windows.Forms.Button btnBuild;
    }
}

