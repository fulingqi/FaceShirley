using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="strData"></param>
        public static void LogFile(string strData)
        {
            string m_sLogPath = System.AppDomain.CurrentDomain.BaseDirectory + "Log\\";


            string fileName = m_sLogPath + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".log";

            if (!Directory.Exists(m_sLogPath))
            {
                Directory.CreateDirectory(m_sLogPath);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Append))
            {
                // 创 建一个StreamWriter对象，使用UTF-8编码格式
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.WriteLine(string.Format("{0:HH:mm:ss}", DateTime.Now));
                    writer.WriteLine(strData);
                }
            }
        }
    }
}
