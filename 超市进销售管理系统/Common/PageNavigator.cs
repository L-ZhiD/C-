using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public enum SortType
    {
        DESC,
        ASC
    }

    public delegate void PagerQueryDelegate(int currentPage);
    public partial class PageNavigator : UserControl
    {
        public event PagerQueryDelegate ExceuteSetPageEvent;
        public PageNavigator()
        {
            InitializeComponent();
            FristBtn.Enabled = false;
            LastBtn.Enabled = false;
            NextBtn.Enabled = false;
            ReturnBtn.Enabled = false;
            UpBtn.Enabled = false;
        }
        /// <summary>
        /// 查询分页的存储过程名称
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 顺序查询或倒序查询
        /// </summary>
        public SortType SortType { get; set; } = SortType.ASC;

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 每页的数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分页的表名
        /// </summary>
        public string TableName { get; set; }

        public int PageCount
        {
            get
            {
                if (RecordCount % PageSize == 0)
                {
                    return RecordCount / PageSize;
                }
                else
                {
                    return (RecordCount / PageSize) + 1;
                }
            }
        }

        void ShowInfo()
        {
            PageIndex.Text = CurrentPage.ToString();
            lblPageCount.Text = PageCount.ToString();
        }

        /// <summary>
        /// 设置按钮的状态
        /// </summary>
        public void SetButtonEnable()
        {
            ShowInfo();
            if (PageCount <= 1)
            {
                FristBtn.Enabled = false;
                LastBtn.Enabled = false;
                NextBtn.Enabled = false;
                ReturnBtn.Enabled = false;
                UpBtn.Enabled = false;
                return;
            }
            FristBtn.Enabled = true;
            LastBtn.Enabled = true;
            NextBtn.Enabled = true;
            ReturnBtn.Enabled = true;
            UpBtn.Enabled = true;
            if (CurrentPage == 1)
            {
                FristBtn.Enabled = false;
                UpBtn.Enabled = false;
            }
            else if (CurrentPage == PageCount)
            {
                LastBtn.Enabled = false;
                NextBtn.Enabled = false;
            }
        }
        //首次查询的方法
        public void FirstSearh()
        {
            CurrentPage = 1;
            ExceuteSetPageEvent(CurrentPage);
        }
        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FristBtn_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            ExceuteSetPageEvent(CurrentPage);
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpBtn_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            ExceuteSetPageEvent(CurrentPage);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtn_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            ExceuteSetPageEvent(CurrentPage);
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastBtn_Click(object sender, EventArgs e)
        {
            CurrentPage = PageCount;
            ExceuteSetPageEvent(CurrentPage);
        }
        /// <summary>
        /// 转跳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            string input = txtNum.Text.Trim();
            if (input.Length >= 1)
            {
                if (txtNum.CheckData(@"^\d*$", "请输入有效数字") == 0)
                {
                    MessageBox.Show("请输入有效数字！", "提示");
                    txtNum.Text = "";
                    return;
                }
                else
                {
                    int pageIndex = Convert.ToInt32(input);
                    if (pageIndex > PageCount)
                    {
                        MessageBox.Show($"最大页数不能超出{PageCount}", "提示");
                        return;
                    }
                    else
                    {
                        CurrentPage = pageIndex;
                        ExceuteSetPageEvent(CurrentPage);
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
