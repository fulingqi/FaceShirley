using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServer
{

    public class Config
    {
        // 应用ID,您的APPID
        public static string AppId = "2018030502317718";
        /// <summary>
        /// 合作商户uid
        /// </summary>
       // public static string Uid = "mingyidoc@126.com";
        // 支付宝网关
        public static string Gatewayurl = "https://openapi.alipay.com/gateway.do";
        // 商户私钥，您的原始格式RSA私钥
        public static string PrivateKey = "MIIEpQIBAAKCAQEAy0ILFuf5lPkyHW79M63+zmB+JsmRSlQmXorVbI9ou1vJSP9tWSMPZkMe5kQzFoae/K44LZn3cmDfqhwdeX1vSksv1umM6PVtCGi2CpHV7Rwo/clcMK6dxykFG0ciqKHkKev6AYJdTtODXyzPRWsk2DteShtNbvDQ9MGTPy6GuUCT2+Cuyo9DcVgvc4WuqLxn+bNqpF8npXVLUlAJqeQM0zdk3ne45batgr5J+oPXKg8neJblYmgt+KbmtWeGk1SxTHBw3qA4MueCuut8Yj3LfnUGi97cwriS7q7uSzH91NnsaieQpcENOfJUQhjG50KgRIg2OuptOc1YcYj1Zj97qQIDAQABAoIBAQC3l4PkwMvTH/OAWowibftF9ip8Znzxomi15Lk6QZ7b/OAnK3Bdnyl9uQrj+p4arqZUnhjoN7YmfhII2TRWVFJ9zMP9Xx+EHIrLmak0it2sOk9cTNEUt+STzB1ssihAIVqx8w/y5Qna9XooMZVWopy5I4bbcSXUG3jiWybhXukZ0fqh5YisWO27KicHNCzwRaVLPy0mEScRjaxUUz6KH1Pam9oPNugfFkNPDmQXE3HDPpelGann7f6/HdJuc5uJhk4OD11iddjp9aY+dnjzvqkn9F2P+G1aZyn3JQVbvknC3aLka4BoTrjV74TptAkbcfYV7pf+ZmyqCwDY5WVkqNXBAoGBAOY+DwbuMzca9DP5zoRFqej5AHjwNwlulDXSrwyEaKxQN8IGGNfrnEWZ+4JE9Jpquc69zGbdQUUyF+dbxfpPwlr9lzqU1puemAoHhTSaNC0okNTUMvOkVwTj2KNwMR8UIym+AfwV+HsW0xbLnUyTq9iuO2maA4Mul+/WBpQrJECdAoGBAOH/LIxL0u562zGhjqtT7MdXT3V+0MpbZKFzqQDBgYtD4LQc9HG4zvT49lDhNJ/SRg8/Qcnq0MTFz/JOTxyzhPMvq6Z6arcHjxyWAZ+G5KpCmNXlUEZhOFkrEYmX/ZyL6+2mdmmX1L54ZuUzZRBtdEel6uYjs9pr1twpYbFvWvt9AoGBAK1uVbdsuhtqLETyiMNODly1I8v1dh+esfmRB9av7oDNtBJssU4D5iTvhmpsMqwWdHvssAlT5JSQ1Tyq/og8iDQuDTpNXxK99dyoKYikF2VkxyPp669nPT0ru8Xw/q9gZdRNkgUnL5LYcI0dNxG1sp53L72uC7NG55/7Yd0+WgJpAoGAWraUaRQebL8/sKJKpBah2jn/mQf4QuGAeRX15mnF1+K9FU7yB5vI5qfwJvLerrA2kvQvMh9hATrthzNLqhec7AhcfAxzPVh5Z14G9MOukXD97A2JtVZcyd3xwisSD35SiyfgK+5X5MRreOCyVm+41vGQYiQ+kxPqQKETp6cs+/kCgYEAqiBkOfLRAAuue1YTaALDW6qEsQZOtb3G8WZadl0HKMMT7hP2NwAVq6LrXN5BR6c1RyJxXcRoSgeMsfnhpuRcs2CRScLheJGOvsmu2yBQa76uq2+SfQALzNxIsEQNuZBDrZb3AgQ/oPtUjPRNPLU4LPVkBiY32vTtdpYV1Ovc1IM=";
        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArbsD2b2pxOkPDlVAcWB2BNOyRRR+ELs0BONnbrsYA9BhPNGSUA3Nc/s/6gDUTpKs2Nwbn3PbIrqrCANzkFYlpDehnfcQBasYYzo+XcHs0AgE89t555w2uxoHmE0MfRdAcaN1WyFXxI4ZHE8NgGzJRCLWDToemN5aMhPiZ1wCX56Pfs7U6GvN9+D6VzRNMf4JMQlVrBreUw1v4iv7OXibCWDnCZXi5CSMFrpMZq81IpRJ8dw7yvtvReWegfItXJ1DnWG7sZ+HEzyklXmuWUoxmf4AW5VHWA6CbwO4610ugoNXmM8r88Y7H1ONNe8Qi4sMgyAaanW/yHFjJhtrqGExcQIDAQAB";
        // 签名方式
        public static string SignType = "RSA2";
        // 编码格式
        public static string CharSet = "UTF-8";

        public static string version = "1.0";
        //支付宝网关
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

    }
}