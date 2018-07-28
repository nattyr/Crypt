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
            this.lblIcon = new System.Windows.Forms.Label();
            this.txtIcon = new System.Windows.Forms.TextBox();
            this.btnBrowseIcon = new System.Windows.Forms.Button();
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
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(13, 54);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(28, 13);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "Icon";
            // 
            // txtIcon
            // 
            this.txtIcon.Location = new System.Drawing.Point(16, 71);
            this.txtIcon.Name = "txtIcon";
            this.txtIcon.Size = new System.Drawing.Size(296, 20);
            this.txtIcon.TabIndex = 1;
            // 
            // btnBrowseIcon
            // 
            this.btnBrowseIcon.Location = new System.Drawing.Point(318, 70);
            this.btnBrowseIcon.Name = "btnBrowseIcon";
            this.btnBrowseIcon.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseIcon.TabIndex = 2;
            this.btnBrowseIcon.Text = "Browse";
            this.btnBrowseIcon.UseVisualStyleBackColor = true;
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(318, 127);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 3;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 162);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnBrowseIcon);
            this.Controls.Add(this.btnBrowsePayload);
            this.Controls.Add(this.txtIcon);
            this.Controls.Add(this.lblIcon);
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
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.TextBox txtIcon;
        private System.Windows.Forms.Button btnBrowseIcon;
        private System.Windows.Forms.Button btnBuild;
    }
}

