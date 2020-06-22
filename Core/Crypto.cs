using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core
{
    public class Crypto
    {
        #region 丽水Java加密算法
        /// <summary>
        /// RSA 加密算法
        /// </summary>
        /// <param name="rawInput">内容</param>
        /// <returns></returns>
        public static string RsaEncrypt(string rawInput)
        {
            String publicKey = "<RSAKeyValue><Modulus>yiOA0woIRt0KIHv1DauhGRMyxMu/sxKz345ilwJbbRk38tPF+sJzB1+N3C0f5izAtO31nXQ2JO6ekKRneEvhiaiIBk3E2iui20hBPLE+z1Y7ycezCwpKqh4Xr7GMbmi+MDMCvBDGcVaS6xp43cE29swxe+N0VipvFVIcKDRil18=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            if (string.IsNullOrEmpty(rawInput))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(publicKey))
            {
                throw new ArgumentException("Invalid Public Key");
            }

            using (var rsaProvider = new RSACryptoServiceProvider())
            {
                var inputBytes = Encoding.UTF8.GetBytes(rawInput);//有含义的字符串转化为字节流
                rsaProvider.FromXmlString(publicKey);//载入公钥
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

        #region Java DES 解密
        /// <summary>
        /// DES 解密 注意:密钥必须为８位
        /// </summary>
        /// <param name="inputString">待解密字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(string inputString)
        {
            string decryptKey = "E.Al0p1O";
            byte[] byKey = null;
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[inputString.Length];
            byKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;//解析Java加密字符
            inputByteArray = Convert.FromBase64String(inputString);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        #endregion
    }
}
