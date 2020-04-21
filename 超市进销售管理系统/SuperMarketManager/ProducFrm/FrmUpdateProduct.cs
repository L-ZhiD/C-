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
using Common;

namespace SuperMarketManager.ProducFrm
{
    public partial class FrmUpdateProduct : Form
    {
        ISuperMarketProductManager productManager = new SuperMarketProductManager();
        List<ProductCategory> categories = null;
        BindingSource source1 = new BindingSource();
        BindingSource source2 = new BindingSource();
        List<ProductUnit> units = null;
        //商品表的属性
        public Produts CurrentProduct { get; set; }
        public FrmUpdateProduct(Produts produts)
        {
            InitializeComponent();
            //商品名字获取焦点
            txtProductName.Focus();
            //获取并给 categories泛型中添加内容
            categories = productManager.GetCategories();
            //获取并给 units泛型中添加内容
            units = productManager.GetUnit();
            //【商品类型】和【商品计量单位】不能为空
            if (categories.Count == 0 || units.Count == 0)
            {
                return;
            }
            source1.DataSource = categories;
            cmbCategory.DataSource = source1;
            //通过 CategoryId的key名字来记录内容
            cmbCategory.ValueMember = "CategoryId";
            //Combox的值的显示
            cmbCategory.DisplayMember = "CategoryName";
            //获取或设置指定当前选定的索引项
            cmbCategory.SelectedIndex = produts.CategoryId - 1;

            source2.DataSource = units;
            cmbUnit.DataSource = source2;
            //Combox中对应位置放入对应的所有数据
            cmbUnit.ValueMember = "Id";
            cmbUnit.DisplayMember = "Unit";
            //通过linq查询返回第一个Id值
            cmbUnit.SelectedIndex = ((from item in units where item.Unit == produts.Unit select item.Id).FirstOrDefault() - 1);
            //添加ID数据
            txtProductId.Text = produts.ProductId;
            //天津四商品名字
            txtProductName.Text = produts.ProductName;
            //折扣的修改
            txtUnitPrice.Text = produts.UnitPrice.ToString("F2");
            //CurrentProduct商品属性记录
            CurrentProduct = produts;
            //把获取焦点是触发数据
            txtProductId.GotFocus += TxtProductId_GotFocus;
            txtProductName.GotFocus += TxtProductId_GotFocus;
            txtUnitPrice.GotFocus += TxtProductId_GotFocus;
        }
        /// <summary>
        /// 获取焦点时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtProductId_GotFocus(object sender, EventArgs e)
        {
            textbox text = sender as textbox;
            text.SelectAll();
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtProductId.CheckData(@"^\d{6,}$", "商品编格式错误") * txtProductName.CheckNullOrEmpty() * txtUnitPrice.CheckData(@"^\d*(.\d\d?)+$", "单价格式错误！") != 0)
            {
                CurrentProduct.ProductId = txtProductId.Text.Trim();
                CurrentProduct.ProductName = txtProductName.Text.Trim();
                CurrentProduct.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                CurrentProduct.CategoryId = (from item in categories where item.CategoryId == Convert.ToInt32(cmbCategory.SelectedValue) select item.CategoryId).FirstOrDefault();
                CurrentProduct.Unit = (from uitem in units where uitem.Id == cmbUnit.SelectedIndex + 1 select uitem.Unit).FirstOrDefault();
                if (productManager.UpdateProduct(CurrentProduct))
                {
                    MessageBox.Show("修改商品成功！");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改商品失败！");
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
