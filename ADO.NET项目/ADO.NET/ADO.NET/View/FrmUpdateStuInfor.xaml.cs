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
using Common;
using System.IO;
using WPFMediaKit.DirectShow.Controls;

namespace ADO.NET.View
{
    /// <summary>
    /// FrmUpdateStuInfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmUpdateStuInfor : Window
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager manager = new StudentManagerBLL.StudentManager();
        common.BitmapImg imge = null;//给这个BitmapImg先一个空值
        common.BitmapImg img = new common.BitmapImg();//从上传照片
        public StudentExt student { get; set; }
        public FrmUpdateStuInfor(StudentExt stu)
        {
            InitializeComponent();
            this.Title = "修改【" + stu.StudentName + "】信息";
            student = stu;
            txtAddress.Text = stu.StudentAddress;
            txtAge.Content = stu.Age.ToString();
            txtCardNo.Text = stu.CardNo;
            txtName.Text = stu.StudentName;
            txtPhoneNumber.Text = stu.PhoneNumber;
            txtStuNoId.Text = stu.StudentIdNo;
            if (stu.StudentSex=="男")//性别男
            {
                radBoy.IsChecked = true;
            }
            else//性别女
            {
                radGirl.IsChecked = true;
            }
            datePkBirthday.Content = stu.Birthday.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(stu.StuImage))
            {
                stuImg.Source = new BitmapImage(new Uri("/img/zwzp.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                //如果学员的Iamge字段中能够查询到数据，那么就可以直接将这个数据反序列化成BitmapImage对象
                imge = SerializeObjectTostring.DeserializeObject(stu.StuImage) as common.BitmapImg;//照片字符串格式
                img.Buffer = imge.Buffer;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();//开始
                bitmap.StreamSource = new MemoryStream(imge.Buffer);//获取流
                bitmap.EndInit();//结束
                stuImg.Source = bitmap;//获取照片
            }
            List<StudentClass> classes = csm.GetClasses();
            cmbClassName.ItemsSource = classes;
            cmbClassName.DisplayMemberPath = "ClassName";
            cmbClassName.SelectedValuePath = "ClassId";
            cmbClassName.SelectedIndex = stu.ClssID ;
        }
        //取消修改
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //确认修改
        private void btnSureUpdate_Click(object sender, RoutedEventArgs e)
        {
            //改变数据之前的最终验证
            if (CheckInfor())
            {
                student.StudentName = txtName.Text;
                student.Age = Convert.ToInt32(txtAge.Content);
                student.Birthday = (DateTime)datePkBirthday.Content;
                student.CardNo = txtCardNo.Text;
                student.ClssID = (int)cmbClassName.SelectedValue;
                student.StudentSex = (radBoy.IsChecked == true ? "男" : "女");
                student.PhoneNumber = txtPhoneNumber.Text;
                student.StudentAddress = (string.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text);
                student.StudentIdNo = txtStuNoId.Text;
                //判断是否重新选择了Image
                if (stuImg.Source == new BitmapImage(new Uri("/img/zwzp.jpg", UriKind.RelativeOrAbsolute)))
                {
                    student.StuImage = null;
                }
                //判断数据库中的图片是否和目前的上传图片一样
                else
                {
                    //证明未修改图片,目前的图片和原来数据库中的一致
                    if (imge != null && img.Buffer == imge.Buffer)
                    {
                        student.StuImage = Common.SerializeObjectTostring.SerializeObject(imge);
                    }
                    else
                    {
                        student.StuImage = Common.SerializeObjectTostring.SerializeObject(img);
                    }

                }
                if (manager.UpdateStudentInfor(student))
                {
                    System.Windows.MessageBox.Show("修改成功！", "提示");
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("修改失败，请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 姓名约束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
            }
        }
        /// <summary>
        /// 所有输入的不能为空
        /// </summary>
        /// <returns></returns>
        bool CheckInfor()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
                return false;
            }
            else if (Common.DataValidate.IsSCard(txtStuNoId.Text.Trim()))
            {
                System.Windows.MessageBox.Show("身份证号非法！");
                txtStuNoId.Focus();
            }
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 打卡号的约束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
            }
        }
        /// <summary>
        /// 身份证号的约束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStuNoId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
            }
            else if (Common.DataValidate.IsSCard(txtStuNoId.Text.Trim()))
            {
                System.Windows.MessageBox.Show("身份证号非法！");
                txtStuNoId.Focus();
            }
            else
            {
                datePkBirthday.Content = Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()).ToString("yyyy-MM-dd");

                txtAge.Content = Common.GetValueById.GetAge(Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()));
            }
        }
        /// <summary>
        /// 联系方式的约束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
            }
        }
        
        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
            }
        }
        /// <summary>
        /// 照片格式的约束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png,*.bmp";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                stuImg.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                stuImg.Stretch = Stretch.UniformToFill;// 自己调试合适大小
                //用在通用层中创建一个属性类型为BitmapImg图片路径类型；在这里将其实例化
                img.Buffer = File.ReadAllBytes(path);//Buffer记录图片路径
            }
        }
        //保存拍照的图片时保存的路径
        public static string imgPath;
        /// <summary>
        /// 打开摄像头拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenVideo_Click_1(object sender, RoutedEventArgs e)
        {
            //要在这个地方获取一个窗口用来拍照
            FrmStudentPhoto photo = new FrmStudentPhoto();
            //展开这个窗口
            photo.ShowDialog();
            if (!string.IsNullOrEmpty(imgPath))//指定空字符串
            {
                //照片刷新了之后对新照片进行序列化;表示把路径放在数据库中；反序列化时从数据库中拿出图片路径
                stuImg.Source = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
                //把这个图片路径给img1.Buffer(这样表示放在图片路径里面了)
                img.Buffer = File.ReadAllBytes(imgPath);
            }
        }
    }
}
