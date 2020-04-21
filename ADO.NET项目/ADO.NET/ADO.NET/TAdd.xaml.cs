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
using StudentManagerModel;
using StudentManagerBLL;

namespace ADO.NET
{
    /// <summary>
    /// TAdd.xaml 的交互逻辑
    /// </summary>
    public partial class TAdd : Window
    {
        public TAdd()
        {
            InitializeComponent();
        }

        AdminManager Manager = new AdminManager();
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogPwd.Password == "" && txtLogId.Text == "")
            {
                nameZC.Content = "注册名：";
                pwdZC.Content = "注册密码";
                MessageBox.Show("转成注册界面");
                return;
            }
            Admins admins = new Admins();
            admins.LoginPwd = txtLogPwd.Password;
            admins.AdminName = txtLogId.Text;
            Manager.AddAdmins(admins);
            MessageBox.Show("注册成功");
            nameZC.Content = "登录账号：";
            pwdZC.Content = "登录密码：";
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}
