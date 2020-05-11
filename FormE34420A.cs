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
    public partial class FormE34420A : Form
    {
        public FormE34420A()
        {
            InitializeComponent();
        }

        public string address = null;
        private void btnSave_Click(object sender, EventArgs e)
        {
            address = txtaddress.Text;

            if (this.txtaddress.Text.Equals(""))
            {
                MessageBox.Show("设备地址不能为空", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "address", this.txtaddress.Text);
            clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "EquipmentType", "E34420A");

            MessageBox.Show("成功保存参数数据", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SysInitialzation(string _iniPath)
        {
            if (System.IO.File.Exists(_iniPath))
            {
                return;
            }
            System.IO.File.Create(_iniPath).Close();
            clsINISetting.INIWrite(_iniPath, "System", "EquipmentType", "None");
            clsINISetting.INIWrite(_iniPath, "System", "address", "None");

        }

        private void LoadInitialSetting(string _iniPath)
        {
            if (!System.IO.File.Exists(_iniPath))
            {
                SysInitialzation(_iniPath);
            }

            txtaddress.Text = clsINISetting.INIRead(_iniPath, "System", "address", "");


        }

        private void Form4980_Load(object sender, EventArgs e)
        {
            this.LoadInitialSetting(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini");
        }
    }
}
