using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LCRTest
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
        }
            public void SysInitialzation(string _iniPath)
            {
                if (System.IO.File.Exists(_iniPath))
                {
                    return;
                }
                System.IO.File.Create(_iniPath).Close();
                clsINISetting.INIWrite(_iniPath, "System", "EquipmentType", "None");
                clsINISetting.INIWrite(_iniPath, "System", "PortName", "COM1");
                clsINISetting.INIWrite(_iniPath, "System", "BaudRate", "9600");
                clsINISetting.INIWrite(_iniPath, "System", "StopBits", "1");
                clsINISetting.INIWrite(_iniPath, "System", "DataBits", "8");
                clsINISetting.INIWrite(_iniPath, "System", "Parity", "None");
                clsINISetting.INIWrite(_iniPath, "System", "DtrEnable", "None");

             }
            private void LoadInitialSetting(string _iniPath)
            {
                if (!System.IO.File.Exists(_iniPath))
                {
                    SysInitialzation(_iniPath);
                }
                string _str = string.Empty;
                _str = clsINISetting.INIRead(_iniPath, "System", "PortName", "");
                if (_str == "")
                {
                    this.cmbPortName.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < cmbPortName.Items.Count; i++)
                    {
                        if (cmbPortName.Items[i].ToString().ToUpper() == _str.ToUpper())
                        {
                            this.cmbPortName.SelectedIndex = i;
                            break;
                        }
                    }
                }
                _str = clsINISetting.INIRead(_iniPath, "System", "BaudRate", "");
                if (_str == "")
                {
                    this.cmbBaudBits.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < cmbBaudBits.Items.Count; i++)
                    {
                        if (cmbBaudBits.Items[i].ToString().ToUpper() == _str.ToUpper())
                        {
                            this.cmbBaudBits.SelectedIndex = i;
                            break;
                        }
                    }
                }
                _str = clsINISetting.INIRead(_iniPath, "System", "StopBits", "");
                if (_str == "")
                {
                    this.cmbStopBits.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < cmbStopBits.Items.Count; i++)
                    {
                        if (cmbStopBits.Items[i].ToString().ToUpper() == _str.ToUpper())
                        {
                            this.cmbStopBits.SelectedIndex = i;
                            break;
                        }
                    }
                }
                _str = clsINISetting.INIRead(_iniPath, "System", "DataBits", "");
                if (_str == "")
                {
                    this.cmbDatabits.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < cmbDatabits.Items.Count; i++)
                    {
                        if (cmbDatabits.Items[i].ToString().ToUpper() == _str.ToUpper())
                        {
                            this.cmbDatabits.SelectedIndex = i;
                            break;
                        }
                    }
                }
                _str = clsINISetting.INIRead(_iniPath, "System", "Parity", "");
                if (_str == "")
                {
                    this.cmbParity.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < cmbParity.Items.Count; i++)
                    {
                        if (cmbParity.Items[i].ToString().ToUpper() == _str.ToUpper())
                        {
                            this.cmbParity.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            private void FrmConfig_Load(object sender, EventArgs e)
            {
                this.cmbPortName.DataSource = System.IO.Ports.SerialPort.GetPortNames();
                this.LoadInitialSetting(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini");
            }

            private void button1_Click(object sender, EventArgs e)
            {
                ///this.cmbPortName.Text = "COM3";  ///调试代码
                 if (this.cmbPortName.Text.Equals(""))
                {
                    //MessageBox.Show("串口号不能为空!", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.cmbPortName.Focus();
                    //return;
                }
                if (this.cmbBaudBits.Text.Equals(""))
                {
                    MessageBox.Show("波特率不能为空!", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbBaudBits.Focus();
                    return;
                }
                if (this.cmbStopBits.Text.Equals(""))
                {
                    MessageBox.Show("停止位不能为空!", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbStopBits.Focus();
                    return;
                }
                if (this.cmbDatabits.Text.Equals(""))
                {
                    MessageBox.Show("数据位不能为空!", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbDatabits.Focus();
                    return;
                }
                if (this.cmbParity.Text.Equals(""))
                {
                    MessageBox.Show("效验位不能为空!", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbParity.Focus();
                    return;
                }

           


                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "PortName", this.cmbPortName.Text);
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "BaudRate", this.cmbBaudBits.Text);
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "StopBits", this.cmbStopBits.Text);
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "DataBits", this.cmbDatabits.Text);
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "Parity", this.cmbParity.Text);
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "DtrEnable", Convert.ToString(this.ckbdtr.Checked));
                clsINISetting.INIWrite(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini", "System", "EquipmentType", "34420A");
            ///EquipmentType

            MessageBox.Show("成功保存参数数据", "保存配置", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void FrmConfig_Load_1(object sender, EventArgs e)
        {
            this.cmbPortName.DataSource = System.IO.Ports.SerialPort.GetPortNames();
        }
    }
}