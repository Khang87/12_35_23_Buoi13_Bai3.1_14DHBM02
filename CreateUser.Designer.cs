namespace _12_35_5_14DHBM02
{
    partial class CreateUser
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
            this.lbl_Create = new System.Windows.Forms.Label();
            this.btn_cr = new System.Windows.Forms.Button();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.lbl_user = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Create
            // 
            this.lbl_Create.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Create.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Create.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Create.ForeColor = System.Drawing.Color.Black;
            this.lbl_Create.Location = new System.Drawing.Point(0, 0);
            this.lbl_Create.Name = "lbl_Create";
            this.lbl_Create.Size = new System.Drawing.Size(800, 78);
            this.lbl_Create.TabIndex = 15;
            this.lbl_Create.Text = "Create User";
            this.lbl_Create.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cr
            // 
            this.btn_cr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_cr.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_cr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_cr.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btn_cr.FlatAppearance.BorderSize = 0;
            this.btn_cr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cr.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cr.ForeColor = System.Drawing.Color.White;
            this.btn_cr.Location = new System.Drawing.Point(0, 381);
            this.btn_cr.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_cr.Name = "btn_cr";
            this.btn_cr.Size = new System.Drawing.Size(800, 69);
            this.btn_cr.TabIndex = 14;
            this.btn_cr.Text = "Create";
            this.btn_cr.UseVisualStyleBackColor = false;
            this.btn_cr.Click += new System.EventHandler(this.btn_cr_Click_1);
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(183, 195);
            this.txt_pass.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_pass.Multiline = true;
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(604, 45);
            this.txt_pass.TabIndex = 13;
            this.txt_pass.UseSystemPasswordChar = true;
            // 
            // lbl_password
            // 
            this.lbl_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_password.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_password.Location = new System.Drawing.Point(7, 195);
            this.lbl_password.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(166, 45);
            this.lbl_password.TabIndex = 17;
            this.lbl_password.Text = "Password:";
            this.lbl_password.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(183, 116);
            this.txt_user.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_user.Multiline = true;
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(604, 48);
            this.txt_user.TabIndex = 12;
            // 
            // lbl_user
            // 
            this.lbl_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_user.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_user.Location = new System.Drawing.Point(6, 116);
            this.lbl_user.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(166, 48);
            this.lbl_user.TabIndex = 16;
            this.lbl_user.Text = "User:";
            this.lbl_user.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_Create);
            this.Controls.Add(this.btn_cr);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.lbl_user);
            this.Name = "CreateUser";
            this.Text = "CreateUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Create;
        private System.Windows.Forms.Button btn_cr;
        public System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label lbl_password;
        public System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label lbl_user;
    }
}