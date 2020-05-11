using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLBuilder.Repositories;
using ATE;
using TWFramwork.Common;
using TWFramwork.Net.Tcp;
using TWFramwork.Win;
using System.Threading;

namespace LCRTest
{
    public partial class fMain : Form
    {
        private HandleInterfaceUpdataDelegate UpdateCombinTextHandler;
        private SerialPortUtil serialPort1;
        DBconnetion DBC = new DBconnetion();
        clsLCRMachineControl LCRManchine = null;
        private delegate void DataReceive(string A_0);
        private string LCRUnit = string.Empty;
        ENVCtrl LCR = new ENVCtrl();
        private string testvalue;
        private decimal ETestvalue;
        List<string> testlist = new List<string>();
        private string itemtype;
        FrmLogin flogin = new FrmLogin();
        private string userid;
        private string reelid;
        private string partno;
        private string lotno;
        private string vendorcode;
        private int qty;
        private string datecode;
        private bool result = false;
        private string lotno1;

        public readonly OracleRepository repository = new OracleRepository("Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = ksmesh-scan.luxshare.com.cn)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ksmesh)  )  );Persist Security Info=True;User ID=lcr;Password=lcr;")
        {
            IsEnableFormat = false,
            SqlIntercept = (sql, parameter) =>
            {
                return null;
            }
        };
        public fMain()
        {
            InitializeComponent();
        }

        public fMain(string  UserId)
        {

            InitializeComponent();
            userid = UserId;
           
            serialPort1 = new SerialPortUtil();


        }
        private void SetSplitContainer(ref SplitContainer SplitC)
        {
            SplitC.FixedPanel = FixedPanel.Panel1;
            SplitC.BorderStyle = BorderStyle.None;
            SplitC.SplitterWidth = 1;
            SplitC.IsSplitterFixed = true;
        }
        private void FrmLCRCheck_Load(object sender, EventArgs e)
        {
            SplitContainer splitC = this.splitContainer1;
            this.SetSplitContainer(ref splitC);
            this.splitContainer1 = splitC;
            splitC = this.splitContainer2;
            this.SetSplitContainer(ref splitC);
            this.splitContainer2 = splitC;
            LCRManchine = new clsLCRMachineControl();
            //if (!this.LoadIniFile(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini"))
            //{
            //    toolStripButton1_Click(null, null);
            //}
            this.LoadIniFile(Environment.CurrentDirectory.ToString() + "\\LCRControl.ini");
            this.WindowState = FormWindowState.Maximized;
            //if(OpenCom();
        }


        /// <summary>
        /// 检测测量值
        /// </summary>
        /// 
        private void CheckValue(string testvalue)
        {
            testvalue= (MeasureUnit(txtUnit.Text, testvalue)).ToString();
         

            if (this.txtMakeValue.Text.Equals(""))
            {
               
                setMessage(MessageType.Error, "请输入最小料包号或料号");
                this.txtReelNo.Focus();
                this.txtReelNo.SelectAll();
                return;
            }
            else 
            {
                if (Convert.ToDouble(testvalue) > Convert.ToDouble(this.txtLowerTo.Text) & Convert.ToDouble(testvalue) < Convert.ToDouble(this.txtUpperTo.Text))
                {
                    this.lblCommunicateCount.Text = (Convert.ToInt32(this.lblCommunicateCount.Text) + 1).ToString();
                    if (Convert.ToInt32(this.lblCommunicateCount.Text) >3)//连续接收到2次成功数据，才开始判断(稳定数据)
                    {
                        
                        this.lblTestResult.Text = "Pass";
                        this.lblTestResult.BackColor = System.Drawing.Color.Green;
                        if (InsertTestResult(txtReelNo.Text,txtCPN.Text, testvalue, txtUnit.Text,userid))//测量成功才会插入数据
                        {
                            setMessage(MessageType.Information, "量测成功!");
                        }
                        result = true;
                        this.txtReelNo.Focus();
                        this.txtReelNo.SelectAll();
                    }
                    else
                    {
                        this.lblTestResult.Text = "Testing";
                        this.lblTestResult.BackColor = System.Drawing.Color.Green;
                        this.setMessage(MessageType.Input, "读取数据并匹配中");
                    }
                }
                else
                {
                    
                        this.lblTestResult.Text = "Fail";
                        this.lblTestResult.BackColor = System.Drawing.Color.Red;
                        setMessage(MessageType.Error, "实际量测值与维护的标准值范围不匹配！");
                        this.lblCommunicateCount.Text = "0";
                        this.txtReelNo.Focus();
                        this.txtReelNo.SelectAll();
                   
                }
            }
           

        }

        /// <summary>
        /// 匹配收集到的多笔测试数据
        /// </summary>
        private void Match_test_data()
        {
            for (int i = 0; i <= testlist.Count-1; i++)
            {
                CheckValue(testlist[i]);
            
            }
        }

        
        private StringBuilder Builder = new StringBuilder();


        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        private bool OpenCom()
        {
            bool flag = true;
            try
            {
                if ((LCRManchine.EquipmentType == "4980" ) || ( LCRManchine.EquipmentType == "E4982A" )  || (LCRManchine.EquipmentType == "E34420A") )
                {
                    flag = LCR.OpenPowerSupplyGPIB(LCRManchine.AgilentAddress);
                }
                else

                {
                    //if (this.serialPort1.IsOpen)
                    //{
                    //    this.serialPort1.Stop();
                    //}
                    StringBuilder temp = new StringBuilder(10000);
                    this.serialPort1.PortName = LCRManchine.PortName;
                    this.serialPort1.BaudRate = LCRManchine.Baudbits;
                    this.serialPort1.DataBits = LCRManchine.DataBits;
                    this.serialPort1.StopBits = (System.IO.Ports.StopBits)LCRManchine.StopBits;
                    this.serialPort1.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), LCRManchine.Parity);
                    this.serialPort1.DtrEnable = LCRManchine.DTR;
                    //this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Serial_DataReceived);
                    //this.serialPort1.Open();

                    serialPort1.Sart(temp.ToString());
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.ReceivedData += new EventHandler<TEventArgs<string>>(sport_ReceivedData);
                        UpdateCombinTextHandler = new HandleInterfaceUpdataDelegate(UpdateCombinText);
                        serialPort1.NewLines = new string[] { "\r\n   ", "\r\n    ", "\r\n" };
                    }

                }
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="data"></param>
        private void UpdateCombinText(string data)
        {
            try
            {

                 LCRManchine.AgilentAddress=  (data);

               //// listBox1.Items.Add(data);
               //string TESTDATA = data;
               //if (result == false)
               //{                
               //    CheckValue(TESTDATA); 
               //}
               //else
               //{
               //    Application.DoEvents();
               //    this.serialPort1.Stop();
               //}

            }
            catch (System.Exception ex)
            {
               
            }

        }




       
        private void sport_ReceivedData(object sender, TWFramwork.Net.Tcp.TEventArgs<string> e)
        {
            testvalue = e.Param;
            /////    int idxStart = strisn.LastIndexOf(" ");  ///取空格的位置
            ///   strisn = strisn.Substring(0, idxStart);///去空格后的数
            ///strisn = Regex.Replace(strisn, "[a-z]", "", RegexOptions.IgnoreCase);  ///去掉字母，只剩数值 
            this.Invoke(UpdateCombinTextHandler, testvalue);
        }

        /// <summary>
        /// 根据消息类型，设置提示界面
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_msg"></param>
        private void setMessage(MessageType _type, string _msg)
        {
            clsMessageInfo.setMessageinfo(_type, ref textBox8, ref lblpicMessage, _msg);
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="_fileName"></param>
        /// <returns></returns>
        private bool LoadIniFile(string _fileName)
        {
            bool flag = true;
            try
            {
                if (!System.IO.File.Exists(_fileName))
                {
                    setMessage(MessageType.Error, "配置文件不存在");
                    flag = false;
                }
                else
                {
                    LCRManchine.Baudbits = int.Parse(clsINISetting.INIRead(_fileName, "System", "BaudRate", "0"));
                    LCRManchine.PortName = clsINISetting.INIRead(_fileName, "System", "PortName", "");
                    LCRManchine.DataBits = int.Parse(clsINISetting.INIRead(_fileName, "System", "DataBits", "0"));
                    LCRManchine.StopBits = int.Parse(clsINISetting.INIRead(_fileName, "System", "StopBits", "1"));
                    LCRManchine.Parity = clsINISetting.INIRead(_fileName, "System", "Parity", "None");
                    LCRManchine.DTR = Convert.ToBoolean(clsINISetting.INIRead(_fileName, "System", "DtrEnable", "True"));
                    LCRManchine.AgilentAddress = clsINISetting.INIRead(_fileName, "System", "address", "None");
                    LCRManchine.EquipmentType= clsINISetting.INIRead(_fileName, "System", "EquipmentType", "None");
                }

            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 发送串口命令
        /// </summary>
        /// <param name="sendMessage"></param>
        /// <returns></returns>
        private bool Send(string sendMessage)
        {
            bool flag = true;
            try
            {
                sendMessage = clsASCIITransfer.StringToHexString(sendMessage + "\n\r", Encoding.ASCII);
                byte[] bytes;

                bytes = new byte[sendMessage.Length / 3];
                for (int i = 0; i < sendMessage.Length; i += 3)
                {
                    try
                    {
                        bytes[i / 3] = Convert.ToByte(sendMessage.Substring(i, 2), 0x10);
                    }
                    catch
                    {
                    }
                }
                //this.serialPort1.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        private void txtVendor_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打开测试界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen)
            {
                ///this.serialPort1.Close();
            }
            FrmDeviceTesting _testing = new FrmDeviceTesting();
            _testing.ShowDialog();
        }
        /// <summary>
        /// 打开设置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmConfig _config = new FrmConfig();
            _config.ShowDialog();
        }
        /// <summary>
        /// 清除界面
        /// </summary>
        private void ClearAll()
        {
            setMessage(MessageType.Input, "请输入料号或者扫描最小料包号");
            //this.checkBox1.Checked = false;
            ///this.serialPort1.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(Serial_DataReceived);
            this.lblCommunicateCount.Text = "0";
            this.lblTestResult.Text = "Waiting";
            this.lblTestResult.BackColor = System.Drawing.Color.Green;
            this.txtCPN.Text = "";
            this.txtDESC.Text = "";
            // this.txtICCharacter.Text = "";
            this.txtLC.Text = "";
            this.txtDC.Text = "";
            this.txtLowerTo.Text = "";
            this.txtUnit.Text = "";
            this.txtUpperTo.Text = "";
            this.txtVendor.Text = "";
            this.txtQTY.Text = "";
            this.txtMakeValue.Text = "";
            this.txtTestValue.Text = "";
            if (this.dataGridView1.Rows.Count >= 6)
            { 
            this.dataGridView1.Rows.Clear();
               }
            //  this.textBox6.Text = "";
        }

        /// <summary>
        /// 处理科学计算法，直接返回decimal类型值
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
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
                setMessage(MessageType.Error, ex.Message);
                return 0;
            }
            return dData;
        }

        /// <summary>
        /// FromUnit从系统中取出来的单位，单位转换
        /// </summary>
        /// <param name="FromUnit"></param>
        /// <param name="ToUnit"></param>
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
                    ETestvalue = Math.Round(ETestvalue * 1000*1000, 4);
                }

                if (FromUnit.ToUpper() == "NH")
                {
                    ETestvalue = Math.Round(ETestvalue * 1000*1000*1000, 4);
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
                return ETestvalue=0; 
            }
            return ETestvalue;
        }

        /// <summary>
        /// 拆分字符串
        /// </summary>
        /// <param name="testvalue"></param>
        /// <param name="equipmenttype"></param>
        /// <param name="itemtype"></param>
        /// <returns></returns>
        public bool SpiletTestValue(string testvalue, string equipmenttype, string itemtype)
        {

            try
            {
                string[] strArray = testvalue.Split(new char[] { ',' });


                if (equipmenttype == "E4982A" && itemtype == "Inductor")///取第二位字符串
                {
                    testlist.Add(strArray[1]);

                }

                if (equipmenttype == "E4982A" && itemtype == "Filter")///取第四位字符串
                {
                    testlist.Add(strArray[3]);

                }

                if (equipmenttype == "4980" && itemtype == "Resistor")///电阻取第二位
                {

                    testlist.Add(strArray[1]);

                }

                if (equipmenttype == "4980" && itemtype == "Capacitor")///电容去第一位
                {

                    testlist.Add(strArray[0]);

                }

                Array.Clear(strArray, 0, strArray.Length);
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                return  false;
            }
            return true;
        }
        private void txtMINSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            result = false;
            ClearAll();
            if (e.KeyChar == '\r')
            {

                
                if (this.txtReelNo.Text.Equals(""))
                {
                    setMessage(MessageType.Error, "请输入最小料包号或料号");
                    this.txtReelNo.Focus();
                    this.txtReelNo.SelectAll();
                    return;
                }

                

                ///********料号带横杠，最小料包号不带*********/
                    //if (this.txtReelNo.Text.IndexOf("-") > 0)
                    //{
                    //    this.txtCPN.Text = this.txtReelNo.Text;
                    //}
                    //else 

                    if (!getReelIDInfo(this.txtReelNo.Text))
                {
                    this.txtReelNo.Focus();
                    this.txtReelNo.SelectAll();
                    return;
                }

                  
                //Action at = new Action(() => { getLCRMeasure(this.txtCPN.Text); });
                //this.Invoke(at);

               

                if (!getLCRMeasure(this.txtCPN.Text))
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(50);
                    this.txtReelNo.Focus();
                    this.txtReelNo.SelectAll();
                    return;
                }
                else if (OpenCom())//打开COM 或者和安捷伦建立通讯
                {

                    if (LCRManchine.EquipmentType == "4980")
                    {
                        setMessage(MessageType.Input, "已与设备建立通信！");
                         
                        setMessage(MessageType.Input, "4980获取测量数据中.........");

                        int i = 0;
                        while (i < 5)
                        {
                            Application.DoEvents();
                            testvalue = LCR.ReadLcRValue();
                            SpiletTestValue(testvalue, "4980", itemtype);
                            System.Threading.Thread.Sleep(100);
                            i = i + 1;
                            this.lblCommunicateCount.Text = i.ToString();
                        }
                        this.lblCommunicateCount.Text = "0";
                        Match_test_data();///匹配收集的测试数据
                       
                        testlist.Clear();///清空收集的数据
                        LCR.SendSYSTlocal();

                    }
                    else if (LCRManchine.EquipmentType == "E4982A")
                    {

                        setMessage(MessageType.Input, "已与设备建立通信！");
                                    
                        setMessage(MessageType.Input, "获取测量数据中.........");

                        int i = 0;
                        while (i <5)
                        {
                            Application.DoEvents();
                            testvalue = LCR.ReadLcRValue();
                            SpiletTestValue(testvalue, "E4982A", itemtype);
                            System.Threading.Thread.Sleep(100);
                            i = i + 1;
                            this.lblCommunicateCount.Text = i.ToString();
                        }
                        this.lblCommunicateCount.Text = "0";

                        Match_test_data();

                       
                        testlist.Clear();
                    
                       
                    }
                    else if (LCRManchine.EquipmentType == "E34420A")
                    {

                        setMessage(MessageType.Input, "已与设备建立通信！");


                        setMessage(MessageType.Input, "设置测量模式.........");
                        LCR.SendSYSTRem();

                        setMessage(MessageType.Input, "获取数据中.........");

                        int i = 0;
                        while (i < 5)
                        {
                            Application.DoEvents();
                            testvalue = LCR.ReadLcR34420Value();
                      
                            System.Threading.Thread.Sleep(500);
                            i = i + 1;
                            this.lblCommunicateCount.Text = i.ToString();

                            CheckValue(testvalue);
                        }

                        

                        LCR.SendSYSTlocal();
                        testlist.Clear();

                      


                    }
                    LCR.CloseGPIB();///关闭通讯
                }
                else
                {

                    setMessage(MessageType.Error, "通信建立失败，请确认设备地址或者COM是否打开！");

                }
            }
           
        }
        /// <summary>
        /// 测量成功时，插入测量值
        /// </summary>
        /// <returns></returns>
        private bool InsertTestResult(string reelid,string partno,string testvalue,string unit,string userid )
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                 cmd= @"select reel_id from lcr.LCR_REEL lt where lt.reel_id=:reelid";

                DataTable dt = repository.FindTable(cmd, new { reelid = reelid });

                if (dt.Rows.Count > 0)//已有数据，存入历史表
                {
                    cmd = " insert into LCR_REEL_DETAIL( reel_id,part_no,result,value,unit,cr" +
                        "eatedate,createuserid) select t.reel_id,t.part_no," +
                        "t.result,t.value,t.unit,t.createdate,t.createuserid from LCR_REEL t  where t.reel_id= :reelid ";
                    repository.ExecuteBySql(cmd, new { reelid = reelid });

                    cmd = "delete from lcr.LCR_REEL lt where lt.reel_id=:reelid";
                    repository.ExecuteBySql(cmd, new { reelid = reelid });
                }

                cmd = "insert into lcr.LCR_REEL(reel_id,part_no,result,value,unit,createuserid,createdate) values " +
                    "(:reelid,:partno,:result,:value,:unit,:userid,:createdate)";

                repository.ExecuteBySql(cmd, new { reelid = reelid, partno = partno, result = "PASS", 
                    value = testvalue, unit =unit, userid = userid, createdate = DateTime.Now });
                
                AddRow(txtCPN.Text.Trim(), txtReelNo.Text.Trim(), testvalue, "PASS");
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }


        private bool InsertBaserell(string reelid, string partno, string datecode, int qty,string lotno,string vendorcode,string po, string userid)
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                cmd = @"select reel_id from lcr.LCR_BASE_REEL lt where lt.reel_id=:reelid";

                DataTable dt = repository.FindTable(cmd, new { reelid = reelid });

                if (dt.Rows.Count <= 0)//已有数据，存入历史表
                {
                    cmd = " insert into LCR_REEL_DETAIL( reel_id,part_no,DATE_CODE,QTY,LOTNO,VENDOR_CODE,PO) select t.reel_id,t.part_no," +
                        "reel_id,part_no,DATE_CODE,QTY,LOTNO,VENDOR_CODE,PO from LCR_BASE_REEL t  where t.reel_id= :reelid ";
                    repository.ExecuteBySql(cmd, new { reelid = reelid });
                }

   
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }

        //private bool InsertTestResult()
        //{
        //    bool flag = true;
        //    try
        //    {
        //        string cmd = string.Empty;
        //        cmd = @"select reel_id from lcr.LCR_REEL lt where lt.reel_id=:reelid";

        //        DataTable dt = repository.FindTable(cmd, new { reelid = txtReelNo.Text.Trim() });

        //        if (dt.Rows.Count > 0)//已有数据，存入历史表
        //        {
        //            cmd = " insert into LCR_REEL_DETAIL( reel_id,part_no,result,value,unit,createdate,createuserid) select t.reel_id,t.part_no," +
        //                "t.result,t.value,t.unit,t.createdate,t.createuserid from LCR_REEL t  where t.reel_id= :reelid ";
        //            repository.ExecuteBySql(cmd, new { reelid = txtReelNo.Text.Trim() });

        //            cmd = "delete from lcr.LCR_REEL lt where lt.reel_id=:reelid";
        //            repository.ExecuteBySql(cmd, new { reelid = txtReelNo.Text.Trim() });
        //        }

        //        cmd = "insert into lcr.LCR_REEL(reel_id,part_no,result,value,unit,createuserid,createdate) values " +
        //            "(:reelid,:partno,:result,:value,:unit,:userid,:createdate)";

        //        repository.ExecuteBySql(cmd, new
        //        {
        //            reelid = txtReelNo.Text.Trim(),
        //            partno = this.txtCPN.Text,
        //            result = "PASS",
        //            value = testvalue,
        //            unit = txtUnit.Text,
        //            userid = flogin.userid,
        //            createdate = DateTime.Now.ToString()
        //        });
        //      
        //        AddRow(txtCPN.Text.Trim(), txtReelNo.Text.Trim(), txtTestValue.Text.Trim(), "PASS");
        //    }
        //    catch (Exception ex)
        //    {
        //        setMessage(MessageType.Error, ex.Message);
        //        flag = false;
        //    }
        //    return flag;
        //}
        /// <summary>
        /// 获取LCR测量标准，标准值，最大值，最小值
        /// </summary>
        /// <param name="_CPN">料号</param>
        /// <returns></returns>
        private bool getLCRMeasure(string PN)
        {
            bool flag = true;
            try
            {
                PN = PN.ToUpper().Trim();
                string cmd = string.Empty;
                // cmd = "select CPN,MAKEVALUE,UNIT,MINVALUE,MAXVALUE,MATERIALTYPE,[DESC] from udt_lcr WHERE CPN='" + _CPN.Replace("'", "''") + "'";
                cmd = "select T.ITEM_TYPE, T.PART_NO,T.DESCRIPTION,T.UPPER_LIMIT,T.LOWER_LIMIT,T.STANDARD_VALUE,T.UNIT,T.REMARK1" +
                " from lcr.LCR_PN t"
                    + " where t.PART_NO=:PNNO";
                DataTable dt = repository.FindTable(cmd, new { PNNO = PN });
                if (dt.Rows.Count <= 0)
                {

                    setMessage(MessageType.Error, "料号没有维护量测值或者不需要量测");
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
                    LCRManchine.LCRMarkUnit= dt.Rows[0]["UNIT"].ToString(); 
                    txtcondition.Text= dt.Rows[0]["REMARK1"].ToString();
                }
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 获取料号描述
        /// </summary>
        /// <param name="_CPN">料号</param>
        /// <returns></returns>
        private bool getPNInfo(string PN)
        {
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                cmd = "select SPEC1 from sajet.sys_part where part_no=:PNNO";
                DataTable dt = repository.FindTable(cmd, new { PNNO = PN }); ;
                if (dt.Rows.Count <= 0)
                {
                    setMessage(MessageType.Error, "料号(" + PN + ")不存在");
                    flag = false;
                }
                else
                {
                    this.txtDESC.Text = dt.Rows[0]["SPEC1"].ToString();
                }
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 获取最小料包号的料号，剩余量，DateCode，LotCode
        /// </summary>
        /// <param name="_MINSN">最小料包号</param>
        /// <returns></returns>
        /// 
        //string cmd = string.Empty;
        //var sql = @"select * from sajet.g_lcr_test lt where lt.reel_no=:rellNo";
        //var result = repository.FindList<dynamic>(sql, new { rellNo = "" });
        //var table = repository.FindTable(sql, parameter);
        //parameter = new List<Parameter>();
        //parameter.Add(DbParameters.CreateDbParameter("@ProfitCenterId", supplierId, DbType.String));
        //    return this.BaseRepository().FindTable(sql, parameter.ToArray());
        ///repository.ExecuteBySql("")
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

                cmd1= @"select reel_id, part_no, date_code, qty, lotno, vendor_code, po, sortcode, createdate, createuserid, modifydate, modifyuserid, 
                        remark1, remark2, remark3, remark4, remark5, remark6 from lcr_base_reel m
                       where  m.reel_id=:rellNo ";

                DataTable dt = repository.FindTable(cmd, new { rellNO = reelid });

                //DataTable dt = ClientUtils.ExecuteSQL(cmd).Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    DataTable dt1 = repository.FindTable(cmd1, new { rellNO = reelid });

                    if (dt1.Rows.Count <= 0)
                    {
                        setMessage(MessageType.Error, "该最小料包号(" + reelid + ")不存在或没有料号或供应商或输入错误!");

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
                    setMessage(MessageType.Error, "该最小料包号(" + reelid + ")已经全部用完");
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
                setMessage(MessageType.Error, ex.Message);
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
       
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FrmLCRCheck_FormClosed(null, null);
        }

        private void FrmLCRCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.serialPort1.IsOpen)
                {
                   
                    this.serialPort1.Stop();
                }
            }
            catch (Exception ex)
            {
                setMessage(MessageType.Error, ex.Message);
            }
        }
      

        void AddRow(string partno, string reelno, string testvalue, string testresult)
        {
            //int index = dataGridView1.Rows.Add();
            //dataGridView1.Rows[index].Cells["PARTNO"].Value = partno;
            //dataGridView1.Rows[index].Cells["REELNO"].Value = reelno;
            //dataGridView1.Rows[index].Cells["TESTVALUE"].Value = testvalue;
            //dataGridView1.Rows[index].Cells["TESTRESULT"].Value = testresult;

            object[] row = new object[] { partno, reelno, testvalue, testresult };
            dataGridView1.Rows.Insert(0, row);
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            dataGridView1.ScrollBars = ScrollBars.Horizontal;



        }

        private void fMain_Shown(object sender, EventArgs e)
        {
            txtReelNo.Focus();
        }

        private void txtTestValue_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)//测试用//特殊权限人员使用
            {
                if (DBC.Getuser(userid))

                {
                    if (Convert.ToDouble(txtTestValue.Text) > Convert.ToDouble(this.txtLowerTo.Text) & Convert.ToDouble(txtTestValue.Text) < Convert.ToDouble(this.txtUpperTo.Text))
                    {

                        this.lblTestResult.Text = "Pass";
                        this.lblTestResult.BackColor = System.Drawing.Color.Green;
                        if (InsertTestResult(txtReelNo.Text, txtCPN.Text, txtTestValue.Text, txtUnit.Text, userid))//测量成功才会插入数据
                        {
                            setMessage(MessageType.Information, "量测成功!");
                        }
                        result = true;
                        this.txtReelNo.Focus();
                        this.txtReelNo.SelectAll();

                    }
                    else
                    {

                        this.lblTestResult.Text = "Fail";
                        this.lblTestResult.BackColor = System.Drawing.Color.Red;
                        setMessage(MessageType.Error, "实际量测值与维护的标准值范围不匹配！");
                        this.lblCommunicateCount.Text = "0";
                        this.txtReelNo.Focus();
                        this.txtReelNo.SelectAll();

                    }
                }
                else
                {
                    setMessage(MessageType.Error, "无权限，请勿非法作业！");
                    txtTestValue.Text = "";

                }


            }
        }

        private void seting4980_Click(object sender, EventArgs e)
        {
            Form4980 _config = new Form4980();
            _config.ShowDialog();
        }

        private void btn4982_Click(object sender, EventArgs e)
        {
            Form4982 _config = new Form4982();
            _config.ShowDialog();
        }

        private void btn34420A_Click(object sender, EventArgs e)
        {
            FrmConfig _config = new FrmConfig();
            _config.ShowDialog();
        }
        /// <summary>
        /// 注册reel对应的基础信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtreelid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtreelid.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(txtqrcode.Text))
                    {

                        string message;
                        ///记录注册的信息
                        if (!DBC.insertqrcode(this.txtreelid.Text.Trim().ToUpper(), lotno, partno, datecode, qty, vendorcode, lotno1, userid, out message))
                        {

                            setMessage(MessageType.Error, message);
                            return;

                        }
                        else
                        {

                            setMessage(MessageType.Information, message);
                            txtqrcode.Text = "";
                            txtreelid.Text = "";
                            txtReelNo.Focus();

                        }



                    }
                    else
                    { 
                        setMessage(MessageType.Error, "二维码不能为空");
                    }
                }
            }
        }
        /// <summary>
        /// 注册reel信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtqrcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtqrcode.Text))
                {
                    setMessage(MessageType.Error, "二维码不能为空");
                    return;
                }
                else
                {
                    ///去除字符串中的空格，回车，换行符，制表符 
                    txtqrcode.Text = txtqrcode.Text.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");

                    string[] sArray = txtqrcode.Text.Split('|');
                 
                    partno = sArray[0].ToUpper().Trim();
                    datecode = sArray[1];
                    qty = int.Parse(sArray[2]);
                    vendorcode = sArray[3].ToUpper().Trim();
                    lotno = sArray[4];
                    lotno1 = sArray[5];
                    ///判断料号是否是LCR物料
                    if (!DBC.CheckItemiISlcr(partno))
                    {

                        setMessage(MessageType.Error, "请确认是否是LCR物料，是否料号错误....");
                        return;

                    }
                    txtreelid.Text = "";
                    txtreelid.Focus();

                }
            }
        }

        private void txtReelNo_TextChanged(object sender, EventArgs e)
        {

        }
        

        Thread threadShowComData;//在主线程中声明线程 threadShowComData
        


        /// <summary>
        /// 自动模式/手动模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
              decimal value= MeasureUnit(LCRManchine.LCRMarkUnit, LCRManchine.AgilentAddress);
               
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

                testvalue = (MeasureUnit(txtUnit.Text, testvalue)).ToString();
             
            }
            catch (Exception e)
            {
               
            }
        }

        private void e34420ACommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormE34420A _config = new FormE34420A();
            _config.ShowDialog();
        }
    }
}
