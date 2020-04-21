using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentManagerModel.ObjExt;
using Common;
using System.IO;

namespace ADO.NET.View
{
    /// <summary>
    /// FrmStudentInfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStudentInfor : Window
    {
        public FrmStudentInfor(StudentExt stu)
        {
            //获取到所执行学员的信息
            //还有估计这里面你那个也有错 然后问题就不大了
            InitializeComponent();
            StuId = stu.StudentID;
            this.Title = stu.StudentName + "-信息";
            lblAddress.Content = stu.StudentAddress;
            lblAge.Content = stu.Age;
            lblBirthday.Content = stu.Birthday.ToString("yyyy-MM-dd");
            lblCardNo.Content = stu.CardNo;
            lblClassName.Content = stu.ClassName;
            lblStudentSex.Content = stu.StudentSex;
            lblName.Content = stu.StudentName;
            lblPhoneNumber.Content = stu.PhoneNumber;
            lblStuId.Content = stu.StudentID;
            lblStuNoId.Content = stu.StudentIdNo;
            if (string.IsNullOrEmpty(stu.StuImage))
            {
                stuImg.Source = new BitmapImage(new Uri("/img/zwzp.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                //如果学员的Iamge字段中能够查询到数据，那么就可以直接将这个数据反序列化成BitmapImage对象
                common.BitmapImg image = SerializeObjectTostring.DeserializeObject(stu.StuImage) as common.BitmapImg;//把照片变成字符串
                BitmapImage bitmap = new BitmapImage();//优化照片
                bitmap.BeginInit();//开始初始化
                bitmap.StreamSource = new MemoryStream(image.Buffer);//找到流并储存
                bitmap.EndInit();//结束初始化
                stuImg.Source = bitmap;
            }
        }
      //  common.BitmapImg image = new common.BitmapImg();

        /// <summary>
        /// 这个属性用它来记录当前窗口中绑定的学员信息是谁的
        /// </summary>
        public int StuId { get; set; }
    }
}
