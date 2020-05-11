using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LCRTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmLogin());

            FrmLogin frm = new FrmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new fMain(frm.userid));
            }
        }
    }
}
