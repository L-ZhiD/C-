using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentManagerModel;
using StudentManagerModel.ObjExt;
using StudentManagerBLL;
using Common;
using System.IO;


namespace ADO.NET.View
{
    /// <summary>
    /// ScoreInquiry.xaml 的交互逻辑
    /// </summary>
    public partial class ScoreInquiry : UserControl
    {
        StudentManager stuManager = new StudentManager(); //处理学生数据
        StudentClassManager stuclassManager = new StudentClassManager();//处理班级数据
        ScoreManager scoreManager = new ScoreManager();//处理成绩数据
        List<StuCoreExt> score = null; //成绩表数据集合
        StuCoreExt selesco = null; //成绩表实例对象
        public ScoreInquiry()
        {
            InitializeComponent();
            List<StudentClass> stuclass = stuclassManager.GetClasses(); //获取班级表数据
            smclassCmb.ItemsSource = stuclass;
            smclassCmb.DisplayMemberPath = "ClassName";
            smclassCmb.SelectedValuePath = "ClassID";
            smclassCmb.SelectedIndex = 1;
            score = scoreManager.GetScore(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = score;
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectByCId_Click(object sender, RoutedEventArgs e)
        {
            if (smclassCmb.SelectedIndex < 0)
            {
                MessageBox.Show("请选择班级", "提示");
                return;
            }
            score = scoreManager.GetScore(Convert.ToInt32(smclassCmb.SelectedValue));   //获取下拉框选定的班级名
            smDgStudentLsit.ItemsSource = score;//添加班级名对应的成绩表
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog(); //创建工作簿对象
            fileDialog.Filter = "Excel工作簿(*.xlsx;*.xls)|*.xlsx;*.xls"; //检索格式
            fileDialog.FileName = "学生成绩表.xlsx"; //设置工作簿的名称(桌面见到的名称)
            fileDialog.Title = "导出Excel表";
            if (fileDialog.ShowDialog() == true) //如果工作簿的窗体打开
            {
                string path = fileDialog.FileName; //存储的位置
                System.Data.DataTable table = scoreManager.GetScoreByCId((int)smclassCmb.SelectedValue);
                if (table.Rows.Count <= 0)
                {
                    System.Windows.Forms.MessageBox.Show("该班级暂无学生信息！", "提示");
                    return;
                }
                if (Common.ImportOrExportData.ExportToExcel(table, path))
                {
                    MessageBox.Show("导出数据完成！", "提示");
                }
                else
                {
                    MessageBox.Show("导出数据失败，请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 刷新DG中的成绩信息
        /// </summary>
        private void RefreshDG()
        {
            score = scoreManager.GetScore(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = score;
        }
        private void weijige_Click(object sender, RoutedEventArgs e)
        {
            /*list = lists.Fail(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;*/
        }
        //修改成绩
        private void btnDeleteStu_Click(object sender, RoutedEventArgs e)
        {
            /*selesco = smDgScoreLsit.SelectedItem as StuCoreExt;
            try
            {
                if (selesco == null)
                {
                    MessageBox.Show("请选择要修改的学员！", "提示");
                    return;
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("请选定学员！");
                return;
            }
            StuCoreExt objscor = scoreManager.GetStudentById(selesco.StudentID);
            FrmUpdateScoreInfo updateScoreInfo = new FrmUpdateScoreInfo(objscor);
            updateScoreInfo.ShowDialog();
            //刷新DG中学员信息
            RefreshDG();*/
        }
        //姓名排序
        private void btnGroupBySName_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True") //默认为True 即升序
            {
                score.Sort(new StudentNameDESC(true));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/jiang.ico", UriKind.RelativeOrAbsolute));//切换向下图片
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                score.Sort(new StudentNameDESC(false));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/sheng.ico", UriKind.RelativeOrAbsolute));//切换向上图片
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = score;
        }
        //学号排序
        private void btnGroupBySid_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            //倒序：从大到小
            if ((sender as Button).Tag.ToString() == "True")
            {
                score.Sort(new StudentIdDESC(true));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/up.ico", UriKind.RelativeOrAbsolute));//切换向上的图片
                (sender as Button).Tag = "False";
            }
            // 从小到大
            else if ((sender as Button).Tag.ToString() == "False")
            {
                score.Sort(new StudentIdDESC(false)); //Sort()排序方法，默认为升序
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/down.ico", UriKind.RelativeOrAbsolute)); //切换到箭头向下图片
                (sender as Button).Tag = "True"; //
            }
            smDgStudentLsit.ItemsSource = null;//清空表
            smDgStudentLsit.ItemsSource = score; //添加表数据
        }
        /// <summary>
        /// 学号比较器
        /// </summary>
        class StudentIdDESC : IComparer<StuCoreExt> //继承Incompare接口
        {
            public StudentIdDESC(bool b)
            {
                B = b;
            }
            public bool B { get; set; }
            public int Compare(StuCoreExt x, StuCoreExt y) // 返回数字，正数为升序，负数为倒序排列
            {
                if (B) //升序  即返回正数
                {
                    return x.StudentID.CompareTo(y.StudentID);//第一个对象属性与第二个对象的属性比较
                }
                else//倒序  即返回负数
                {
                    return y.StudentID.CompareTo(x.StudentID);//第二个对象的属性和第一个对象的属性比较
                }
            }
        }
        /// <summary>
        /// 姓名比较器
        /// </summary>
        class StudentNameDESC : IComparer<StuCoreExt>
        {
            public StudentNameDESC(bool b) //默认为True
            {
                B = b;
            }
            public bool B { get; set; }
            public int Compare(StuCoreExt x, StuCoreExt y)
            {
                if (B) //默认为升序
                {
                    return y.StudentName.CompareTo(x.StudentName); //第一个对象的属性与第二个对象的属性比较
                }
                else //倒序
                {
                    return x.StudentName.CompareTo(y.StudentName); //第二个对象的属性和第一个对象的属性比较
                }
            }
        }
        //学号或姓名查询（模糊查询）
        private void btnSelectBySIN_Click(object sender, RoutedEventArgs e)
        {
            string target = mstxtIdorName.Text.Trim();//输入的学号或姓名
            List<StuCoreExt> liststu = scoreManager.GetStuScoreIDorName(target);//查询的数据表
            if (liststu.Count < 0)
            {
                MessageBox.Show("未查找到有个信息！", "提示");
                return;
            }
            score = liststu;
            smDgStudentLsit.ItemsSource = null; //清空表格
            smDgStudentLsit.ItemsSource = score;//添加信息
        }
    }
}
