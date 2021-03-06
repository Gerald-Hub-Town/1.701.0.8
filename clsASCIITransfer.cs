﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LCRTest
{
    class clsASCIITransfer
    {
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组          
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开            
            { 
                result += (Convert.ToString(b[i], 16).Length <= 1 ? "0" + Convert.ToString(b[i], 16) : Convert.ToString(b[i], 16)) + " "; 
            }
            return result;        
        }
    }
}
