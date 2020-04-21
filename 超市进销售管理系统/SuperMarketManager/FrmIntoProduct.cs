
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

namespace SuperMarketManager
{
    public partial class FrmIntoProduct : Form
    {
        public FrmIntoProduct()
        {
            InitializeComponent();
            txtProductId.Focus();
            txtProductId.LostFocus += TxtProductId_LostFocus;
        }

        private void TxtProductId_LostFocus(object sender, EventArgs e)
        {
            if (txtProductId.CheckData(@"^\d+$", "商品编号录入有误") == 1)
            {
                Produts produts = manager.GetProductWithId(txtProductId.Text.Trim());
                if (produts == null)
                {
                    MessageBox.Show("商品编号录入有误,未查询到对应商品！", "提示");
                    txtProductId.SelectAll();
                    txtProductId.Focus();
                    return;
                }
                else
                {
                    txtProductName.Text = produts.ProductName;
                }
            }
        }

        ISuperMarketProductManager manager = new SuperMarketProductManager();
        /// <summary>
        /// 确认入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtProductId.CheckNullOrEmpty() * txtProductName.CheckNullOrEmpty() != 0)
            {
                if (txtCount.CheckData(@"^(-?[1-9]\d*)$", "入库数量为整数") != 0)
                {
                    if (manager.InventoryProduct(txtProductId.Text.Trim(), Convert.ToInt32(txtCount.Text.Trim())))
                    {
                        if (MessageBox.Show("商品入库成功！是否继续入库", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            txtCount.Text = "0";
                            txtProductId.Text = txtProductName.Text = "";
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("商品入库失败！", "提示");
                    }
                }
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
