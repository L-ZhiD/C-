using Moder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketManager
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
            //禁止程序多次启动，一个程序运行一个进程，当this.Close()主线程关闭,而进程并没有彻底关闭，然而重启是由主线程中的一个方法支持，继续启动了一个重复的线程
            Process[] processList = Process.GetProcesses();
            int currentCount = 0;
            foreach (Process item in processList)
            {
                if (item.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    currentCount += 1;
                }
            }
            if (currentCount > 1)
            {
                Application.Exit();
                return;
            }
            FrmLogin frm = new FrmLogin();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }
        }
        public static SysAdmins CurrentAdmin { get; set; } = null;
        public static ProductCategory ClockCategory { get; set; } = null;
    }
}
