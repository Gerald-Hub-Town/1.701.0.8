using System;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
namespace LCRTest
{
    internal class clsLCRMachineControl
    {
        private int _Baudbits;
        private int _DataBits;
        private bool _LCRMarkReadValue;
        private string _LCRMarkUnit;
        private double _LCRMarkValue;
        private Mode _LCRMode;
        private int _LCRModeType;
        private bool _LCROnline;
        private int _LCRStatus;
        private bool _LCRTRG;
        private string _Message;
        private string _Parity;
        private string _PortName;
        private int _StopBits;
        private bool _DTR;
        private string _AgilentAddress;
        private string _EquipmentType;
        private string _LCRminValue;
        private string _LCRmaxValue;

        public clsLCRMachineControl()
        {
            this._LCRMode = Mode.NONE;
            this._LCRTRG = false;
            this._PortName = "Com1";
            this._LCRMarkUnit = string.Empty;
            this._LCRModeType = 0;
            this._LCRMarkValue = 0.0;
            this._LCRStatus = 0;
            this._LCROnline = false;
            this._Baudbits = 0x9600;
            this._DataBits = 8;
            this._StopBits = 1;
            this._Message = string.Empty;
            this._Parity = "None";
            this._DTR = true;
            this._AgilentAddress = null;
            this._AgilentAddress = null;
            this._LCRmaxValue ="";
            this._LCRminValue ="";
        }

        public clsLCRMachineControl(string PortNo, int BBits, int DBits, int SBits, string ParityType,string AgeilentAddress,bool dtr,string EquipmentType)
        {
            this._LCRMode = Mode.NONE;
            this._LCRTRG = false;
            this._PortName = "Com1";
            this._LCRMarkUnit = string.Empty;
            this._LCRModeType = 0;
            this._LCRMarkValue = 0.0;
            this._LCRStatus = 0;
            this._LCROnline = false;
            this._Baudbits = 0x9600;
            this._DataBits = 8;
            this._StopBits = 1;
            this._Message = string.Empty;
            this._Parity = "None";
            this.PortName = PortNo;
            this.Baudbits = BBits;
            this.DataBits = DBits;
            this.StopBits = SBits;
            this.Parity = ParityType;
            this._AgilentAddress = "";
             this._DTR= dtr;
            this._AgilentAddress = AgilentAddress;
        }

        public bool CheckMode(string unit, ref string _Command)
        {
            bool flag = true;
            _Command = string.Empty;
            this.Message = "";
            try
            {
                if (unit.ToUpper().IndexOf('F') >= 0)
                {
                    if (this.LCRMode != Mode.CD)
                    {
                        flag = false;
                        _Command = "MAIN:MODE:CD";
                    }
                    return flag;
                }
                if (unit.ToUpper().IndexOf("OHM") >= 0)
                {
                    if (this.LCRMode != Mode.RQ)
                    {
                        flag = false;
                        _Command = "MAIN:MODE:RQ";
                    }
                    return flag;
                }
                if (unit.ToUpper().IndexOf('H') >= 0)
                {
                    if (this.LCRMode != Mode.LR)
                    {
                        flag = false;
                        _Command = "MAIN:MODE:LQ";
                    }
                    return flag;
                }
                if (this.LCRMode != Mode.RQ)
                {
                    flag = false;
                    _Command = "MAIN:MODE:RQ";
                }
            }
            catch (Exception exception)
            {
                this.Message = exception.Message;
                flag = false;
            }
            return flag;
        }

        private bool IsOnlyNumber(string value)
        {
            Regex regex = new Regex(@"^(-?\d+)(\.\d+)?$");
            return regex.Match(value).Success;
        }

        public bool MessageAnalysis(string _msg)
        {
            this.Message = string.Empty;
            bool flag = true;
            try
            {
                if ((_msg == string.Empty) | _msg.Equals(""))
                {
                    return flag;
                }
                string[] strArray = _msg.Split(new char[] { '\n' });
                foreach (string str in strArray)
                {
                    string[] strMessage = str.Split(new char[] { ':' });
                    this.MessageAnalysis(strMessage);
                }
            }
            catch (Exception exception)
            {
                this.Message = exception.Message;
                flag = false;
            }
            return flag;
        }

       

        private void MessageAnalysis(string[] strMessage)
        {
            for (int i = 0; i < strMessage.Length; i++)
            {
                string[] strArray;
                if (strMessage[i].Equals(""))
                {
                    break;
                }
                if (this.LCRMarkReadValue)
                {
                    if (strMessage[i] == "MAIN")
                    {
                        strArray = strMessage[i + 1].Split(new char[] { ' ' });
                        if (strArray[0] == "SECO")
                        {
                            for (int k = 1; k < strArray.Length; k++)
                            {
                                if (!strArray[k].Equals(""))
                                {
                                    this.LCRMarkUnit = strArray[k];
                                    k = strArray.Length;
                                    if (Enum.GetName(typeof(Mode), this.LCRMode) == "RQ")
                                    {
                                        this.LCRMarkUnit = this.IsOnlyNumber(this.LCRMarkUnit.Substring(this.LCRMarkUnit.Length - 1, 1).ToString()) ? "OHM" : (this.LCRMarkUnit.Substring(this.LCRMarkUnit.Length - 1, 1).ToString() + "OHM");
                                    }
                                    else
                                    {
                                        foreach (string str in Enum.GetNames(typeof(LCRUnit)))
                                        {
                                            if (this.LCRMarkUnit.ToUpper().IndexOf(str.ToUpper()) > 0)
                                            {
                                                this.LCRMarkUnit = (str.ToUpper() == "uF".ToUpper()) ? "μF" : str;
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        this._LCRMarkReadValue = false;
                        i++;
                    }
                    else
                    {
                        this.LCRMarkReadValue = false;
                    }
                }
                string str2 = strMessage[i];
                if (str2 != null)
                {
                    if (!(str2 == "COMU"))
                    {
                        if (str2 == "MAIN")
                        {
                            goto Label_0280;
                        }
                    }
                    else
                    {
                        if (strMessage[i + 1] == "OVER")
                        {
                            this._LCROnline = true;
                        }
                        else if (strMessage[i + 1] == "ON..")
                        {
                            this._LCRStatus = 1;
                        }
                        else
                        {
                            this._LCROnline = false;
                            this._LCRStatus = 0;
                        }
                        i++;
                    }
                }
                continue;
            Label_0280:;
                strArray = strMessage[i + 1].Split(new char[] { ' ' });
                for (int j = 0; j < strArray.Length; j++)
                {
                    if (strArray[j] == "PRIM")
                    {
                        this.LCRMarkValue = 0.0;
                        this.LCRMarkReadValue = true;
                        if (strArray.Length == 2)
                        {
                            this.LCRMarkValue = double.Parse(strArray[j + 1]);
                            j++;
                        }
                        else
                        {
                            this.LCRMarkValue = double.Parse(strArray[j + 2]);
                            j += 2;
                        }
                        i++;
                    }
                    else if (strArray[j] == "TRIG")
                    {
                        this.LCRModeType = (strMessage[i + 1] == "AUTO") ? 1 : 2;
                        i++;
                    }
                    else if (strArray[j] == "MODE")
                    {
                        this.LCRMode = (Mode) Enum.Parse(typeof(Mode), strMessage[i + 2]);
                    }
                }
            }
        }

        public int Baudbits
        {
            get
            {
                return this._Baudbits;
            }
            set
            {
                if (value <= 0)
                {
                    this._Baudbits = 0x9600;
                }
                else
                {
                    this._Baudbits = value;
                }
            }
        }

        public int DataBits
        {
            get
            {
                return this._DataBits;
            }
            set
            {
                if (value <= 0)
                {
                    this._DataBits = 1;
                }
                else
                {
                    this._DataBits = value;
                }
            }
        }

        private bool LCRMarkReadValue
        {
            get
            {
                return this._LCRMarkReadValue;
            }
            set
            {
                if (!value)
                {
                    this._LCRMarkValue = 0.0;
                }
                this._LCRMarkReadValue = value;
            }
        }

        public string LCRminValue
        {
            get 
            {
                return this._LCRminValue;

            }
            set
            {

                this._LCRminValue = value;
            }
        
        }

        public string LCRmaxValue
        {
            get
            {
                return this._LCRmaxValue;

            }
            set
            {

                this._LCRmaxValue = value;
            }



        }
        public string LCRMarkUnit
        {
            get
            {
                return this._LCRMarkUnit;
            }
            set
            {
                this._LCRMarkUnit = value;
            }
        }

        public string EquipmentType
        {
            get
            {
                return this._EquipmentType;
            }
            set
            {
                this._EquipmentType = value;
            }
        }

        public double LCRMarkValue
        {
            get
            {
                return this._LCRMarkValue;
            }
            set
            {
                this._LCRMarkValue = value;
            }
        }

        internal Mode LCRMode
        {
            get
            {
                return this._LCRMode;
            }
            set
            {
                this._LCRMode = value;
            }
        }

        public int LCRModeType
        {
            get
            {
                return this._LCRModeType;
            }
            set
            {
                this._LCRModeType = value;
            }
        }

        public bool LCROnline
        {
            get
            {
                return this._LCROnline;
            }
            set
            {
                this._LCROnline = value;
            }
        }

        public int LCRStatus
        {
            get
            {
                return this._LCRStatus;
            }
            set
            {
                this._LCRStatus = value;
            }
        }

        public bool LCRTRG
        {
            get
            {
                return this._LCRTRG;
            }
            set
            {
                this._LCRTRG = value;
            }
        }

        public string AgilentAddress
        {

            get
            {
                return this._AgilentAddress;
            }
            set
            {
                this._AgilentAddress = value;
            }

        }

        public bool DTR
        {
            get
            {
                return this._DTR;
            }
            set
            {
                this._DTR = value;
            }
        }

        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        public string Parity
        {
            get
            {
                return this._Parity;
            }
            set
            {
                if (value.Equals(""))
                {
                    this._Parity = "None";
                }
                else
                {
                    foreach (string str in Enum.GetNames(typeof(System.IO.Ports.Parity)))
                    {
                        if (value.ToUpper().Trim() == str.ToUpper().Trim())
                        {
                            this._Parity = str;
                            break;
                        }
                    }
                }
            }
        }

        public string PortName
        {
            get
            {
                return this._PortName;
            }
            set
            {
                if (value.Equals(""))
                {
                    this._PortName ="" ;
                }
                else
                {
                    foreach (string str in SerialPort.GetPortNames())
                    {
                        if (str.ToUpper().Trim() == value.ToUpper().Trim())
                        {
                            this._PortName = str;
                            break;
                        }
                    }
                }
            }
        }

        public int StopBits
        {
            get
            {
                return this._StopBits;
            }
            set
            {
                if (value <= 0)
                {
                    this._StopBits = 1;
                }
                else
                {
                    foreach (int num in Enum.GetValues(typeof(System.IO.Ports.StopBits)))
                    {
                        if (value == num)
                        {
                            this._StopBits = value;
                            break;
                        }
                    }
                }
            }
        }

        public enum F
        {
            F = 1,
            mF = 0x3e8,
            nF = 0x3b9aca00,
            uF = 0xf4240
        }

        public enum H
        {
            H = 1,
            mH = 0x3e8,
            nH = 0x3b9aca00,
            uH = 0xf4240
        }

        public enum LCRUnit
        {
            mF,
            uF,
            nF,
            pF,
            F,
            mH,
            uH,
            nH,
            pH,
            H,
            Ω,
            mΩ,
            kΩ,
            MΩ
        }

        public enum Mode
        {
            CD,
            CR,
            LQ,
            ZQ,
            LR,
            RQ,
            NONE
        }

        public enum OHM
        {
            KOHM = 0x3e8,
            mOHM = 0x3b9aca00,
            MOHM = 1,
            OHM = 0xf4240
        }
    }
}

