using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Collections;
using System.Xml;
using System.Runtime.InteropServices;
using System.Security.Permissions;


namespace LCRTest
{
    class clsINISetting
    {
        #region "API Declare"

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSection(string lpApplicationName, [MarshalAs(UnmanagedType.LPStr)]string lpBuffer, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileSectionNames([MarshalAs(UnmanagedType.LPStr)]string lpBuffer, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]

        private static extern int GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileIntW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]

        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int lpDefault, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileSectionW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]

        private static extern int GetPrivateProfileSection(string lpApplicationName, byte[] lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileSectionNamesW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]

        private static extern int GetPrivateProfileSectionNames(byte[] lpRetArray, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]

        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region "Global Variables definition"

        #endregion

        #region "Constructor and Destructor"

        #endregion

        #region "Function"

        public static string INIRead(string INIpath, string sectionName, string keyName, string defaultValue)
        {
            if ((!(System.IO.File.Exists(INIpath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return defaultValue;
            }

            string returnValue = string.Empty;

            int n = 0;
            string sData = string.Empty;
            sData = new string((char)0, 1024);
            n = GetPrivateProfileString(sectionName, keyName, defaultValue, sData, sData.Length, INIpath);
            if (n > 0)
            {
                returnValue = sData.Substring(0, n);
            }
            else
            {
                returnValue = defaultValue;
            }

            return returnValue;
        }


        public static int INIReadInteger(string INIpath, string sectionName, string keyName, int defaultValue)
        {
            if ((!(System.IO.File.Exists(INIpath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return defaultValue;
            }

            int returnValue = defaultValue;

            int n;

            n = GetPrivateProfileInt(sectionName, keyName, defaultValue, INIpath);
            returnValue = n;

            return returnValue;
        }


        public static bool INIReadBoolean(string INIpath, string sectionName, string keyName, bool defaultValue)
        {
            if ((!(System.IO.File.Exists(INIpath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return defaultValue;
            }

            int n = 0;
            string sData = string.Empty;
            string sValue;
            sData = new string((char)0, 1024);
            n = GetPrivateProfileString(sectionName, keyName, bool.TrueString, sData, sData.Length, INIpath);
            if (n > 0)
            {
                sValue = sData.Substring(0, n);
            }
            else
            {
                sValue = "";
            }

            if (sValue.Trim().ToLower() == "true")
            {
                return true;
            }
            else if (sValue.Trim().ToLower() == "false")
            {
                return false;
            }
            else
            {
                return defaultValue;
            }

        }


        public static string INIWrite(string INIPath, string sectionName, string keyName, string theValue)
        {
            if ((!(System.IO.File.Exists(INIPath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return "Parameter error";
            }

            try
            {
                WritePrivateProfileString(sectionName, keyName, theValue, INIPath);
                return theValue;
            }
            catch
            {
                return "error";
            }
        }


        public static int INIWriteInteger(string INIPath, string sectionName, string keyName, int theValue)
        {
            if ((!(System.IO.File.Exists(INIPath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return -1;
            }

            try
            {
                WritePrivateProfileString(sectionName, keyName, theValue.ToString(), INIPath);
                return theValue;
            }
            catch
            {
                return -1;
            }
        }


        public static bool INIWriteBoolean(string INIPath, string sectionName, string keyName, bool theValue)
        {
            if ((!(System.IO.File.Exists(INIPath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return false;
            }

            try
            {
                WritePrivateProfileString(sectionName, keyName, theValue.ToString(), INIPath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static string INIDelete(string INIPath, string sectionName, string keyName)
        {
            if ((!(System.IO.File.Exists(INIPath))) || string.Equals(sectionName, string.Empty) || string.Equals(keyName, string.Empty))
            {
                return "Parameter error";
            }

            try
            {
                WritePrivateProfileString(sectionName, keyName, null, INIPath);
                return "success";
            }
            catch
            {
                return "error";
            }
        }


        public static string INIDelete(string INIPath, string sectionName)
        {
            if ((!(System.IO.File.Exists(INIPath))) || string.Equals(sectionName, string.Empty))
            {
                return "Parameter error";
            }

            try
            {
                WritePrivateProfileString(sectionName, null, null, INIPath);
                return "success";
            }
            catch
            {
                return "error";
            }
        }


        public static bool INIGetPrivateProfileSection(string lpApplicationName, ref ArrayList lpRetArray, int nSize, string lpFileName)
        {
            if ((!(System.IO.File.Exists(lpFileName))) || string.Equals(lpApplicationName, string.Empty) || nSize <= 0)
            {
                return false;
            }

            byte[] sRtrnCode = new byte[nSize + 1];
            int lRet = 0;
            int Pos = 0;
            int iCount = 0;

            try
            {
                lRet = GetPrivateProfileSection(lpApplicationName, sRtrnCode, nSize, lpFileName);
                if (lRet > 0)
                {
                    string s;
                    string r;
                    string[] tmp = new string[lRet + 1 + 1];
                    s = System.Text.Encoding.Default.GetString(sRtrnCode);
                    //tmp = s.Split(Chr(0) & Chr(0))
                    tmp = s.Split('\0');

                    if (lpRetArray.Count > 0)
                    {
                        lpRetArray.Clear();
                    }
                    s = "";
                    iCount = 0;
                    foreach (string tempLoopVar_r in tmp)
                    {
                        r = tempLoopVar_r;
                        if (iCount > 5) //> 5 spaces, exit
                        {
                            //goto endOfForLoop;
                            continue;
                        }
                        if (r != "")
                        {
                            s = s + r;
                        }
                        else
                        {
                            if (s != "")
                            {
                                Pos = s.IndexOf("=");
                                if (Pos > 0)
                                {
                                    s = s.Substring(0, Pos).Trim();
                                }
                                lpRetArray.Add(s);
                                s = "";
                                iCount = 0;
                            }
                            iCount++;
                        }
                    }
                    //endOfForLoop:
                    if (s != "")
                    {
                        Pos = s.IndexOf("=");
                        if (Pos > 0)
                        {
                            s = s.Substring(0, Pos).Trim();
                        }
                        lpRetArray.Add(s);
                    }
                    tmp = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //ex = null;
                return false;
            }
            finally
            {
                sRtrnCode = null;
            }

        }


        public static bool INIGetPrivateProfileSectionNames(ArrayList lpRetArray, int nSize, string lpFileName)
        {
            if ((!(System.IO.File.Exists(lpFileName))) || nSize <= 0)
            {
                return false;
            }

            byte[] sRtrnCode = new byte[nSize + 1];
            int lRet = 0;
            int iCount = 0;

            try
            {
                lRet = GetPrivateProfileSectionNames(sRtrnCode, nSize, lpFileName);
                if (lRet > 0)
                {
                    string s;
                    string r;
                    string[] tmp = new string[lRet + 1 + 1];
                    s = System.Text.Encoding.Default.GetString(sRtrnCode);
                    //tmp = s.Split(Chr(0) & Chr(0))
                    tmp = s.Split('\0');

                    if (lpRetArray.Count > 0)
                    {
                        lpRetArray.Clear();
                    }
                    s = "";
                    iCount = 0;
                    foreach (string tempLoopVar_r in tmp)
                    {
                        r = tempLoopVar_r;
                        if (iCount > 5) //> 5 spaces, exit
                        {
                            //goto endOfForLoop;
                            continue;
                        }
                        if (r != "")
                        {
                            s = s + r;
                        }
                        else
                        {
                            if (s != "")
                            {
                                lpRetArray.Add(s);
                                s = "";
                                iCount = 0;
                            }
                            iCount++;
                        }
                    }
                    //endOfForLoop:
                    if (s != "")
                    {
                        lpRetArray.Add(s);
                    }
                    tmp = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //ex = null;
                return false;
            }
            finally
            {
                sRtrnCode = null;
            }
        }

        #endregion

        #region "INIRead Overloads"
        /*
		
		public static string INIRead(string INIpath, string sectionName, string keyName)
		{
			return INIRead(INIpath, sectionName, keyName, "");
		}
		
		
		public static string INIRead(string INIpath, string sectionName)
		{
			return INIRead(INIpath, sectionName, null, "");
		}
		
		
		public static string INIRead(string INIpath)
		{
			return INIRead(INIpath, null, null, "");
		}
		
		//*/
        #endregion

    }

    public class KernelAccess
    {

        #region "API Declare"

        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_HIDE = 0;
        public const int SW_MAXIMIZE = 3;
        public const int SW_MINIMIZE = 6;
        public const int SW_RESTORE = 9;
        public const int SW_SHOW = 5;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOWNORMAL = 1;

        #region "Structures"

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public int lpReserved2;
            public int hStdInput;
            public int hStdOutput;
            public int hStdError;
        }


        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        #endregion

        [method: System.CLSCompliant(false)]
        [DllImport("kernel32.dll")]
        static public extern bool CreateProcess(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, UInt32 dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In()]ref STARTUPINFO lpStartupInfo, [In, Out()]ref PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        static public extern bool CloseHandle(IntPtr hProcess);

        [DllImport("kernel32.dll")]
        static public extern bool GetExitCodeProcess(IntPtr hProcess, [In, Out()]ref long lpExitCode);

        [DllImport("kernel32.dll")]
        static public extern void ExitProcess(long lpExitCode);

        [DllImport("User32.dll")]
        static public extern long WaitForInputIdle(IntPtr hProcess, long dwMilliseconds);

        [DllImport("kernel32.dll")]
        static public extern long WaitForSingleObject(IntPtr hProcess, long dwMilliseconds);

        [DllImport("kernel32.dll")]
        static public extern long GetLastError();

        [DllImport("kernel32.dll")]
        static public extern long GetTickCount();

        [DllImport("kernel32.dll")]
        static public extern bool Beep(int freq, int dur);

        #endregion

        #region "Global Variables definition"

        #endregion

        #region "Constructor and Destructor"

        #endregion

        #region "Function"

        #endregion
    }

    public class User32Access
    {

        #region "API Declare"
        public const int WM_USER = 0x400;
        public const int EM_EXGETSEL = WM_USER + 52;
        public const int EM_LINEFROMCHAR = 0xC9;
        public const int EM_LINEINDEX = 0xBB;
        public const int EM_GETSEL = 0xB0;

        //Declare Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        [DllImport("user32.dll")]
        static public extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        static public extern bool SendNotifyMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static public extern bool SendMessageCallback(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam, IntPtr lpCallBack, IntPtr dwData);
        //Declare Function SendMessage Lib "coredll.dll" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr

        //[DllImport("user32.dll")]static public extern int MessageBeep(uint n);

        #endregion

        #region "Global Variables definition"

        #endregion

        #region "Constructor and Destructor"

        #endregion

        #region "Function"
        public static IntPtr SendMessageA(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return SendMessage(hWnd, Msg, wParam, lParam);
        }

        public static bool SendNotifyMessageA(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return SendNotifyMessage(hWnd, Msg, wParam, lParam);
        }

        public static bool SendMessageCallbackA(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam, IntPtr lpCallBack, IntPtr dwData)
        {
            return SendMessageCallback(hWnd, Msg, wParam, lParam, lpCallBack, dwData);
        }
        #endregion
    }
}
