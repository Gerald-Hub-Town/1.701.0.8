using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace ATE
{
   public class ENVCtrl
    {
        [DllImport("visa32.dll", CharSet = CharSet.Ansi)]
        public static extern int viOpenDefaultRM(out int sesn);

        [DllImport("visa32.dll", CharSet = CharSet.Ansi)]
        public static extern int viOpen(int sesn, string viDesc, int mode, int timeout, out int vi);

        [DllImport("visa32.dll", CharSet = CharSet.Ansi)]
        public static extern int viRead(int vi, byte[] buffer, int count, out int retCount);

        [DllImport("visa32.dll", CharSet = CharSet.Ansi)]
        public static extern int viWrite(int vi, byte[] buffer, int count, out int retCount);

        [DllImport("visa32.dll", CharSet = CharSet.Ansi)]
        public static extern int viClose(int vi);

        private int PSANTvi = 0;
        private int PSdefANT = 0;

        private int TIANTvi = 0;
        private int TIdefANT = 0;
        private string address = "";
        public ENVCtrl()
        {
            //OpenPowerSupplyGPIB();
            //OpenTestInstrumentGPIB();
        }

        public bool OpenPowerSupplyGPIB( string address)
        {
            if (PSANTvi > 0) // invalid
            {
                return true; // GPIB is open
            }

            int viState = viOpenDefaultRM(out PSdefANT);

            //viState = viOpen(PSdefANT, "USB0::0x0957::0x1609::MY52402599::0::INSTR", 0, 0, out PSANTvi);
            viState = viOpen(PSdefANT, address, 0, 0, out PSANTvi);

            if (viState != 0)
            {
            
                viState = viOpen(PSdefANT, address, 0, 0, out PSANTvi);
            }

            if (viState != 0)
            {
                return false;
            }
            //if (viState != VISA32.VI_SUCCESS)
            //{
            //    switch (viState)
            //    {
            //        case VISA32.VI_ERROR_SYSTEM_ERROR:
            //            MessageBox.Show("[ERROR]: The VISA system failed to initialize.");
            //            break;
            //        case VISA32.VI_ERROR_ALLOC:
            //            MessageBox.Show("[ERROR]: Insufficient system resources to create a defaultRM to the Default Resource Manager resource.");
            //            break;
            //        case VISA32.VI_ERROR_INV_SETUP:
            //            MessageBox.Show("[ERROR]: Some implementation-specific configuration file is corrupt or does not exist");
            //            break;
            //        default:
            //            MessageBox.Show("[ERROR]: Unknown error while opening default resource manager defaultRM.");
            //            break;
            //    }
            //    return false;

            //}
            return true;
        }

        public bool OpenPowerSupplyGPIB()
        {
            if (PSANTvi > 0) // invalid
            {
                return true; // GPIB is open
            }

            int viState = viOpenDefaultRM(out PSdefANT);

            //viState = viOpen(PSdefANT, "USB0::0x0957::0x1609::MY52402599::0::INSTR", 0, 0, out PSANTvi);
            viState = viOpen(PSdefANT, address, 0, 0, out PSANTvi);

            if (viState != 0)
            {

                viState = viOpen(PSdefANT, address, 0, 0, out PSANTvi);
            }

            if (viState != 0)
            {
                return false;
            }
            //if (viState != VISA32.VI_SUCCESS)
            //{
            //    switch (viState)
            //    {
            //        case VISA32.VI_ERROR_SYSTEM_ERROR:
            //            MessageBox.Show("[ERROR]: The VISA system failed to initialize.");
            //            break;
            //        case VISA32.VI_ERROR_ALLOC:
            //            MessageBox.Show("[ERROR]: Insufficient system resources to create a defaultRM to the Default Resource Manager resource.");
            //            break;
            //        case VISA32.VI_ERROR_INV_SETUP:
            //            MessageBox.Show("[ERROR]: Some implementation-specific configuration file is corrupt or does not exist");
            //            break;
            //        default:
            //            MessageBox.Show("[ERROR]: Unknown error while opening default resource manager defaultRM.");
            //            break;
            //    }
            //    return false;

            //}
            return true;
        }

        public bool OpenTestInstrumentGPIB()
        {
            if (TIANTvi > 0) // invalid
            {
                return true; // GPIB is open
            }

            int viState = viOpenDefaultRM(out TIdefANT);

            viState = viOpen(TIdefANT, "GPIB0::1::INSTR", 0, 0, out TIANTvi);

            if (viState != 0)
            {
                viState = viOpen(TIdefANT, "GPIB0::1::INSTR", 0, 0, out TIANTvi);
            }

            if (viState != 0)
            {
                return false;
            }
            return true;
        }

        public string ReadVoltage()
        {
            WriteGPIB("Measure:Voltage?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }


        public string ReadLcrIAC()
        {
            WriteGPIB(":FETCH:SMONitor:IAC?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }


        public string ReadLcrIDC()
        {
            WriteGPIB(":FETCH:SMONitor:IDC?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }


        public string ReadLcRVAC()
        {
            WriteGPIB(":FETCH:SMONitor:VAC?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }

        public string ReadLcRVDC()
        {
            WriteGPIB(":FETCH:SMONitor:VDC?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }



        public string ReadLcRValue()
        {
            WriteGPIB(":FETCH?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }
        public void SendSYSTRem()
        {
            WriteGPIB("SYST:REM\n");

        }

        public string ReadLcR34420Value()
        {          
            WriteGPIB(":READ?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }

        public void SendSYSTlocal()
        {
            WriteGPIB("SYST:LOC\n");

        }

        public string ReadLcRValue1()
        {
            WriteGPIB("FETCh:IMPedance:FORMatted?\n");
            string voltageValue = ReadGPIB();
            return voltageValue;
        }

        public string ReadCurrent()
        {
            WriteGPIB("Measure:CURRENT?\n");
            string CurrentValue = ReadGPIB();
            return CurrentValue;
        }

        /// <summary>
        /// :FREQuency 1MHz
        /// </summary>
        /// <returns></returns>
        /// 
        public string SetFreq()
        {
            WriteGPIB(":FREQuency 1MHz\n");
            string CurrentValue = ReadGPIB();
            return CurrentValue;
        }

        //public bool setVoltage()
        //{
        //    int status = WriteGPIB("VOLTAGE " + Convert.ToDouble(Properties.ENV_Instrument.Default.PowerNormalVoltage));
        //    return (status == 0) ? true : false;
        //}

        //public bool SetCurrentLimit()
        //{
        //    int status = WriteGPIB("CURRENT " + Convert.ToDouble(Properties.ENV_Instrument.Default.PowerCurrentLimit));
        //    return (status == 0) ? true : false;
        //}

        public string ReadGPIB()
        {
            int readIn = 0;
            byte[] buffer = new byte[256];
            string readString;

            try
            {
                viRead(PSANTvi, buffer, buffer.Length, out readIn);
            }
            catch (Exception ex)
            {
                CloseGPIB();
            }

            if (readIn > 0)
            {
                readString = Encoding.Default.GetString(buffer, 0, readIn);
                return readString;
            }
            return "";
        }

        public int WriteGPIB(string cmd)
        {
            int size, writeState;
            byte[] buffer = Encoding.Default.GetBytes(cmd);
            try
            {
                writeState = viWrite(PSANTvi, buffer, buffer.Length, out size);
            }
            catch (Exception ex)
            {
                CloseGPIB();
                writeState = -1;
            }
            return writeState;
        }

        public bool CloseGPIB()
        {
            if (PSANTvi > 0)
            {
                viClose(PSANTvi);
                PSANTvi = 0; // must reopen GPIB
            }
            return true;
        }

        public bool OutputOn()
        {
            int status = WriteGPIB("Output On");

            return (status == 0) ? true : false;
        }

        public bool OutputOff()
        {
            int status = WriteGPIB("Output Off");

            return (status == 0) ? true : false;
        }

        public bool InitInstrument(float current, float voltage)
        {
            if (!OpenPowerSupplyGPIB())
            {
                return false;
            }

            try
            {
                //if (!SetCurrentLimit() && !setVoltage() && !OutputOff())
                //{
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                CloseGPIB();
                return false;
            }
            return true;
        }

        public bool DeInitInstrument()
        {
            if (PSANTvi != 0)
            {
                if (!CloseGPIB())
                {
                    return false;
                }
            }
            if (PSdefANT != 0)
            {
                if (!CloseGPIB())
                {
                    return false;
                }
            }
            return true;
        }

        //RelayBoard
        public bool RBSwitch()
        {
            try
            {
                return true;

            }
            catch (System.Exception exp)
            {
                return false;
            }
            finally
            {

            }
        }
    }
}
