using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Log4net
    {
        static log4net.ILog logInfo = log4net.LogManager.GetLogger("Info");
        /// <summary>
        /// 用来记录错误日志
        /// </summary>
        static log4net.ILog logError = log4net.LogManager.GetLogger("Error");
        public static void WriteInfo(string msg)//这个方法是公开并静态的所以直接可以点
        {
            logInfo.Info(msg);
        }

        public static void WriteError(string msg,Exception ex)//这个方法是公开并静态的所以直接可以点
        {
            logError.Error($"{msg}。错误记载：{ex.Message}");
        }
    }
}
