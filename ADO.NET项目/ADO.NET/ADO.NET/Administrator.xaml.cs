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
using System.Windows.Shapes;
using System.Windows.Threading;//获取当前系统的时间
using System.Configuration;

namespace ADO.NET
{
    /// <summary>
    /// Administrator.xaml 的交互逻辑
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator()
        {
            InitializeComponent();
            MainWindow login = new MainWindow();//登录窗口的验证,登录成功就关闭登陆窗口
            login.ShowDialog();//展示主窗口
            if (login.DialogResult != true)
            {
                Environment.Exit(0);
            }
            //计时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            try
            {
                //获取登录的管理员的名称
                statusAdminLb.Content = "操作管理员【" + App.CurrentAdmin.AdminName + "】";
                //获取版本号(版本号这个应在App.config配置文件中写方便更改)
                statusVersionLb.Content = "版本号：" + ConfigurationManager.AppSettings["version"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //获取系统的年月日
            DateTime now = DateTime.Now;
            string week = "星期天";
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "星期天";
                    break;
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                default:
                    break;
            }
            //将获取的值传递到装有日期时间的Lable上
            statusTimeLb.Content = string.Format("{0}年{1}月{2}日    {3}:{4}:{5}    {6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, week);
        }
        //计时器声明
        DispatcherTimer timer = null;
        //关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 访问官网
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inlinehMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ConfigurationManager.AppSettings["webadd"].ToString());
        }
        /// <summary>
        /// 在线回答问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lianxiMenu_Click(object sender, RoutedEventArgs e)
        {
            //弹框：请拨打电话：89564386
            //弹框：请联系QQ：xxxxxxx
            //System.Diagnostics.Process.Start("TeamViewer14.exe");
        }
        /// <summary>
        /// 学生信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smMenu_Click(object sender, RoutedEventArgs e)
        {
            //这个点击事情起到打开接下来的页面
            Gird_Content.Children.Clear();//移除这个页面的东西
            View.FrmStuManager frmStudent = new View.FrmStuManager();//把获取的页面实例化
            Gird_Content.Children.Add(frmStudent);
            //把获取到的窗口添加到这个页面上
        }
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            View.ADDStudent student=new View.ADDStudent();
            Gird_Content.Children.Add(student);
        }
        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Switchover_Click(object sender, RoutedEventArgs e)
        {
            statusAdminLb.Content = "";//登录者清空
            statusVersionLb.Content = "";//版本清空
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void ScoreInquiry_Click(object sender, RoutedEventArgs e)
        {
            //这个点击事情起到打开接下来的页面
            Gird_Content.Children.Clear();//移除这个页面的东西
            View.ScoreInquiry score = new View.ScoreInquiry();//把获取的页面实例化
            Gird_Content.Children.Add(score);
            //把获取到的窗口添加到这个页面上
        }
        //考勤打卡
        private void queraMenu_Click(object sender, RoutedEventArgs e)
        {
            View.FrmAddatInfor att = new View.FrmAddatInfor();
            att.ShowDialog();
        }
    }
}
