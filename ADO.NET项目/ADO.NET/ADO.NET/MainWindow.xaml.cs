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
using Common;//1.在引用中添加 2.在命名空间中导入 3.导入时suing+类库的名称
using StudentManagerBLL;
using System.Data.SqlClient;
using StudentManagerModel;


namespace ADO.NET
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLogId.Focus();
        }
        //退出
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        //窗口最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //登录
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //数据验证
            if (txtLogId.Text.Trim().Length == 0)//对账号的验证
            {
                MessageBox.Show("请输入登录账号！", "登录提示");
                txtLogId.Focus();
                return;
            }//使用正则表达式进行判断
            if (DataValidate.IsInteger(txtLogId.Text.Trim()) == false)//调用这个DataValidate(类)的Isinteger这个方法表示验证某个文本框是否为正整数
            {
                MessageBox.Show("请输入正确账号！(纯数字格式)", "登录提示");
                txtLogId.Focus();
                return;
            }//密码的验证
            if (txtLogPwd.Password.Length == 0)//在这个文本框中如果输入的账号长度0，则这个文本框为空
            {
                MessageBox.Show("请输入登录密码！", "登录提示");
                txtLogPwd.Focus();
                return;
            }
            #region//第一种登录方式
            //输入的账号密码
            //在Model中获取Admins
            Admins admin = new Admins()
            {
                //1.判断LoginId中是否是数字2.验证Common里面的DataValidate 
                LoginId = Convert.ToInt32(txtLogId.Text.Trim()),
                LoginPwd = txtLogPwd.Password
                //获取账号密码后和Admins中属性做比较 对应
            };
            //和后台交互查询，判断登录信息是否正确
            //导入BLL(业务逻辑层)
            try
            {
                //拿到数据查看是否正确,然后传给MainWindow从数据库中获取值和拿到的数据作比较
                Admins mainuse = new AdminManager().GetAdmins(admin);
                if (mainuse == null)//内容不能为空
                {
                    MessageBox.Show("用户账号不存在！", "提示信息");
                    txtLogId.Focus();
                }
                else
                {
                    if (mainuse.LoginPwd == txtLogPwd.Password)//输入的密码和数据库的密码作比较
                    {
                        //保存登录信息
                        App.CurrentAdmin = mainuse;
                        this.DialogResult = true;//为true时关闭这个窗口
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户密码错误！", "提示信息");
                        txtLogPwd.Focus();
                    }
                }
            }
            catch (Exception  )
            {
                MessageBox.Show("服务器连接异常，登录失败！请检查您的网络！");
            }
            #endregion
        }

        private void zhuce_Click(object sender, RoutedEventArgs e)
        {
            TAdd add = new TAdd();
            add.Show();
            //this.Hide();
        }
    }
}
