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

namespace SuperMarketManager.AdminFrm
{
    public partial class FrmLogCheck : Form
    {
        ISuperMarketLoginLogManager logManager = new SuperMarketLoginLogManager();
        public FrmLogCheck()
        {
            InitializeComponent();
            //第一个文本框查询时间的焦点
            startTime.Focus();
            //在接收焦点时发生变化
            txtWhere.GotFocus += TxtWhere_GotFocus;
            //焦点失去时发生改变
            txtWhere.LostFocus += TxtWhere_LostFocus;
            //获取数据库中日志的所有数据，把数据库中的所有数据给logList泛型里面装数据库获取的内容
            logList = logManager.GetLoginLog();
            InitializePage();
            dataGridView1.AutoGenerateColumns = false;
        }
        //日志
        List<LoginLogs> logList = null;
        List<LoginLogs> currentPageList = null;
        private void InitializePage()
        {
            if (logList == null)
            {
                MessageBox.Show("暂无任何登录信息！");
                return;
            }
            else
            {
                pageNav.RecordCount = logList.Count;
                pageNav.CurrentPage = 1;
                //每页的数量
                pageNav.PageSize = 15;
                pageNav.SortType = SortType.ASC;
                pageNav.ExceuteSetPageEvent += PageNav_ExceuteSetPageEvent;
                //首次查询的方法（第一页的查询）
                pageNav.FirstSearh();
            }
        }
        //封装的中间站
        BindingSource source = new BindingSource();
        private void PageNav_ExceuteSetPageEvent(int currentPage)
        {
            if (logList != null)
            {
                currentPageList = logList.Skip((pageNav.CurrentPage - 1) * pageNav.PageSize).Take(pageNav.PageSize).ToList();
                source.DataSource = currentPageList;
                dataGridView1.DataSource = source;
                pageNav.SetButtonEnable();
            }
        }

        private void TxtWhere_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWhere.Text))
            {
                txtWhere.Text = "姓名、账号、服务器名";
                txtWhere.Tag = "1";
                txtWhere.ForeColor = Color.FromArgb(100, 100, 100);
            }
        }

        private void TxtWhere_GotFocus(object sender, EventArgs e)
        {
            txtWhere.SelectAll();
            txtWhere.ForeColor = Color.Black;
        }

        /// <summary>
        /// 开始查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            //证明条件框中未输入内容,也不需要按区间查询，查询所有数据
            if (checkBox1.Checked == false && txtWhere.Tag.ToString() == "1")
            {
                logList = logManager.GetLoginLog();
                pageNav.RecordCount = logList.Count;
                pageNav.FirstSearh();
            }
            else
            {
                DateTime start = DateTime.Now;
                DateTime end = DateTime.Now;
                string where = "";
                int check = 0;
                //需要按照查询区间进行查询
                if (checkBox1.Checked == true)
                {
                    check = 1;
                    if (startTime.Value == endTime.Value)//等于
                    {
                        check = 2;
                        start = end = Convert.ToDateTime(startTime.Value.ToShortDateString());
                    }
                    else if (startTime.Value < endTime.Value)//小于
                    {
                        start = Convert.ToDateTime(startTime.Value.ToShortDateString());
                        //'2020-04-14 0:00:00'
                        end = Convert.ToDateTime(endTime.Value.ToShortDateString()).AddDays(1);
                    }
                    else if (startTime.Value > endTime.Value)//大于
                    {
                        check = -1;
                        start = end = Convert.ToDateTime(startTime.Value.ToShortDateString());
                    }
                    if (txtWhere.Tag.ToString() == "1")//不带条件的查询
                    {
                        where = "";
                    }
                    else
                    {
                        where = txtWhere.Text.Trim();
                    }
                }
                else//不按照区间查询但是需要按照条件查询所有的
                {
                    check = 0;
                    where = txtWhere.Text.Trim();
                }
                logList = logManager.GetLoginLogBy(start, end, where, check);
                pageNav.RecordCount = logList.Count;
                pageNav.FirstSearh();
            }
        }
        /// <summary>
        /// 取消查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //在控件上更改Text属性的值时引发的事件
        private void txtWhere_TextChanged(object sender, EventArgs e)
        {
            txtWhere.Tag = "0";
        }
    }
}
