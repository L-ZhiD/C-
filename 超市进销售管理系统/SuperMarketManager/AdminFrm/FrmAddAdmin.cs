using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBLL.Manager;
using BLL.Manager;
using Moder;

namespace SuperMarketManager.AdminFrm
{
   
    public partial class FrmAddAdmin : Form
    {
        ISuperMarketAdminManager adminManager = new SuperMarketAdminManager();
        public FrmAddAdmin()
        {
            InitializeComponent();
            //获取焦点，获取管理者的添加的焦点
            txtAdmName.Focus();
            //下拉框Combox获取指定索引位置为0
            cmbAdminRole.SelectedIndex = 0;
            //密码获取焦点时触发
            txtAdmPwd.GotFocus += TxtAdmPwd_GotFocus;
        }

        private void TxtAdmPwd_GotFocus(object sender, EventArgs e)
        {
            //选中密码文本框的内容
            txtAdmPwd.SelectAll();
        }
        /// <summary>
        /// 添加管理者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtAdmName.CheckNullOrEmpty() * txtAdmPwd.CheckData(@"^\w{6,}$", "密码必须为6位字母、数字、下划线组合") == 0)
            {
                return;
            }
            else
            {
                SysAdmins admin = new SysAdmins()
                {
                    AdminName = txtAdmName.Text.Trim(),
                    AdminStatus = 1,
                    LoginPwd = txtAdmPwd.Text.Trim(),
                    Roleld = cmbAdminRole.SelectedIndex + 1
                };
                admin = adminManager.InsertAdmin(admin);
                if (admin == null)
                {
                    MessageBox.Show("用户添加失败！", "提示");
                    return;
                }
                else
                {

                    if (MessageBox.Show($"添加成功！登录账号为【{admin.LoginId}】\r\n是否继续添加", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        txtAdmName.Text = "";
                        txtAdmPwd.Text = "123456";
                        cmbAdminRole.SelectedIndex = 0;
                        return;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 取消添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
