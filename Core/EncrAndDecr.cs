using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
    /// <summary>
    /// 加密与解密
    /// </summary>
    public class EncrAndDecr
    {
        #region 加密算法
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <returns></returns>
        public static String Encryption(String source)
        {
            string _DESKey = "app_pass";

            StringBuilder sb = new StringBuilder();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] key = ASCIIEncoding.ASCII.GetBytes(_DESKey);
                byte[] iv = ASCIIEncoding.ASCII.GetBytes(_DESKey);
                byte[] dataByteArray = Encoding.UTF8.GetBytes(source);
                des.Mode = System.Security.Cryptography.CipherMode.CBC;
                des.Key = key;
                des.IV = iv;
                string encrypt = "";
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
                return encrypt;
            }
        }
        #endregion

        #region 解密算法
        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="source">需要解密的字符串</param>
        /// <returns></returns>
        public static String Decryption(String source)
        {
            string sKey = "app_pass";


            byte[] inputByteArray = System.Convert.FromBase64String(source);// Encoding.UTF8.GetBytes(source);


            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion
    }
}
