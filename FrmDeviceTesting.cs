using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OracleClient;
using SQLBuilder.Repositories;
using ATE;
using TWFramwork.Common;
using TWFramwork.Net.Tcp;
using TWFramwork.Win;
using System.Threading;

namespace LCRTest
{
    public partial class FrmDeviceTesting : Form
    {
        private bool az;
        private int Ax;
       
        SaveSettings setting = new SaveSettings();
        private delegate void DataReceive(string A_0);
        clsLCRMachineControl lcrcontrol = new clsLCRMachineControl();

        public OracleRepository repository = new OracleRepository("Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = ksmesh-scan.luxshare.com.cn)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ksmesh)  )  );Persist Security Info=True;User ID=lcr;Password=lcr;")
        {
            IsEnableFormat = false,
            SqlIntercept = (sql, parameter) =>
            {
                return null;
            }
        };


        public FrmDeviceTesting()
        {
            InitializeComponent();
        }
        private void IniStatus()
        {
            this.n_SelectedIndexChanged(null, null);
            this.h_SelectedIndexChanged(null, null);
            this.g_SelectedIndexChanged(null, null);
            this.m_SelectedIndexChanged(null, null);
            this.i_SelectedIndexChanged(null, null);
            this.k_CheckedChanged(null, null);
            this.l_CheckedChanged(null, null);
        }
        private void openCOM()
        {
            if (!this.ab.IsOpen)
            {
                try
                {
                    this.IniStatus();
                    this.ab.DataReceived -= new SerialDataReceivedEventHandler(this.ab_DataReceived);
                    this.ab.DataReceived += new SerialDataReceivedEventHandler(this.ab_DataReceived);
                    this.ab.Encoding = Encoding.Default;
                    this.ab.Open();
                    if (this.ab.IsOpen)
                    {
                        this.OpenState();
                    }
                    else
                    {
                        this.CloseState();
                    }
                }
                catch(Exception ex)
                {

                    MessageBox.Show(ex.Message, "打开串口失败", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    //this.ak.Text = "打开串口失败!";
                }
            }
        }
        private void stringReceived(string A_0)
        {
            try
            {
                if (this.textBox1.InvokeRequired)
                {
                    DataReceive method = new DataReceive(this.stringReceived);
                    base.Invoke(method, new object[] { A_0 });
                }
                else
                {
                    this.textBox1.SuspendLayout();
                    if ((A_0.Length == 1) && (A_0[0] == '\b'))
                    {
                        if (this.textBox1.Text.Length > 0)
                        {
                            this.textBox1.SelectionStart = this.textBox1.Text.Length - 1;
                            this.textBox1.SelectionLength = 1;
                            this.textBox1.SelectedText = "";
                        }
                    }
                    else
                    {
                        this.textBox1.AppendText(A_0);
                        decimal testvalue;
                       /// MessageAnalysis(this.textBox1.Text,out testvalue);
                       //// this.textBox1.AppendText(testvalue.ToString());



                    }
                    if (this.textBox1.Text.Length > 0x186a0)
                    {
                        this.textBox1.Text = this.textBox1.Text.Substring(0xc350, this.textBox1.Text.Length - 0xc350);
                    }
                    this.textBox1.ResumeLayout(false);
                }
            }
            catch
            {
            }
        }
        private void ab_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
            az = true;
            try
            {
                 
                    this.Ax += this.ab.BytesToRead;
                    //this.b(this.ab.ReadExisting());
                    this.stringReceived(this.ab.ReadExisting());
                
                    this.DataReceiveControl(this.Ax.ToString());
            }
            catch
            {
            }
            az = false;

        }
        private void DataReceiveControl(string A_0)
        {
            try
            {
                if (this.u.InvokeRequired)
                {
                    DataReceive method = new DataReceive(this.DataReceiveControl);
                    base.Invoke(method, new object[] { A_0 });
                }
                else
                {
                    this.u.Text = A_0;
                }
            }
            catch
            {
            }
        }
        private void ClosedCOM()
        {
            if (this.ab.IsOpen)
            {
                try
                {
                    this.ab.DataReceived -= new SerialDataReceivedEventHandler(this.ab_DataReceived);
                    while (this.az)
                    {
                        Application.DoEvents();
                    }
                    this.ab.Close();
                    if (this.ab.IsOpen)
                    {
                        this.OpenState();
                    }
                    else
                    {
                        this.CloseState();
                    }
                }
                catch
                {
                    //this.ak.Text = "关闭串口失败!";
                }
            }
        }
        private void j_Click(object sender, EventArgs e)
        {
            if (this.j.Text == "打开串口")
            {
                this.openCOM();
            }
            else
            {
                this.ClosedCOM();
            }

        }

        private void n_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ab.IsOpen)
            {
                this.ClosedCOM();
                this.ab.PortName = this.n.Text;
                this.openCOM();
            }
            else
            {
                this.ab.PortName = this.n.Text;
            }
        }

        private void h_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ab.BaudRate = Convert.ToInt32(this.h.Text);
            }
            catch
            {
                // this.ak.Text = "波特率配置错误!";
            }
            this.y.Text = this.ab.BaudRate.ToString();

        }

        private void g_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ab.DataBits = Convert.ToInt32(this.g.Text);
        }

        private void m_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.m.Text == "0")
                {
                    this.ab.StopBits = StopBits.None;
                }
                else if (this.m.Text == "1")
                {
                    this.ab.StopBits = StopBits.One;
                }
                else if (this.m.Text == "2")
                {
                    this.ab.StopBits = StopBits.OnePointFive;
                }
                else if (this.m.Text == "3")
                {
                    this.ab.StopBits = StopBits.Two;
                }
            }
            catch
            {
                //this.ak.Text = "停止位配置错误!";
            }
        }

        private void i_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.i.Text == "None")
                {
                    this.ab.Parity = Parity.None;
                }
                else if (this.i.Text == "Odd")
                {
                    this.ab.Parity = Parity.Odd;
                }
                else if (this.i.Text == "Even")
                {
                    this.ab.Parity = Parity.Even;
                }
                else if (this.i.Text == "Mark")
                {
                    this.ab.Parity = Parity.Mark;
                }
                else if (this.i.Text == "Space")
                {
                    this.ab.Parity = Parity.Space;
                }
            }
            catch
            {
                //this.ak.Text = "校验位配置错误!";
            }
        }
        private void OpenState()
        {
            this.j.Text = "关闭串口";
            this.aa.ForeColor = Color.Lime;
            this.z.Text = this.ab.PortName + "已打开";
            this.y.Text = this.ab.BaudRate.ToString();
        }

        private void CloseState()
        {
            this.j.Text = "打开串口";
            this.aa.ForeColor = Color.DarkGray;
            this.z.Text = this.ab.PortName + "已关闭";
            this.y.Text = this.ab.BaudRate.ToString();
        }

        private void l_CheckedChanged(object sender, EventArgs e)
        {
            this.ab.DtrEnable = this.l.Checked;
        }

        private void k_CheckedChanged(object sender, EventArgs e)
        {
            this.ab.RtsEnable = this.k.Checked;
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.textBox3.Text = clsASCIITransfer.StringToHexString(this.textBox2.Text+"\n\r", Encoding.ASCII).ToUpper();
        //    byte[] bytes;
        //    try
        //    {
        //        bytes = new byte[this.textBox3.Text.Length / 3];
        //        for (int i = 0; i < this.textBox3.Text.Length; i += 3)
        //        {
        //            try
        //            {
        //                bytes[i / 3] = Convert.ToByte(this.textBox3.Text.Substring(i, 2), 0x10);
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        //if (this.serialPort1.IsOpen)
        //        //{
        //        //    this.serialPort1.Close();
        //        //}
        //        //this.serialPort1.Open();
        //        //this.ab.Write(bytes, 0, bytes.Length);
        //        this.ab.WriteLine(this.textBox2.Text);
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show( ex.Message, "LCR设备测试", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}
        private void SetSplitContainer(ref SplitContainer SplitC,FixedPanel _Panel)
        {
            SplitC.FixedPanel = _Panel;
            SplitC.BorderStyle = BorderStyle.None;
            SplitC.SplitterWidth = 1;
            SplitC.IsSplitterFixed = true;
        }
        private void FrmDeviceTesting_Load(object sender, EventArgs e)
        {
            this.n.DataSource = System.IO.Ports.SerialPort.GetPortNames();
            SplitContainer spct = this.splitContainer1;
            this.SetSplitContainer(ref spct, FixedPanel.Panel1);
            spct = this.splitContainer2;
            this.SetSplitContainer(ref spct, FixedPanel.Panel1);
            spct = this.splitContainer3;
            this.SetSplitContainer(ref spct, FixedPanel.Panel1);
            this.h.SelectedText = setting.BaudRate;
            //this.n.SelectedText = setting.PortName;
            //this.g.SelectedText = setting.DataBit;
            this.m.SelectedText = setting.StopBit;
            //this.i.SelectedText = setting.Parity;
        }

        private void FrmDeviceTesting_FormClosed(object sender, FormClosedEventArgs e)
        {
            setting.BaudRate = this.h.SelectedText;
            //setting.PortName = this.n.SelectedText;
            //setting.DataBit = this.g.SelectedText;
            setting.StopBit = this.m.SelectedText;
            //setting.Parity = this.i.SelectedText;
            setting.Save();
            if (this.ab.IsOpen)
            {
                this.ab.Close();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
           

        }


        private bool getReelIDInfo(string reelid)
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                string cmd1 = string.Empty;
                cmd = @"select p.part_no,m.reel_no,m.reel_qty,m.datecode,m.lot ,v.vendor_code||':'||v.vendor_name as vendor
                        from sajet.g_material m,sajet.sys_part p,sajet.sys_vendor v
                        where p.part_id=m.part_id
                        and m.vendor_id=v.erp_id(+)
                        and m.reel_no=:rellNo ";

                cmd1 = @"select reel_id, part_no, date_code, qty, lotno, vendor_code, po, sortcode, createdate, createuserid, modifydate, modifyuserid, 
                        remark1, remark2, remark3, remark4, remark5, remark6 from lcr_base_reel m
                       where  m.reel_id=:rellNo ";

                DataTable dt = repository.FindTable(cmd, new { rellNO = reelid });

                //DataTable dt = ClientUtils.ExecuteSQL(cmd).Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    DataTable dt1 = repository.FindTable(cmd1, new { rellNO = reelid });

                    if (dt1.Rows.Count <= 0)
                    {
                        MessageBox.Show("该最小料包号(" + reelid + ")不存在或没有料号或供应商或输入错误!");

                        flag = false;
                    }
                    else
                    {
                        this.txtCPN.Text = dt1.Rows[0]["part_no"].ToString();
                        this.txtQTY.Text = dt1.Rows[0]["qty"].ToString();
                        this.txtLC.Text = dt1.Rows[0]["lotno"].ToString();
                        this.txtDC.Text = dt1.Rows[0]["date_code"].ToString();
                        this.txtVendor.Text = dt1.Rows[0]["vendor_code"].ToString();

                    }

                }
                else if (Double.Parse(dt.Rows[0]["reel_qty"].ToString()) <= 0)
                {
                    MessageBox.Show("该最小料包号(" + reelid + ")已经全部用完");
                    flag = false;
                }
                else
                {
                    this.txtCPN.Text = dt.Rows[0]["part_no"].ToString();
                    this.txtQTY.Text = dt.Rows[0]["reel_qty"].ToString();
                    this.txtLC.Text = dt.Rows[0]["Lot"].ToString();
                    this.txtDC.Text = dt.Rows[0]["datecode"].ToString();
                    this.txtVendor.Text = dt.Rows[0]["vendor"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                flag = false;
            }
            return flag;
        }


              public   string   itemtype;
            private bool getLCRMeasure(string PN)
            {
                bool flag = true;
                try
                {
                    string cmd = string.Empty;
                    // cmd = "select CPN,MAKEVALUE,UNIT,MINVALUE,MAXVALUE,MATERIALTYPE,[DESC] from udt_lcr WHERE CPN='" + _CPN.Replace("'", "''") + "'";
                    cmd = "select T.ITEM_TYPE, T.PART_NO,T.DESCRIPTION,T.UPPER_LIMIT,T.LOWER_LIMIT,T.STANDARD_VALUE,T.UNIT" +
                    " from lcr.LCR_PN t"
                        + " where t.PART_NO=:PNNO";
                    DataTable dt = repository.FindTable(cmd, new { PNNO = PN });
                    if (dt.Rows.Count <= 0)
                    {

                        MessageBox.Show( "料号(" + PN + ")没有维护量测值或者不需要量测");
                        flag = false;
                    }
                    else
                    {
                        this.txtMakeValue.Text = dt.Rows[0]["STANDARD_VALUE"].ToString();//标准值
                        this.txtUnit.Text = dt.Rows[0]["UNIT"].ToString();
                        this.txtLowerTo.Text = dt.Rows[0]["LOWER_LIMIT"].ToString();
                        this.txtUpperTo.Text = dt.Rows[0]["UPPER_LIMIT"].ToString();
                        this.txtDESC.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                        itemtype = dt.Rows[0]["ITEM_TYPE"].ToString();
                        lcrcontrol.LCRMarkUnit= dt.Rows[0]["UNIT"].ToString();
                        lcrcontrol.LCRminValue= dt.Rows[0]["LOWER_LIMIT"].ToString();
                        lcrcontrol.LCRmaxValue = dt.Rows[0]["UPPER_LIMIT"].ToString();
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    flag = false;
                }
                return flag;
            }

        private void txtreelid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            { 
            getReelIDInfo(txtreelid.Text);

            if (!getLCRMeasure(this.txtCPN.Text))
            {
                return;
            }

           
            }
        }

        public string[] cmdArray;
        public bool MessageAnalysis(string _msg, out decimal value)
        {
            decimal cvalue=0;
            bool flag = true;
            try
            {
                if ((_msg == string.Empty) | _msg.Equals(""))
                {
                    value = 0;
                    return flag;
                }
                int length;
                 cmdArray = StringEx.Split(_msg, new string[] { "\r\n   ", "\r\n    ", "\r\n" }, out length);
                foreach (string cmd in cmdArray)
                {

                    cvalue = MeasureUnit(lcrcontrol.LCRMarkUnit, cmd);
                }

                


            }
            catch (Exception exception)
            {

                value = 0;
                flag = false;
            }
            value = cvalue;
            return flag;
        }

        Thread threadShowComData;//在主线程中声明线程 threadShowComData
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            //this.InvokeRequired  判斷是否是同一個Thread操作
            if (this.cbAutoMode.Checked)
            {
                //獲取com口數據
                threadShowComData = new Thread(this.getComDataAndValue);
                threadShowComData.IsBackground = true;
                threadShowComData.Start();
            }
            else
            {
                //終止showComData Thread
                threadShowComData.Abort();
                this.cbAutoMode.Text = "";
                this.cbAutoMode.Text = "";
            }
        }


        private void getComDataAndValue()
        {
            //Thread.Sleep(500);
            while (true)
            {
                if (!string.IsNullOrEmpty(""))
                    showComDataAndValue();
            }
        }



        /// <summary>
        /// 把COM接收到的數據
        /// 解析并顯示到頁面
        /// </summary>
        private void showComDataAndValue()
        {
            try
            {

             string dd=  (MeasureUnit(txtUnit.Text, "")).ToString();


            }
            catch (Exception e)
            {

            }
        }


        private Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            try
            {

                if (strData.Contains("E"))
                {
                    dData = Decimal.Parse(strData, System.Globalization.NumberStyles.Float);
                }
            }
            catch (Exception ex)
            {
                
                return 0;
            }
            return dData;
        }

        decimal ETestvalue;
        /// <summary>
        /// 转换为标准值
        /// </summary>
        /// <param name="FromUnit"></param>
        /// <param name="EValue"></param>
        /// <returns></returns>
        private decimal MeasureUnit(string FromUnit, string EValue)
        {

            ETestvalue = ChangeDataToD(EValue);

            try
            {

                if (FromUnit.ToUpper() == "Ω")
                {
                    ETestvalue = Math.Round(ETestvalue, 4);
                }

                if (FromUnit == "mΩ")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000, 4);
                }

                if (FromUnit.ToUpper() == "KΩ")
                {
                    ETestvalue = Math.Round(ETestvalue / 1000, 4);
                }

                if (FromUnit == "MΩ")
                {
                    ETestvalue = Math.Round(ETestvalue / 1000000, 4);
                }

                if (FromUnit.ToUpper() == "H")
                {
                    ETestvalue = Math.Round(ETestvalue, 4);
                }

                if (FromUnit.ToUpper() == "H")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000, 4);
                }

                if (FromUnit.ToUpper() == "UH")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000 * 1000, 4);
                }

                if (FromUnit.ToUpper() == "NH")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000 * 1000 * 1000, 4);
                }

                if (FromUnit.ToUpper() == "F")
                {
                    ETestvalue = Math.Round(ETestvalue, 4);
                }

                if (FromUnit.ToUpper() == "MF")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000, 4);
                }

                if (FromUnit.ToUpper() == "UF")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000000, 4);
                }

                if (FromUnit.ToUpper() == "NF")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000000000, 4);
                }

                if (FromUnit.ToUpper() == "PF")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000000000000, 4);
                }
            }
            catch
            {
                return ETestvalue = 0;
            }
            return ETestvalue;
        }


        private bool InsertTestResult(string reelid, string partno, string testvalue, string unit, string userid)
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                cmd = @"select reel_id from lcr.LCR_REEL lt where lt.reel_id=:reelid";

                DataTable dt = repository.FindTable(cmd, new { reelid = reelid });

                if (dt.Rows.Count > 0)//已有数据，存入历史表
                {
                    cmd = " insert into LCR_REEL_DETAIL( reel_id,part_no,result,value,unit,createdate,createuserid) select t.reel_id,t.part_no," +
                        "t.result,t.value,t.unit,t.createdate,t.createuserid from LCR_REEL t  where t.reel_id= :reelid ";
                    repository.ExecuteBySql(cmd, new { reelid = reelid });

                    cmd = "delete from lcr.LCR_REEL lt where lt.reel_id=:reelid";
                    repository.ExecuteBySql(cmd, new { reelid = reelid });
                }

                cmd = "insert into lcr.LCR_REEL(reel_id,part_no,result,value,unit,createuserid,createdate) values " +
                    "(:reelid,:partno,:result,:value,:unit,:userid,:createdate)";

                repository.ExecuteBySql(cmd, new
                {
                    reelid = reelid,
                    partno = partno,
                    result = "PASS",
                    value = testvalue,
                    unit = unit,
                    userid = userid,
                    createdate = DateTime.Now
                });

                ///AddRow(txtCPN.Text.Trim(), txtReelNo.Text.Trim(), testvalue, "PASS");
            }
            catch (Exception ex)
            {
                ///setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }


    }
    }
