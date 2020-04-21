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
    public partial class FrmAddProduct : Form
    {
        public FrmAddProduct()
        {
            InitializeComponent();
            InitializeProduct();
            txtProductId.GotFocus += TxtProductId_GotFocus;
            txtDiscount.GotFocus += TxtProductId_GotFocus;
            txtMaxCount.GotFocus += TxtProductId_GotFocus;
            txtMinCount.GotFocus += TxtProductId_GotFocus;
            txtProductName.GotFocus += TxtProductId_GotFocus;
            txtProductUnitprice.GotFocus += TxtProductId_GotFocus;
            txtProductId.Focus();
        }

        private void TxtProductId_GotFocus(object sender, EventArgs e)
        {
            textbox text = sender as textbox;
            text.SelectAll();
        }
        ISuperMarketProductManager productManager = new SuperMarketProductManager();
        //初始化基本信息
        BindingSource categorysource = new BindingSource();
        BindingSource unitsource = new BindingSource();
        List<ProductCategory> categories = null;
        List<ProductUnit> units = null;
        private void InitializeProduct()
        {
            txtMinCount.Text = "0";
            txtMaxCount.Text = "0";
            txtDiscount.Text = "0";
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductUnitprice.Text = "0.00";
            categories = productManager.GetCategories();
            categorysource.DataSource = categories;
            cmbCategory.DataSource = categorysource;
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DisplayMember = "CategoryName";
            if (Program.ClockCategory != null)
            {
                cmbCategory.SelectedIndex = Program.ClockCategory.CategoryId - 1;
                button1.ImageIndex = 0;
                cmbCategory.Enabled = false;
            }
            else
            {
                cmbCategory.SelectedIndex = 0;
                button1.ImageIndex = 1;
                cmbCategory.Enabled = true;
            }

            units = productManager.GetUnit();
            unitsource.DataSource = units;
            cmbUnit.DataSource = unitsource;
            cmbUnit.ValueMember = "Id";
            cmbUnit.DisplayMember = "Unit";
            cmbUnit.SelectedIndex = 0;
        }

        /// <summary>
        /// 锁定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Program.ClockCategory == null)//证明如果是非锁定状态，则应该改变成锁定状态
            {
                var obj = from item in categories where item.CategoryId == Convert.ToInt32(cmbCategory.SelectedValue) select item;
                Program.ClockCategory = obj.FirstOrDefault();
                button1.ImageIndex = 0;
                cmbCategory.Enabled = false;
            }
            else
            {
                Program.ClockCategory = null;
                button1.ImageIndex = 1;
                cmbCategory.Enabled = true;
            }
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtProductId.CheckData("^\\d+$", "商品编号必须是至少6位数字") * txtProductName.CheckNullOrEmpty() * txtProductUnitprice.CheckData(@"^(([1-9]\d*)|(\d*.\d{1,2}))$", "输入金额有误") * txtDiscount.CheckData(@"^((\d)|(\d.\d))$", "折扣输入有误") * txtMinCount.CheckData(@"^\d+$", "最小库存输入有误") * txtMaxCount.CheckData(@"^\d+$", "最大库存输入有误") == 0)
            {
                return;
            }
            else
            {
                //当录入商品的时候要做一些判断：判断商品编号必须是唯一的，其二判断商品的名称必须是唯一的
                List<Produts> productList = productManager.GetAllProduct();
                var obj1 = from objPro in productList
                           where objPro.ProductId == txtProductId.Text
                           select objPro;
                if (obj1.Count() > 0)
                {
                    MessageBox.Show("商品编号已经存在,请重新录入!", "提示");
                    txtProductId.SelectAll();
                    return;
                }
                var obj2 = from objPro in productList
                           where objPro.ProductName == txtProductName.Text
                           select objPro;
                if (obj2.Count() > 0)
                {
                    MessageBox.Show("商品名称已经存在,请重新录入!", "提示");
                    txtProductName.SelectAll();
                    return;
                }
                if (int.Parse(txtMinCount.Text) > int.Parse(txtMaxCount.Text))
                {
                    MessageBox.Show("最大库存量不能小于最小库存量", "提示");
                    return;
                }
                else//添加商品
                {
                    var pu = from item in units where item.Id == Convert.ToInt32(cmbUnit.SelectedValue) select item;
                    Produts produts = new Produts()
                    {
                        ProductId = txtProductId.Text.Trim(),
                        ProductName = txtProductName.Text.Trim(),
                        Discount = Convert.ToSingle(txtDiscount.Text.Trim()),
                        UnitPrice = Convert.ToDecimal(txtProductUnitprice.Text.Trim()),
                        CategoryId = Convert.ToInt32(cmbCategory.SelectedValue),
                        Unit = pu.FirstOrDefault().Unit
                    };

                    ProductInventory inventory = new ProductInventory()
                    {
                        ProductId = txtProductId.Text.Trim(),
                        MinCount = Convert.ToInt32(txtMinCount.Text.Trim()),
                        MaxCount = Convert.ToInt32(txtMaxCount.Text.Trim())
                    };

                    bool res = productManager.InsertProduct(produts, inventory);
                    if (res)
                    {
                        if (MessageBox.Show("添加商品成功，是否继续添加", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            InitializeProduct();
                            txtProductId.Focus();
                            return;
                        }
                        else
                        {
                            if (MessageBox.Show("是否对该商品进行入库？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Tag = produts;
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                            }
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("添加商品失败！", "提示");
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 取消添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
