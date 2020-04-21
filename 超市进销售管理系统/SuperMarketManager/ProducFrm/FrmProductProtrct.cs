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

namespace SuperMarketManager.ProducFrm
{
    public partial class FrmProductProtrct : Form
    {
        //商品封装方法
        ISuperMarketProductManager productManager = new SuperMarketProductManager();
        public FrmProductProtrct()
        {
            InitializeComponent();
            //dataGridView1列表的自动创建
            dgvProductList.AutoGenerateColumns = false;
            // List<ProductCategoryModel> categories = null;商品维护信息
            categories = productManager.GetCategories();
            //商品分类后面的combox为toolCmbType的名字，添加内容
            toolCmbType.Items.Add("所有");
            //把categories（商品分类）的内容全部遍历给ProductCategoryModel item（商品分类）里面
            foreach (ProductCategory item in categories)
            {
                //Combox添加内容
                toolCmbType.Items.Add(item.CategoryName);
            }
            toolCmbType.SelectedIndex = 0;
            list = productManager.GetAllProduct();
            //数据库数据的实现和刷新
            InitializeProduct();
            source.CurrentChanged += Source_CurrentChanged;
            toolTxtWhere.TextChanged += ToolTxtWhere_TextChanged;
            toolTxtWhere.GotFocus += ToolTxtWhere_GotFocus;
            toolTxtWhere.LostFocus += ToolTxtWhere_LostFocus;
        }
        /// <summary>
        /// 查询的文本框名字toolTxtWhere；LostFocus失去焦点时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTxtWhere_LostFocus(object sender, EventArgs e)
        {
            toolTxtWhere.ForeColor = Color.Black;
        }
        /// <summary>
        /// 查询的文本框名字toolTxtWhere；GotFocus获取焦点时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTxtWhere_GotFocus(object sender, EventArgs e)
        {
            toolTxtWhere.SelectAll();
            toolTxtWhere.ForeColor = Color.FromArgb(100, 100, 100);
        }
        /// <summary>
        /// toolTxtWhere文本框中的数据获取，放在CurrentProduct里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTxtWhere_TextChanged(object sender, EventArgs e)
        {
            toolTxtWhere.Tag = "1";
        }
        //商品表属性，属性可以返回值
        public Produts CurrentProduct { get; set; }
        /// <summary>
        /// 封装器在当前绑定项【更改时发生】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Source_CurrentChanged(object sender, EventArgs e)
        {
            CurrentProduct = source.Current as Produts;
        }
        //装商品分类表的数据泛型
        List<ProductCategory> categories = null;
        //装商品表的数据泛型
        List<Produts> list = null;
        //封装器：防止删除修改时直接的报错
        BindingSource source = new BindingSource();
        private void InitializeProduct()
        {
            source.DataSource = list;
            dgvProductList.DataSource = null;
            dgvProductList.DataSource = source;
        }
        /// <summary>
        /// 商品入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnInto_Click(object sender, EventArgs e)
        {
            FrmIntoProduct intoProduct = new FrmIntoProduct();
            intoProduct.ShowDialog();
            InitializeProduct();
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnAdd_Click(object sender, EventArgs e)
        {
            FrmAddProduct addProduct = new FrmAddProduct();
            addProduct.ShowDialog();
            InitializeProduct();
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnUpdate_Click(object sender, EventArgs e)
        {
            if (list.Count == 0 || CurrentProduct == null)
            {
                MessageBox.Show("请选择要修改的商品！");
                return;
            }
            FrmUpdateProduct updateProduct = new FrmUpdateProduct(CurrentProduct);
            if (updateProduct.ShowDialog() == DialogResult.OK)
            {
                list = productManager.GetAllProduct();
                InitializeProduct();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnDelete_Click(object sender, EventArgs e)
        {
            if (list.Count <= 0)
            {
                return;
            }
            dgvProductList.DataSource = null;
            dgvProductList.DataSource = source;
        }
        /// <summary>
        /// 提交查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnQuery_Click(object sender, EventArgs e)
        {
            var selectIndex = from item in categories where item.CategoryName == toolCmbType.SelectedItem.ToString() select item.CategoryId;
            int index = 0;
            string where = toolTxtWhere.Text.Trim();
            //没有选择条件
            if (toolTxtWhere.Tag.ToString() == "0" && selectIndex.FirstOrDefault() == 0)
            {
                list = productManager.GetAllProduct();
                InitializeProduct();
                return;
            }
            else if (toolTxtWhere.Tag.ToString() == "0" && selectIndex.FirstOrDefault() != 0)
            {
                where = "";
                index = selectIndex.FirstOrDefault();
            }
            else if (toolTxtWhere.Tag.ToString() != "0" && selectIndex.FirstOrDefault() == 0)
            {
                where = toolTxtWhere.Text.Trim();
                index = 0;
            }
            else if (toolTxtWhere.Tag.ToString() != "0" && selectIndex.FirstOrDefault() != 0)
            {
                where = toolTxtWhere.Text.Trim();
                index = selectIndex.FirstOrDefault();
            }
            list = productManager.GetProductsWithWhere(index, where);
            InitializeProduct();

            toolTxtWhere.Text = "商品编号、商品名称";
            toolCmbType.SelectedIndex = 0;
            toolTxtWhere.Tag = "0";
        }
        /// <summary>
        /// 更新折扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshDiscount_Click(object sender, EventArgs e)
        {
            if (txtDiscount.CheckData(@"^\d(.\d)?$", "输入有误") != 0)
            {
                if (CurrentProduct != null)
                {
                    if (productManager.SetProductDiscount(CurrentProduct.ProductId, Convert.ToSingle(txtDiscount.Text.Trim())))
                    {
                        CurrentProduct.Discount = Convert.ToSingle(txtDiscount.Text.Trim());
                        InitializeProduct();
                        txtDiscount.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("修改商品折扣失败！", "提示");
                    }
                }
                else
                {
                    MessageBox.Show("请选择要更改折扣的商品！", "提示");
                }
            }
        }
    }
}
