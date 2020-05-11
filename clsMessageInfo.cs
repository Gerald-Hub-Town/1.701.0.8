using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
namespace LCRTest
{
    
    public  class clsMessageInfo
    {
        public enum SoundTypeList { None, AutoSet, Custom };
        private static SoundTypeList soundtype = SoundTypeList.AutoSet;
        [DllImport("winmm.dll ")]
        private static extern bool sndPlaySound(string FileName, int fuSound);
        /// <summary>
        /// 根据类型，设置图片，颜色和text
        /// </summary>
        /// <param name="_Type"></param>
        /// <param name="MsgTxt"></param>
        /// <param name="MsgPic"></param>
        /// <param name="_Message"></param>
        public static void setMessageinfo(MessageType _Type, ref TextBox MsgTxt, ref PictureBox MsgPic, string _Message)
        {
            MsgTxt.Text = _Message;
            switch (_Type)
            {
                case MessageType.Error:

                    
                    MsgTxt.BackColor = System.Drawing.Color.Red;
                    MsgPic.Image = global::LCRTest.Properties.Resources.IMAGE_NG;
                    SoundPlayer spsuceed = new SoundPlayer(Application.StartupPath + "/" + "error.wav");
                    spsuceed.LoadAsync();
                    spsuceed.PlaySync();
                    break;
                case MessageType.Information:
                    MsgTxt.BackColor = System.Drawing.Color.Green;
                    MsgPic.Image = global::LCRTest.Properties.Resources.IMAGE_OK;
                    SoundPlayer spsuceed1 = new SoundPlayer(Application.StartupPath + "/" + "Pass.wav");
                    spsuceed1.LoadAsync();
                    spsuceed1.PlaySync();
                    break;
                case MessageType.Input:
                    MsgTxt.BackColor = System.Drawing.Color.RoyalBlue;
                    MsgPic.Image = global::LCRTest.Properties.Resources.IMAGE_INPUT;
                    break;

            }

        }
        public static void txtSetFocus(ref TextBox txtFocus)
        {
            txtFocus.Focus();
            txtFocus.SelectAll();
        }
    }
    public enum MessageType
    {
        Error,
        Information,
        Input,
        Warning
    }
}
