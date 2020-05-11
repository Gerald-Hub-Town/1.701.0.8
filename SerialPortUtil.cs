using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using TWFramwork.Net.Tcp;
using TWFramwork.Common;
using System.Windows.Forms;

namespace LCRTest
{
    /// <summary>
    /// 串口通讯类
    /// </summary>
    public class SerialPortUtil
    {
        private SerialPort SPort; //串口
        public DataBlock Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }
        private DataBlock cmdData;
        private DataBlock buffer;
        public event EventHandler<TEventArgs<string>> ReceivedData;
        /// <summary>
        /// 文本所使用的编码
        /// </summary>
        public Encoding Encoding
        {
            get;
            set;
        }
        /// <summary>
        /// 命令行分隔字符串
        /// </summary>
        public string[] NewLines
        {
            get;
            set;
        }
        public bool IsOpen
        {
            get { return SPort.IsOpen; }
        }
        #region  串口属性
        public bool DtrEnable
        {
            get;

            set;

        }
        public string PortName
        {
            get;
            set;
        }
        public int BaudRate
        {
            get;
            set;
        }
        public Parity Parity
        {
            get;
            set;
        }
        public StopBits StopBits
        {
            get;
            set;
        }
        public int DataBits
        {
            get;
            set;
        }
        public int ReceivedBytesThreshold
        {
            get;
            private set;
        }
        #endregion
        public SerialPortUtil()
        {
            SPort = new SerialPort();
            cmdData = new DataBlock();
            Encoding = Encoding.UTF8;
            NewLines = new string[] { "\r\n" };
            PortName = "COM1";                      //串口端口
            BaudRate = 9600;                     //串口的波特率
            Parity = Parity.None;                //校验位，一般设置为NONE
            DataBits = 8;                        //数据位
            StopBits = StopBits.One;             //停止位
            //  ReceivedBytesThreshold = 1;
            DtrEnable = SPort.DtrEnable;

        }
        public void Sart(string com)
        {
            try
            {
                ///PortName = com;
                SPort.PortName = PortName;                      //串口端口
                SPort.BaudRate = BaudRate;                      //串口的波特率
                SPort.Parity = Parity;                          //校验位，一般设置为NONE
                SPort.StopBits = StopBits;                      //停止位
                SPort.DataBits = DataBits;                      //数据位
                SPort.ReceivedBytesThreshold = 1;
                SPort.DtrEnable = DtrEnable;

                Buffer = new DataBlock();
                if (!SPort.IsOpen)
                {

                    SPort.Open();
                    SPort.DataReceived += new SerialDataReceivedEventHandler(SPort_DataReceived);    //串口接收数据事件，在接收后触发事件
                }
            }
            catch (Exception e)
            {
                //Log.Error(e.Message);
                // throw e;
            }
        }
        public void Stop()
        {
            SPort.DataReceived -= SPort_DataReceived;
           
           
            SPort.Close();
        }
        //串口接收数据，并将其转化为字符串
        public void SPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string rawCmd;
            rawCmd = "";
        
            try
            {
                //string temp11111 = SPort.ReadTo("\n");
                int readCount = SPort.Read(Buffer.Buffer, 0, SPort.BytesToRead);
                this.Buffer.WriteIndex += readCount;
                //添加上次为使用的数据，重新解析
                cmdData += Buffer;
                //重用数据区
                Buffer.Reset();

                //使用规定的字符集解析字符

                rawCmd = Encoding.ASCII.GetString(cmdData.Buffer, cmdData.ReadIndex, cmdData.DataLength);
                SPort.DiscardInBuffer();

                //rawCmd = System.Text.ASCIIEncoding.ASCII.GetString(cmdData.Buffer, cmdData.ReadIndex, cmdData.DataLength);
                rawCmd = Encoding.ASCII.GetString(cmdData.Buffer, cmdData.ReadIndex, cmdData.DataLength);
                SPort.DiscardInBuffer();
                if (string.IsNullOrEmpty(rawCmd))
                    rawCmd = "0";
                int length;
                string[] cmdArray = StringEx.Split(rawCmd, NewLines, out length);
                cmdArray = cmdArray.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                // temp1 = rawCmd.Replace("\r\n", "").Replace(" ", "").Trim();
                //string[] cmdArray = new string[1];
                //cmdArray[0] = temp1;
                foreach (string cmd in cmdArray)
                {
                    //线程安全
                    EventHandler<TEventArgs<string>> temp = ReceivedData;
                    if (temp != null)
                    {
                        temp(this, new TEventArgs<string>(cmd));
                    }
                }
                //获得已经使用的字符串的数据长度，然后设置为已经读取
                cmdData.ReadIndex += Encoding.GetByteCount(rawCmd.Substring(0, length));

            }
            catch (System.Exception ex)
            {
                //throw ex;
                //Log.Error("称重接收数据异常：" + rawCmd + "\r\n详细错误：" + ex.ToString());
            }
        }
    }
}
