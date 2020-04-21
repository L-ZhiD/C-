using BLL.Manager;
using IBLL.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarketManager
{
    public partial class FrmUpdatePwd : Form
    {
        public FrmUpdatePwd()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        //创建IBLL类
        ISuperMarketAdminManager manager = new SuperMarketAdminManager();
        /// <summary>
        /// 提交修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtOldPwd.CheckNullOrEmpty() * txtNewPwd.CheckNullOrEmpty() * txtRePwd.CheckNullOrEmpty() != 0)
            {
                if (txtOldPwd.Text.Trim().Equals(Program.CurrentAdmin.LoginPwd))
                {
                    //这个方法SetError在SuperTextbox类里面,如果对了则返回一个空字符串（string.Empty表示空字符串）,不会报错
                    txtOldPwd.SetError(string.Empty);
                    //重复密码和新密码一致可以进行修改密码txtNewPwd.Text.Trim().Equals(txtRePwd.Text.Trim()
                    if (txtNewPwd.Text.Trim().Equals(txtRePwd.Text.Trim()))
                    {
                        //Program通过主窗口的CurrentAdmin里面的属性,给其赋值,赋的是原来的密码
                        Program.CurrentAdmin.LoginPwd = txtNewPwd.Text.Trim();
                        //把原来的密码放在这里面进行修改
                        //Program.CurrentAdmin这里面存的数据通过manager.AdminUpdatePwd()里面获取数据
                        if (manager.AdminUpdatePwd(Program.CurrentAdmin))
                        {
                            MessageBox.Show("密码修改成功！请重新登录", "提示");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("密码修改失败！请稍后重试", "提示");
                        }
                    }
                    else
                    {
                        //这个方法SetError在SuperTextbox类里面,返回一个错误的提示
                        txtRePwd.SetError("重复密码和新密码输入不一致！");
                    }
                }
                else
                {
                    //这个方法SetError在SuperTextbox类里面,返回一个错误的提示
                    txtOldPwd.SetError("原密码错误！");
                }
            }
        }
        /// <summary>
        /// 取消修改上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
