using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Manager;
using Common;
using IBLL.Manager;
using Moder;

namespace SuperMarketManager
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        ISuperMarketAdminManager adminManager = new SuperMarketAdminManager();
        /// <summary>
        /// 登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLoginId.CheckData(@"^\\d+$", "账号输入有误！") * txtLoginPwd.CheckNullOrEmpty() != 0)
            {
                SysAdmins sys = new SysAdmins()
                {
                    LoginId = Convert.ToInt32(txtLoginId.Text.Trim()),
                    LoginPwd = txtLoginPwd.Text.Trim()
                };

                try
                {
                    sys = adminManager.AdminLogin(sys);
                    Log4net.WriteInfo($"账号[{sys.LoginId}]开始登录");
                    if (sys != null)
                    {
                        if (sys.AdminStatus == 1)
                        {
                            Log4net.WriteInfo($"[{sys.LoginId}]登录成功！");
                            Program.CurrentAdmin = sys;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            Log4net.WriteInfo($"[{sys.LoginId}]账号被禁用");
                            MessageBox.Show("当前管理员账号已被禁用！", "登录提示");
                        }
                    }
                    else
                    {
                        Log4net.WriteInfo($"[{sys.LoginId}]账号或密码错误登录失败");
                    }
                }
                catch (Exception ex)
                {
                    Log4net.WriteError($"[{sys.LoginId}]登录发生异常", ex);
                    return;
                }
            }
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
