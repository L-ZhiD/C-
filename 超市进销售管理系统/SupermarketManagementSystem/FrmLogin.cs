using Moder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace SupermarketManagementSystem
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            //屏幕居中
            this.StartPosition = FormStartPosition.CenterParent;
            //固定边框
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //不可随意改变窗体大小
            this.AutoSizeMode = AutoSizeMode.GrowOnly;

            //初始化log4net
            log4net.Config.XmlConfigurator.Configure();
        }
        //实例化IBLL
        IBLL.Cashier.ISuperMarketSaleManager manerger = new BLL.Cashier.SaleManager();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_Click(object sender, EventArgs e)
        {
            //【1】文本框数据的验证
            //如果为0则有必填项未填写！
            if (txtLogId.CheckData(@"^[1-9]\d*$", "账号格式为纯数字！") * txtLogPwd.CheckNullOrEmpty() != 0)
            {
                //【2】登录账号和密码封装成收银员对象
                SalesPerson person = new SalesPerson()
                {
                    SalesPersonId = int.Parse(txtLogId.Text),
                    LoginPwd = txtLogPwd.Text.Trim()
                };
                //【3】数据库中查询
                SalesPerson res = manerger.SalesLogin(person);
                if (res != null)//证明登录成功
                {
                    //(1)将登录对象保存到全局
                    Program.Sale = res;
                    //(2)将登录信息记录进系统日志
                    //记录一下不等于null的数据（获取到收银员的数据）
                    int logId = manerger.WriteSalesLog(new LoginLogs()
                    {
                        LoginId = res.SalesPersonId,
                        SPName = res.SPName,
                        ServerName = Dns.GetHostName()
                    });
                    Program.Sale.LogId = logId;
                    this.DialogResult = DialogResult.OK;
                    Log4net.WriteInfo(string.Format("" + res + ""));
                    this.Close();

                }
                else
                {
                    Log4net.WriteInfo(string.Format("" + res + ""));
                    MessageBox.Show("账号或密码错误！", "登录提示");
                }
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
