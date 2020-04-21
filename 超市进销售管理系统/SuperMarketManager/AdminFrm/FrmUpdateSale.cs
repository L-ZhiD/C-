using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moder;
using IBLL.Manager;
using BLL.Manager;

namespace SuperMarketManager.AdminFrm
{
    public partial class FrmUpdateSale : Form
    {
        //获取管理者和营业员中的各种方法
        ISuperMarketAdminManager adminManager = new SuperMarketAdminManager();
        public FrmUpdateSale(SalesPerson person)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Sales = person;
            txtAdmName.Text = person.SPName;
            txtAdmPwd.Text = person.LoginPwd;

        }
        //营业员属性
        public SalesPerson Sales { get; set; }
        /// <summary>
        /// 取消添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click_1(object sender, EventArgs e)
        {
            if (txtAdmName.CheckNullOrEmpty() * txtAdmPwd.CheckData(@"^\w{6,}$", "密码必须为6位字母、数字、下划线组合") != 0)
            {
                Sales.SPName = txtAdmName.Text.Trim();
                Sales.LoginPwd = txtAdmPwd.Text.Trim();
                if (adminManager.UpdateSales(Sales) != null)
                {
                    MessageBox.Show("修改成功！", "提示");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败！", "提示");
                }
            }
        }
    }
}
