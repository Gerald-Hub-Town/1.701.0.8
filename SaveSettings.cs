using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace LCRTest
{
  internal sealed class SaveSettings:ApplicationSettingsBase
    {
      //private static SaveSettings a = ((SaveSettings)SettingsBase.Synchronized(new SaveSettings()));

      [UserScopedSetting,DefaultSettingValue("38400")]
        public string BaudRate
        {
            get 
            {
                return (string)this["BaudRate"];
            }
            set
            {
                this["BaudRate"] = value;
            }
        }

      [UserScopedSetting, DefaultSettingValue("1")]
      public string StopBit
      {
          get
          {
              return (string)this["StopBit"];
          }
          set
          {
              this["StopBit"] = value;
          }
      }

      //[UserScopedSetting, DefaultSettingValue("8")]
      //public string DataBit
      //{
      //    get
      //    {
      //        return (string)this["DataBit"];
      //    }
      //    set
      //    {
      //        this["DataBit"] = value;
      //    }
      //}
      //[UserScopedSetting, DefaultSettingValue("None")]
      //public string Parity
      //{
      //    get
      //    {
      //        return (string)this["Parity"];
      //    }
      //    set
      //    {
      //        this["Parity"] = value;
      //    }
      //}
      //[UserScopedSetting, DefaultSettingValue("COM1")]
      //public string PortName
      //{
      //    get
      //    {
      //        return (string)this["PortName"];
      //    }
      //    set
      //    {
      //        this["PortName"] = value;
      //    }
      //}
    }
}
