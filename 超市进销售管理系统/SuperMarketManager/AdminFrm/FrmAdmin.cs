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
    public partial class FrmAdmin : Form
    {
        ISuperMarketAdminManager adminManager = new SuperMarketAdminManager();
        public FrmAdmin()
        {
            InitializeComponent();
            //窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
            //是否自动创建列
            dataGridView1.AutoGenerateColumns = false;
            //获取管理者的数据
            InitializeUser();
            source.CurrentChanged += Source_CurrentChanged;
        }
        //管理者类
        SysAdmins currentAdm = null;
        //在当前绑定项更改时发生
        private void Source_CurrentChanged(object sender, EventArgs e)
        {
            //获取列表中的当前项
            currentAdm = source.Current as SysAdmins;
        }
        //管理者的泛型储存
        List<SysAdmins> adminList = null;
        //source封装器
        BindingSource source = new BindingSource();
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void InitializeUser()
        {
            adminList = adminManager.GetAdmins();
            source.DataSource = adminList;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = source;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddAdmin addAdmin = new FrmAddAdmin();
            addAdmin.ShowDialog();
            InitializeUser();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0 || currentAdm == null)
            {
                MessageBox.Show("请选择要修改的用户！");
                return;
            }
            //管理者的修改
            FrmUpdateAdmin updateAdmin = new FrmUpdateAdmin(currentAdm);
            if (updateAdmin.ShowDialog() == DialogResult.OK)
            {
                InitializeUser();
            }
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 0表示没有选中管理者角色类型1超级2一般
            if (dataGridView1.RowCount == 0 || currentAdm == null || currentAdm.Roleld == 1)
            {
                return;
            }
            //管理者当前状态1启0禁
            currentAdm.AdminStatus = 0;
            if (adminManager.SetSysStatus(currentAdm))
            {
                //调用管理者和营业员的表显示：相当于刷新dataGridView1里面的数据
                InitializeUser();
            }
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0 || currentAdm == null || currentAdm.Roleld == 1)
            {
                return;
            }
            currentAdm.AdminStatus = 1;
            //在禁用状态时，进入数据库改成禁用状态
            if (adminManager.SetSysStatus(currentAdm))
            {
                InitializeUser();
            }
        }
    }
}
