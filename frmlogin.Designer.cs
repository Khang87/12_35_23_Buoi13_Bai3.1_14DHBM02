namespace _12_35_5_14DHBM02
{
    partial class frmlogin
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
            this.lbl_login = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.lbl_user = new System.Windows.Forms.Label();
            this.txt_sid = new System.Windows.Forms.TextBox();
            this.lbl_sid = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.lbl_port = new System.Windows.Forms.Label();
            this.txt_host = new System.Windows.Forms.TextBox();
            this.lbl_host = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_login
            // 
            this.lbl_login.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_login.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_login.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login.ForeColor = System.Drawing.Color.Black;
            this.lbl_login.Location = new System.Drawing.Point(0, 0);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(800, 78);
            this.lbl_login.TabIndex = 18;
            this.lbl_login.Text = "FORM LOGIN";
            this.lbl_login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_login.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_login.FlatAppearance.BorderSize = 0;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_login.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.White;
            this.btn_login.Location = new System.Drawing.Point(0, 381);
            this.btn_login.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(497, 69);
            this.btn_login.TabIndex = 17;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click_1);
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(226, 327);
            this.txt_password.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_password.Multiline = true;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(560, 45);
            this.txt_password.TabIndex = 16;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // lbl_password
            // 
            this.lbl_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_password.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_password.Location = new System.Drawing.Point(7, 327);
            this.lbl_password.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(209, 45);
            this.lbl_password.TabIndex = 23;
            this.lbl_password.Text = "Password:";
            this.lbl_password.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(226, 266);
            this.txt_user.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_user.Multiline = true;
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(560, 45);
            this.txt_user.TabIndex = 15;
            // 
            // lbl_user
            // 
            this.lbl_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_user.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_user.Location = new System.Drawing.Point(7, 266);
            this.lbl_user.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(209, 45);
            this.lbl_user.TabIndex = 22;
            this.lbl_user.Text = "User:";
            this.lbl_user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_sid
            // 
            this.txt_sid.Location = new System.Drawing.Point(226, 205);
            this.txt_sid.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_sid.Multiline = true;
            this.txt_sid.Name = "txt_sid";
            this.txt_sid.Size = new System.Drawing.Size(560, 45);
            this.txt_sid.TabIndex = 14;
            // 
            // lbl_sid
            // 
            this.lbl_sid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_sid.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sid.Location = new System.Drawing.Point(7, 205);
            this.lbl_sid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_sid.Name = "lbl_sid";
            this.lbl_sid.Size = new System.Drawing.Size(209, 45);
            this.lbl_sid.TabIndex = 21;
            this.lbl_sid.Text = "Sid:";
            this.lbl_sid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(226, 145);
            this.txt_port.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_port.Multiline = true;
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(560, 45);
            this.txt_port.TabIndex = 13;
            // 
            // lbl_port
            // 
            this.lbl_port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_port.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_port.Location = new System.Drawing.Point(7, 145);
            this.lbl_port.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(209, 45);
            this.lbl_port.TabIndex = 20;
            this.lbl_port.Text = "Port:";
            this.lbl_port.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_host
            // 
            this.txt_host.Location = new System.Drawing.Point(226, 84);
            this.txt_host.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_host.Multiline = true;
            this.txt_host.Name = "txt_host";
            this.txt_host.Size = new System.Drawing.Size(560, 45);
            this.txt_host.TabIndex = 12;
            // 
            // lbl_host
            // 
            this.lbl_host.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_host.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_host.Location = new System.Drawing.Point(7, 84);
            this.lbl_host.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_host.Name = "lbl_host";
            this.lbl_host.Size = new System.Drawing.Size(209, 45);
            this.lbl_host.TabIndex = 19;
            this.lbl_host.Text = "Host:";
            this.lbl_host.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_create
            // 
            this.btn_create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_create.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_create.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_create.FlatAppearance.BorderSize = 0;
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_create.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create.ForeColor = System.Drawing.Color.White;
            this.btn_create.Location = new System.Drawing.Point(502, 381);
            this.btn_create.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(284, 69);
            this.btn_create.TabIndex = 24;
            this.btn_create.Text = "Create User";
            this.btn_create.UseVisualStyleBackColor = false;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // frmlogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.lbl_login);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.lbl_user);
            this.Controls.Add(this.txt_sid);
            this.Controls.Add(this.lbl_sid);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.lbl_port);
            this.Controls.Add(this.txt_host);
            this.Controls.Add(this.lbl_host);
            this.Name = "frmlogin";
            this.Text = "frmlogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.Button btn_login;
        public System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label lbl_password;
        public System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label lbl_user;
        public System.Windows.Forms.TextBox txt_sid;
        private System.Windows.Forms.Label lbl_sid;
        public System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label lbl_port;
        public System.Windows.Forms.TextBox txt_host;
        private System.Windows.Forms.Label lbl_host;
        private System.Windows.Forms.Button btn_create;
    }
}