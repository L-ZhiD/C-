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
using Common;
using System.IO;
using StudentManagerBLL;
using StudentManagerModel;
using StudentManagerModel.ObjExt;

namespace ADO.NET.View
{
    /// <summary>
    /// ADDStudent.xaml 的交互逻辑
    /// </summary>
    public partial class ADDStudent : Window
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager manager = new StudentManagerBLL.StudentManager();
        common.BitmapImg image = null;
        //本地上传
        common.BitmapImg img = new common.BitmapImg();
        public StudentExt student1 { get; set; }
        StudentExt stu = new StudentExt();
        public ADDStudent()
        {
            InitializeComponent();
            List<StudentClass> classes = csm.GetClasses();
            cmbClassName.ItemsSource = classes;
            cmbClassName.DisplayMemberPath = "ClassName";
            cmbClassName.SelectedValuePath = "ClassID";
            cmbClassName.SelectedIndex = stu.ClssID - 1;

        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();

            }
        }

        private void txtAge_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                System.Windows.MessageBox.Show("年龄不能为空！");
                txtAge.Focus();
                return;
            }
            return;
        }

        private void txtCardNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
                return;
            }
            return;
        }

        private void txtStuNoId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();

            }
        }

        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return;
            }

            else if (Common.DataValidate.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("必须为11位正整数！");
                txtStuNoId.Focus();
                return;
            }    
        }
        //重新选照片
        private void btnUploadPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png,*.bmp";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                stuImg.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                stuImg.Stretch = Stretch.UniformToFill;
                img.Buffer = File.ReadAllBytes(path);
            }
        }
        public static string imgPath;
        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            FrmStudentPhoto photo = new FrmStudentPhoto();
            photo.ShowDialog();
            if (!string.IsNullOrEmpty(imgPath))
            {
                //照片刷新了之后对新照片进行序列化
                stuImg.Source = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
                img.Buffer = File.ReadAllBytes(imgPath);
            }
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        //确认添加
        private void btnSureUpdate_Click(object sender, RoutedEventArgs e)
        {
            StudentClassManager csm = new StudentClassManager();
            List<StudentClass> classes = csm.GetClasses();
            //改变数据之前的最终验证
            if (CheckInfor())
            {
                student1.StudentName = txtName.Text;
                student1.Age = Convert.ToInt32(txtAge.Text);
                student1.Birthday = datePkBirthday.DisplayDate;
                student1.CardNo = txtCardNo.Text;
                student1.ClssID = (int)cmbClassName.SelectedValue;
                student1.StudentSex = (radBoy.IsChecked == true ? "男" : "女");
                student1.PhoneNumber = txtPhoneNumber.Text;
                student1.StudentAddress = (string.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text);
                student1.StudentIdNo = txtStuNoId.Text;
                //判断是否重新选择了Image
                if (stuImg.Source == new BitmapImage(new Uri("/img/zwzp.jpg", UriKind.RelativeOrAbsolute)))
                {
                    student1.StuImage = null;
                }
                //判断数据库中的图片是否和目前的上传图片一样
                else
                {
                    //证明未修改图片,目前的图片和原来数据库中的一致
                    if (image != null && img.Buffer == image.Buffer)
                    {
                        student1.StuImage = Common.SerializeObjectTostring.SerializeObject(image);
                    }
                    else
                    {
                        student1.StuImage = Common.SerializeObjectTostring.SerializeObject(img);
                    }

                }
                if (manager.AddStudent(student1))
                {
                    System.Windows.MessageBox.Show("添加成功！", "提示");
                    Environment.Exit(0);
                }
                else
                {
                    System.Windows.MessageBox.Show("添加失败，请稍后再试！", "提示");
                }
            }
        }

        bool CheckInfor()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                System.Windows.MessageBox.Show("年龄不能为空！");
                txtAge.Focus();
                return false;
            }
            else if (!DataValidate.IsInteger(txtAge.Text))
            {
                System.Windows.MessageBox.Show("年龄必须是纯数字！");
                txtAge.Focus();
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

            //if (Common.DataValidate.IsIdentitycard(txtStuNoId.Text))
            //{
            //    System.Windows.MessageBox.Show("必须为18位正整数！");
            //    txtStuNoId.Focus();
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return false;
            }

            if (Common.DataValidate.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("必须为11位数！");
                txtStuNoId.Focus();
                return false;
            }
            return true;
        }
    }
}
