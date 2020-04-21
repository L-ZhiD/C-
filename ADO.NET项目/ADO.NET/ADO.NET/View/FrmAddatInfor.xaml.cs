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
using StudentManagerModel.ObjExt;
using StudentManagerBLL;
using System.Data;

namespace ADO.NET.View
{
    /// <summary>
    /// FrmAddatInfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmAddatInfor : Window
    {
        StudentManagerBLL.StudentManager stumanager = new StudentManager();//处理学生表数据
        AttendaceManager attmanager = new AttendaceManager(); //处理考勤数据
        Attendance attend = new Attendance();
        public FrmAddatInfor()
        {
            InitializeComponent();
        }

        private void btnAffirm_Click(object sender, RoutedEventArgs e)
        {
            //先在学生表中查询是否存在
            int carId = Convert.ToInt32(textCard.Text.Trim()); //获取到的考勤号
            DataTable tab = stumanager.GetStuByCard(carId);
            if (tab.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("输入的考勤号不存在，请重新输入！！！", "提示");
                return;
            }
            attend.CardNo = carId;
            attend.AUpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss "));
            if (attmanager.AddattInfor(attend) == true)
            {
                System.Windows.Forms.MessageBox.Show("打开成功！" + "\r\n" + "时间为:" + DateTime.Now, "提示");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("打卡失败，请稍后再试！！！", "提示");
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
