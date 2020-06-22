using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace Core
{
    /// <summary>
    /// 丽水接口基类
    /// </summary>
    public class SeverClass
    {


        #region RSA加密算法
        /// <summary>
        /// RSA 加密算法
        /// </summary>
        /// <param name="rawInput">内容</param>
        /// <param name="publicKey">Key</param>
        /// <returns></returns>
        public static string RsaEncrypt(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }
            String key = "<RSAKeyValue><Modulus>yiOA0woIRt0KIHv1DauhGRMyxMu/sxKz345ilwJbbRk38tPF+sJzB1+N3C0f5izAtO31nXQ2JO6ekKRneEvhiaiIBk3E2iui20hBPLE+z1Y7ycezCwpKqh4Xr7GMbmi+MDMCvBDGcVaS6xp43cE29swxe+N0VipvFVIcKDRil18=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(key);//载入公钥
                int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
                var buffer = new byte[bufferSize];
                using (MemoryStream inputStream = new MemoryStream(inputBytes),
                     outputStream = new MemoryStream())
                {
                    while (true)
                    { //分段加密
                        int readSize = inputStream.Read(buffer, 0, bufferSize);
                        if (readSize <= 0)
                        {
                            break;
                        }

                        var temp = new byte[readSize];
                        Array.Copy(buffer, 0, temp, 0, readSize);
                        var encryptedBytes = rsaProvider.Encrypt(temp, false);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
                }
            }
        }

        #endregion

        #region Get请求
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        public string GetHttpResponse(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion
    }
}
