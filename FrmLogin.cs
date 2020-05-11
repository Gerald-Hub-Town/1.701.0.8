using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCRTest
{
    public partial class FrmLogin : Form
    {
        public  string userid ;
        DBconnetion dbconnection = new DBconnetion();
      
        public FrmLogin()
        {
            InitializeComponent();

            
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            btnlogin.BackColor = Color.Green;
            if (string.IsNullOrEmpty(txtjob.Text))
            {

                MessageBox.Show("工号不能为空");
                txtjob.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtpassword.Text))
            {

                MessageBox.Show("密码不能为空");
                txtpassword.Focus();
                return;

            }

            if (!checkusername(txtjob.Text, txtpassword.Text))
            {
                MessageBox.Show("用户名和密码不对(user name or password error!)");
                return;

            }

            userid = txtjob.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


  

        private void label1_Click(object sender, EventArgs e)
        {

        }

      

      

        private void txtjob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {

                txtpassword.Focus();

            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {

                btnlogin.Focus();
                btnlogin.BackColor = Color.Green;

            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btnlogin.BackColor = Color.Green;
            this.Close();
        }


        public bool checkusername(string job, string password)
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;

                cmd = @" select userid,password,role,role_level from lcr.LCR_USER  where  userid=:id   and  password=:pwd  ";  /// 
                DataTable dt = dbconnection.repository.FindTable(cmd, new { id = job, pwd = password });////, 
                if (dt.Rows.Count <= 0)
                {
                    flag = false;

                }

            }
            catch (Exception ex)
            {

                flag = false;
            }
            return flag;
        }
    }
}
