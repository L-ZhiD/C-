using BLL.Cashier;
using Common;
using IBLL.Cashier;
using Moder;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //关闭窗体前发生
            this.FormClosing += FrmMain_FormClosing;
            this.FormClosed += FrmMain_FormClosed;
            //在底下的栏里获取管理者的名字
            toollblUser.Text = $"【{Program.CurrentAdmin.AdminName}】";
            this.IsMdiContainer = true;
        }


        #region 负责开启功能窗体
        //记录当前开启的功能窗体
        Form currentMDIChild = null;
        void ShowMDIChild(Form mdiForm)
        {
            if (currentMDIChild != null)
            {
                currentMDIChild.Close();
            }
            currentMDIChild = mdiForm;
            mdiForm.MdiParent = this;
            mdiForm.Parent = panel1;
            mdiForm.Left = panel1.Width / 2 - mdiForm.Width / 2;
            mdiForm.Top = panel1.Height / 2 - mdiForm.Height / 2;
            mdiForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            mdiForm.Show();
        }
        #endregion

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath, "restart");
        }

        #region 主程序退出
        private void toolMenuExitSys_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

           #region 主程序退出的时候做什么事情:【用数据库里的日志记录管理者退出时间】或【直接退出】
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISuperMarketSaleManager saleManager = new SaleManager();
            Log4net.WriteInfo($"[{Program.CurrentAdmin.LoginId}]退出程序！");
            saleManager.WriteSalesExitLog(Program.CurrentAdmin.LoginLogId);
        }
        #endregion

        #region 系统时间更新
        private void timer1_Tick(object sender, EventArgs e)
        {
            toollblTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
        #endregion

        #region 修改密码
        private void toolMenuUpdatePwd_Click(object sender, EventArgs e)
        {
            FrmUpdatePwd pwd = new FrmUpdatePwd();
            DialogResult Restart = pwd.ShowDialog();
            //密码修改成功，意味着需要重新登录
            if (Restart == DialogResult.OK)
            {
                Log4net.WriteInfo($"[{Program.CurrentAdmin.LoginId}]成功修改密码");

                this.Close();//主线程关闭
                //修改密码之后重启

            }
        }
        #endregion

        #region 用户管理模块
        private void toolMenuUserManager_Click(object sender, EventArgs e)
        {
            //用户管理模块(没有添加功能)
            AdminFrm.FrmAdmin admin = new AdminFrm.FrmAdmin();
            //负责开启功能窗体的方法，并显示子窗口
            ShowMDIChild(admin);
        }
        #endregion

        #region 添加商品功能
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            FrmAddProduct addProduct = new FrmAddProduct();
            addProduct.FormClosed += AddProduct_FormClosed;
            ShowMDIChild(addProduct);
        }
        #endregion

        #region 商品入库功能
        Produts currentProduct = null;
        private void AddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmAddProduct frmAdd = sender as FrmAddProduct;
            if (frmAdd.DialogResult == DialogResult.OK)
            {
                currentProduct = frmAdd.Tag as Produts;
                frmAdd.DialogResult = DialogResult.Cancel;
                btnIntoProduct_Click(frmAdd, null);
            }
        }
        private void btnIntoProduct_Click(object sender, EventArgs e)
        {
            FrmIntoProduct intoProduct = new FrmIntoProduct();
            if (currentProduct != null)
            {
                intoProduct.txtProductId.Text = currentProduct.ProductId;
                intoProduct.txtProductName.Text = currentProduct.ProductName;
            }
            ShowMDIChild(intoProduct);
            currentProduct = null;
        }

        #endregion

        #region 商品入库功能
        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            ProducFrm.FrmProductProtrct productProtrct = new ProducFrm.FrmProductProtrct();
            ShowMDIChild(productProtrct);
        }
        #endregion

        #region 添加会员
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            //添加会员窗口
            FrmAddMember frm = new FrmAddMember();
            //负责开启功能窗体
            ShowMDIChild(frm);
        }
        #endregion

        #region 日志查询
        private void btnCheckLog_Click(object sender, EventArgs e)
        {
            AdminFrm.FrmLogCheck logCheck = new AdminFrm.FrmLogCheck();
            ShowMDIChild(logCheck);
        }
        #endregion

        #region 用户管理模块
        /// <summary>
        /// 营业员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSaleManager_Click_1(object sender, EventArgs e)
        {
            AdminFrm.FrmSale sale = new AdminFrm.FrmSale();
            ShowMDIChild(sale);
        }
        /// <summary>
        /// 系统管理者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSysManager_Click_1(object sender, EventArgs e)
        {
            if (Program.CurrentAdmin.Roleld == 1)
            {
                AdminFrm.FrmAdmin admin = new AdminFrm.FrmAdmin();
                ShowMDIChild(admin);
            }
            else
            {
                MessageBox.Show("您无权限操作该功能！", "提示");
            }
        }
        #endregion
    }
}

