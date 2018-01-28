namespace HllmUp
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Username = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginButt = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.Update = new System.Windows.Forms.Timer(this.components);
            this.ErrorText = new System.Windows.Forms.Label();
            this.LogoutButt = new System.Windows.Forms.Button();
            this.ContextEnable = new System.Windows.Forms.CheckBox();
            this.AutostartEnable = new System.Windows.Forms.CheckBox();
            this.AutostartLabel = new System.Windows.Forms.Label();
            this.ContextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(12, 25);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(156, 20);
            this.Username.TabIndex = 0;
            this.Username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Username_KeyDown);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(12, 68);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(156, 20);
            this.Password.TabIndex = 1;
            this.Password.UseSystemPasswordChar = true;
            this.Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Password_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // LoginButt
            // 
            this.LoginButt.Location = new System.Drawing.Point(12, 140);
            this.LoginButt.Name = "LoginButt";
            this.LoginButt.Size = new System.Drawing.Size(75, 23);
            this.LoginButt.TabIndex = 5;
            this.LoginButt.Text = "Login/Apply";
            this.LoginButt.UseVisualStyleBackColor = true;
            this.LoginButt.Click += new System.EventHandler(this.LoginButt_Click);
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.ForeColor = System.Drawing.Color.Red;
            this.Status.Location = new System.Drawing.Point(186, 28);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(73, 13);
            this.Status.TabIndex = 6;
            this.Status.Text = "Not logged in!";
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(186, 52);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(10, 13);
            this.UsernameLabel.TabIndex = 7;
            this.UsernameLabel.Text = "-";
            // 
            // Update
            // 
            this.Update.Interval = 1000;
            this.Update.Tick += new System.EventHandler(this.Update_Tick);
            // 
            // ErrorText
            // 
            this.ErrorText.AutoSize = true;
            this.ErrorText.ForeColor = System.Drawing.Color.Firebrick;
            this.ErrorText.Location = new System.Drawing.Point(174, 145);
            this.ErrorText.Name = "ErrorText";
            this.ErrorText.Size = new System.Drawing.Size(10, 13);
            this.ErrorText.TabIndex = 8;
            this.ErrorText.Text = "-";
            // 
            // LogoutButt
            // 
            this.LogoutButt.Enabled = false;
            this.LogoutButt.Location = new System.Drawing.Point(93, 140);
            this.LogoutButt.Name = "LogoutButt";
            this.LogoutButt.Size = new System.Drawing.Size(75, 23);
            this.LogoutButt.TabIndex = 9;
            this.LogoutButt.TabStop = false;
            this.LogoutButt.Text = "Logout";
            this.LogoutButt.UseVisualStyleBackColor = true;
            this.LogoutButt.Click += new System.EventHandler(this.LogoutButt_Click);
            // 
            // ContextEnable
            // 
            this.ContextEnable.AutoSize = true;
            this.ContextEnable.Location = new System.Drawing.Point(15, 94);
            this.ContextEnable.Name = "ContextEnable";
            this.ContextEnable.Size = new System.Drawing.Size(135, 17);
            this.ContextEnable.TabIndex = 10;
            this.ContextEnable.Text = "Explorer right-click stuff";
            this.ContextEnable.UseVisualStyleBackColor = true;
            // 
            // AutostartEnable
            // 
            this.AutostartEnable.AutoSize = true;
            this.AutostartEnable.Location = new System.Drawing.Point(15, 117);
            this.AutostartEnable.Name = "AutostartEnable";
            this.AutostartEnable.Size = new System.Drawing.Size(68, 17);
            this.AutostartEnable.TabIndex = 11;
            this.AutostartEnable.Text = "Autostart";
            this.AutostartEnable.UseVisualStyleBackColor = true;
            // 
            // AutostartLabel
            // 
            this.AutostartLabel.AutoSize = true;
            this.AutostartLabel.Location = new System.Drawing.Point(186, 118);
            this.AutostartLabel.Name = "AutostartLabel";
            this.AutostartLabel.Size = new System.Drawing.Size(80, 13);
            this.AutostartLabel.TabIndex = 12;
            this.AutostartLabel.Text = "Autostart: False";
            // 
            // ContextLabel
            // 
            this.ContextLabel.AutoSize = true;
            this.ContextLabel.Location = new System.Drawing.Point(186, 95);
            this.ContextLabel.Name = "ContextLabel";
            this.ContextLabel.Size = new System.Drawing.Size(100, 13);
            this.ContextLabel.TabIndex = 13;
            this.ContextLabel.Text = "Contextmenu: False";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 176);
            this.Controls.Add(this.ContextLabel);
            this.Controls.Add(this.AutostartLabel);
            this.Controls.Add(this.AutostartEnable);
            this.Controls.Add(this.ContextEnable);
            this.Controls.Add(this.LogoutButt);
            this.Controls.Add(this.ErrorText);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.LoginButt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginButt;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Timer Update;
        private System.Windows.Forms.Label ErrorText;
        private System.Windows.Forms.Button LogoutButt;
        private System.Windows.Forms.CheckBox ContextEnable;
        private System.Windows.Forms.CheckBox AutostartEnable;
        private System.Windows.Forms.Label AutostartLabel;
        private System.Windows.Forms.Label ContextLabel;
    }
}

