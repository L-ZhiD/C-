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
using System.IO;
using WPFMediaKit.DirectShow.Controls;//摄像头控件
namespace ADO.NET.View
{
    /// <summary>
    /// FrmStudentPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStudentPhoto : Window
    {
        public FrmStudentPhoto()
        {
            InitializeComponent();
        }
        //照片纸
        RenderTargetBitmap bmp = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //检测系统连接的摄像头
            //判断大于0表示系统至少有1个以上摄像头可以用
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                //当前的拍照设备采用默认摄像头
                picture.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("您的设备暂无摄像设备链接！", "提示");
                this.Close();
            }
        }
        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClickPhoto_Click(object sender, RoutedEventArgs e)
        {//照片纸
            bmp = new RenderTargetBitmap((int)picture.Width, (int)picture.Height, 96, 96, PixelFormats.Default);
            //将摄像头捕获的区域显示到照片纸上
            bmp.Render(picture);
            //图片预览放在一个image(pictrueYulan名字)标签的路径里面；就是把RTB获取到的图片放在pictrueYulan里面
            pictrueYulan.Source = bmp;
            //预览图显示,摄像头隐藏
            pictrueYulan.Visibility = Visibility.Visible;//预览图显示
            pictrueYulan.Visibility = Visibility.Hidden;//摄像头隐藏
        }
        /// <summary>
        /// 重拍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //重新拍照先把摄像头显示，把预览关闭
            picture.Visibility = Visibility.Visible;
            picture.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePic_Click(object sender, RoutedEventArgs e)
        {
            //如果照片纸为NULL则是未拍照
            if (bmp==null)
            {
                MessageBox.Show("请重新拍照！", "提示");
                //显示摄像头(picture)；隐藏预览区(pictrueYulan):就是把一张图片显示在后面，如果确定里面传递进去照片，则显示在前面
                picture.Visibility = Visibility.Visible;
                pictrueYulan.Visibility = Visibility.Hidden;
                return;
            }
            //选择路径保存照片
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            //这是文件可以满足的文件条件
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp";
            fileDialog.Title = "保存照片";
            //文件默认的名字
            fileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            if (fileDialog.ShowDialog() == true)
            {
                //将刚才的照片以流的方式进行保存
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));//把照片纸放在encoder这个里面
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);//将位图图像编码为指定的stream流
                    byte[] buffer = stream.ToArray();//把这个流赋给byte数组
                    File.WriteAllBytes(fileDialog.FileName, buffer);//创建一个新文件给里面写只定的byte数组
                    MessageBox.Show("照片保存成功！", "提示");
                    //刷新修改界面的照片
                    FrmUpdateStuInfor.imgPath = fileDialog.FileName;
                    //摄像头显示；预览区隐藏
                    picture.Visibility = Visibility.Visible;
                    pictrueYulan.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
