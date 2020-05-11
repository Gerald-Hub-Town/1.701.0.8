namespace LCRTest
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.btnlogin = new System.Windows.Forms.Button();
            this.labjob = new System.Windows.Forms.Label();
            this.labpassword = new System.Windows.Forms.Label();
            this.txtjob = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnlogin
            // 
            this.btnlogin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnlogin.Location = new System.Drawing.Point(312, 239);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(88, 37);
            this.btnlogin.TabIndex = 0;
            this.btnlogin.Text = "登入";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // labjob
            // 
            this.labjob.AutoSize = true;
            this.labjob.BackColor = System.Drawing.Color.Transparent;
            this.labjob.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labjob.Location = new System.Drawing.Point(63, 116);
            this.labjob.Name = "labjob";
            this.labjob.Size = new System.Drawing.Size(66, 21);
            this.labjob.TabIndex = 1;
            this.labjob.Text = "工号:";
            // 
            // labpassword
            // 
            this.labpassword.AutoSize = true;
            this.labpassword.BackColor = System.Drawing.Color.Transparent;
            this.labpassword.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labpassword.Location = new System.Drawing.Point(63, 166);
            this.labpassword.Name = "labpassword";
            this.labpassword.Size = new System.Drawing.Size(76, 21);
            this.labpassword.TabIndex = 2;
            this.labpassword.Text = "密码：";
            // 
            // txtjob
            // 
            this.txtjob.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtjob.Location = new System.Drawing.Point(145, 110);
            this.txtjob.Name = "txtjob";
            this.txtjob.Size = new System.Drawing.Size(207, 31);
            this.txtjob.TabIndex = 1;
            this.txtjob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtjob_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtpassword.Location = new System.Drawing.Point(145, 163);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(207, 31);
            this.txtpassword.TabIndex = 4;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // btncancel
            // 
            this.btncancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btncancel.Location = new System.Drawing.Point(421, 239);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(78, 37);
            this.btncancel.TabIndex = 5;
            this.btncancel.Text = "取消";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(146, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "LCR量测系统";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(511, 288);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtjob);
            this.Controls.Add(this.labpassword);
            this.Controls.Add(this.labjob);
            this.Controls.Add(this.btnlogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LCR登入界面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Label labjob;
        private System.Windows.Forms.Label labpassword;
        private System.Windows.Forms.TextBox txtjob;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Label label1;
    }
}