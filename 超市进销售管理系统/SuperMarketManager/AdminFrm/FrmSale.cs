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
    public partial class FrmSale : Form
    {
        ISuperMarketAdminManager adminManager = new SuperMarketAdminManager();
        List<SalesPerson> list = null;
        BindingSource source = new BindingSource();
        public FrmSale()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            //刷新
            InitializeSale();
            //封装器在当前绑定项更改时发生
            source.CurrentChanged += Source_CurrentChanged;
        }
        //营业员属性可以返回值
        public SalesPerson Person { get; set; }
        private void Source_CurrentChanged(object sender, EventArgs e)
        {
            //获取列表中的当前项
            Person = source.Current as SalesPerson;
        }
        /// <summary>
        /// 调用数据库中营业员的所有数据
        /// </summary>
        private void InitializeSale()
        {
            list = adminManager.GetSales();
            source.DataSource = list;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = source;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSysAdm_Click(object sender, EventArgs e)
        {
            FrmAddSale addSale = new FrmAddSale();
            addSale.ShowDialog();
            InitializeSale();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateSysAdm_Click(object sender, EventArgs e)
        {
            FrmUpdateSale updateSale = new FrmUpdateSale(Person);
            if (updateSale.ShowDialog() == DialogResult.OK)
            {
                InitializeSale();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
