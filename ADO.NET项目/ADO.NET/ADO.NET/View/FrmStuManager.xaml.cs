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
using StudentManagerModel;//实体层
using StudentManagerBLL;//业务逻辑层
using StudentManagerModel.ObjExt;
using ADO.NET.View;
using Common;
using System.Data;
using System.IO;

namespace ADO.NET.View
{
    /// <summary>
    /// FrmStuManager.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStuManager : UserControl
    {
        //Manager为业务逻辑层(BLL)
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager sm = new StudentManagerBLL.StudentManager();
        //实现List
        List<StudentExt> students = null;
        //用来记录当前的选择的学员
        StudentExt selectStu = null;//这个声明后你就没用到
        public FrmStuManager()
        {
            InitializeComponent();
            //获取下拉框的内容
            List<StudentClass> classes = csm.GetClasses();
            //显示获取的内容
            smclassCmb.ItemsSource = classes;
            smclassCmb.DisplayMemberPath = "ClassName";//设置下拉框的显示文本
            smclassCmb.SelectedValuePath = "ClassId";//设置下拉框显示文本对应的value
            //获取显示索引为0的内容(就是让内容显示出来,空荡荡看着难受)
            smclassCmb.SelectedIndex = 0;
            //给DataGrid进行数据绑定,需要针对DG中列进行绑定对应的数据列
            RefreshDG();
        }

        private void RefreshDG()
        {
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectByCId_Click(object sender, RoutedEventArgs e)
        {
            if (smclassCmb.SelectedIndex<0)//如果这个选项小于0表示没有选中内容
            {
                MessageBox.Show("请选择班级!", "提示");
                return;
            }
            //把选中的值GetStudentsExts传进去判断
            students = sm.GetStudents(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;//把smDgStudentLsit里面的值显示给DataGrid这里面的网格
        }
        /// <summary>
        /// 学号排列按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySid_Click(object sender, RoutedEventArgs e)
        {
            //声明比较顺序
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            //倒序：从小到大
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new StudentIdDESC(true));
                //groupBySidImg一个图片
                groupBySidImg.Source = new BitmapImage(new Uri("/img/up.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new StudentIdDESC(false));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/down.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }
        /// <summary>
        /// 姓名排列按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySName_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new StudentNameDESC(true));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/jiang.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new StudentNameDESC(false));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/sheng.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }
        //隐藏
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 根据输入的学号或者姓名查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectBySIN_Click(object sender, RoutedEventArgs e)
        {
            smclassCmb.SelectedIndex = -1;
            string target = mstxtIdorName.Text.Trim();//获取查询的内容
            List<StudentExt> liststu = sm.GetStudentsByIdOrName(target);
            smDgStudentLsit.ItemsSource = null;
            if (liststu.Count<=0)
            {
                MessageBox.Show("根据条件没查询到相关信息");
                mstxtIdorName.Focus();
                mstxtIdorName.SelectAll();
                return;
            }
            students = liststu;//获取内容
            smDgStudentLsit.ItemsSource = students;
                    
        }
        //比较数字的接口
        //声明比较器
        class StudentIdDESC : IComparer<StudentExt>
        {
            public StudentIdDESC(bool b)
            {
                B = b;
            }
            public bool B { get; set; }
            public int Compare(StudentExt x, StudentExt y)
            {
                //ID顺序
                if (B)
                {
                    return x.StudentID.CompareTo(y.StudentID);
                }
                else
                {
                    return y.StudentID.CompareTo(x.StudentID);
                }
            }
        }
        //比较文字的接口
        class StudentNameDESC : IComparer<StudentExt>
        {
            ///-1，0，1
            public StudentNameDESC(bool b)
            {
                B = b;
            }
            public bool B { get; set; }
            //Name顺序排序
            public int Compare(StudentExt x, StudentExt y)
            {
                if (B)
                {
                    return y.StudentName.CompareTo(x.StudentName);
                }
                else
                {
                    return x.StudentName.CompareTo(y.StudentName);
                }
            }
        }
        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XiuGai_Click(object sender, RoutedEventArgs e)
        {
            StudentExt selectStu = smDgStudentLsit.SelectedItem as StudentExt;
            //检测当前选择的学员，查看详细信息的界面还未关闭
            if (IdList.Contains(selectStu.StudentID))
            {
                MessageBox.Show("请关闭正在查看的学员信息界面", "提示");
                return;
            }
            if (selectStu == null)
            {
                MessageBox.Show("请选择要修改的学员!","提示");
                return;
            }
            StudentExt objStu = sm.GetStudentById(selectStu.StudentID);
            FrmUpdateStuInfor updateStuInfor = new FrmUpdateStuInfor(objStu);
            updateStuInfor.ShowDialog();//打开修改界面
            //刷新DG中这个学员的信息
            RefreshDG();
        }
        List<int> IdList = new List<int>();
        List<FrmStudentInfor> frmList = new List<FrmStudentInfor>();
        /// <summary>
        /// 当鼠标双击某个学员查看这个学员的详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smDgStudentLsit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //这个地方你之前重复引用了

            selectStu = smDgStudentLsit.SelectedItem as StudentExt;
            if (selectStu == null)
            {
                return;
            }
            ///当这个学员的完整信息已经存在的话，证明已经打开了一个窗口
            ///除非是打开新的学员窗口，否则只能把之前的窗口给呈现出来
            if (IdList.Contains(selectStu.StudentID))
            {
                foreach (FrmStudentInfor item in frmList)
                {
                    if (item.StuId == selectStu.StudentID)
                    {
                        //激活窗口，
                        item.Activate();
                    }
                }
            }
            else
            {
                StudentExt objStu = sm.GetStudentById(selectStu.StudentID);
                IdList.Add(selectStu.StudentID);
                View.FrmStudentInfor studentInfor = new FrmStudentInfor(objStu);
                studentInfor.Closing += StudentInfor_Closing;
                frmList.Add(studentInfor);
                //展示窗口
                studentInfor.Show();
            }
        }
        /// <summary>
        /// 移除关闭的窗口对应的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StudentInfor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int stuId = (sender as FrmStudentInfor).StuId;//记录是谁的信息
            IdList.Remove(stuId);
            foreach (FrmStudentInfor item in frmList)
            {
                if (item.StuId == stuId)
                {
                    frmList.Remove(item);
                    return;
                }
            }
        }
        /// <summary>
        /// 删除学员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as StudentExt;
            if (IdList.Contains(selectStu.StudentID))
            {
                MessageBox.Show("请关闭正在查看的学员信息界面", "提示");
                return;
            }
            if (selectStu == null)
            {
                MessageBox.Show("请选择要删除的学员！", "提示");
                return;
            }
            StudentExt student = sm.GetStudentById(selectStu.StudentID);
            if (student != null)
            {
                MessageBox.Show("您选择的学员信息已经被删除！", "提示");
                return;
            }
            //删除工作很危险
            MessageBoxResult mbr = MessageBox.Show("您确定要删除【" + student.StudentName + "】", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.OK)
            {
                if (sm.DeleteStudentById(student.StudentID))
                {
                    MessageBox.Show("删除成功！", "提示");
                }
                else
                {
                    MessageBox.Show("删除失败请稍后再试！", "提示");
                }
            }

        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.Filter = "Excel工作簿(*.xlsx;*.xls)|*.xlsx;*.xls";
            fileDialog.FileName = "学生信息表.xlsx";
            fileDialog.Title = "导出到Excel表";
            if (fileDialog.ShowDialog()==true)
            {
                string path = fileDialog.FileName;
                System.Data.DataTable table = sm.GetDataTable((int)smclassCmb.SelectedValue);
                if (table.Rows.Count<=0)
                {
                    MessageBox.Show("该班级暂无学生数据！", "提示");
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
        /// 实现打印及打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as StudentExt;
            if (selectStu == null)
            {
                MessageBox.Show("请选择您要打印的学员", "提示");
                return;
            }
            common.BitmapImg image = null;
            if (string.IsNullOrEmpty(selectStu.StuImage))
            {
                selectStu.ImgPath = "/img/bg/zwzp.jpg";
            }
            else
            {
                image = SerializeObjectTostring.DeserializeObject(selectStu.StuImage) as common.BitmapImg;//将学生照片序列化为字符串格式
                BitmapImage bitmap = new BitmapImage();//优化图片
                bitmap.BeginInit();//开始
                bitmap.StreamSource = new MemoryStream(image.Buffer);
                bitmap.EndInit();//结束
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                long sc = DateTime.Now.Ticks;//显示日期
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    byte[] buffer = stream.ToArray();
                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "/printImg/" + sc + ".png", buffer);
                    stream.Close();
                }
                selectStu.ImgPath = AppDomain.CurrentDomain.BaseDirectory + "/printImg/" + sc + ".png";
            }
            View.FrmPrintWindow frmPrint = new FrmPrintWindow("PrintModel.xaml", selectStu);
            frmPrint.ShowInTaskbar = false;
            frmPrint.ShowDialog();
        }
    }
}
