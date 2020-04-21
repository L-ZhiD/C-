using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    /// <summary>
    /// 将任意指定对象序列化成字符串格式数据
    /// </summary>
    public class SerializeObjectTostring//照片
    {
        /// <summary>
        /// 将对象进行序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            BinaryFormatter binary = new BinaryFormatter();
            string result = string.Empty;//只读
            using (MemoryStream stream = new MemoryStream()) //创建一个流
            {
                binary.Serialize(stream,obj);//把binary的值定向给steram
                byte[] buffer = new byte[stream.Length];//获取流的长度
                buffer = stream.ToArray();//将buffer的内容写入流中
                result = Convert.ToBase64String(buffer);//转换字符
                stream.Flush();//重写
            }
            return result;
        }
        /// <summary>
        /// 反序列化成对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object DeserializeObject(string str) //把之前装的流返回去(换成照片)
        {
            BinaryFormatter binary = new BinaryFormatter();
            byte[] buffer = Convert.FromBase64String(str);
            object obj = null;
            using (Stream stream = new MemoryStream(buffer, 0, buffer.Length))
            {
                obj = binary.Deserialize(stream);
            }
            return obj;
        }
    }
}
