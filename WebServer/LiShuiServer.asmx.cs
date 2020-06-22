using BLL;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;


namespace WebServer
{
    /// <summary>
    /// LiShuiServer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class LiShuiServer : System.Web.Services.WebService
    {
        #region 一对一验证、一对N验证
        [WebMethod]
        //一对N验证
        public string CheckFacesMore(byte[] bytes, string keyCode)
        {
            try
            {
                Stream jpgStream = BytesToStream(bytes);
                string result = BaiDuAuthent.search(jpgStream, "usergroup");

                return result;
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public Stream BytesToStream(byte[] bytes)
        {

            Stream stream = new MemoryStream(bytes);

            return stream;

        }
        #endregion

        #region 根据用户ID进行查询
        [WebMethod]
        public DataTable UserInfo(String U)
        {
            UserInfoBLL ubll = new UserInfoBLL();
            DataTable dt = ubll.YZbyIDno(U);
            return dt;
        }
        #endregion

        #region 获取电子健康卡加密串
        /// <summary>
        /// 获取电子健康卡加密串
        /// </summary>
        /// <param name="KeyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string JKK(string KeyID)
        {
            String data = HttpUtility.UrlEncode(Crypto.RsaEncrypt(KeyID));//丽水电子健康卡参数加密
            string dzjkk = GetHttpResponse("http://10.178.205.81/zsjk-web/app/getQrData?data=" + data, 10000);
            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(dzjkk);
            string ID = jsonObj["entity"].ToString();
            ID = Crypto.DesDecrypt(ID.Replace(' ', '+'));
            return ID;
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

        #region 获取健康卡号
        [WebMethod]
        public string getJKK(string SName, string strSex, string Sidnum, string OnePhone)
        {
            //丽水接口参数拼接
            string data = "{\"name\": \"" + SName + "\",\"sex\":\"" + strSex + "\",\"cardType\":\"1\",\"cardCode\":\"" + Sidnum + "\",\"phone\":\"" + OnePhone + "\"}";
            data = HttpUtility.UrlEncode(Crypto.RsaEncrypt(data));//丽水电子健康卡参数加密
            String dzjkk = "";
            //测试地址—测试通过
            //dzjkk = GetHttpResponse("http://61.153.66.198:8890/zsjk-web/app/saveFace?data=" + data, 10000);//
            //正式地址—测试通过
            //接收健康卡
            dzjkk = GetHttpResponse("http://wx.jkls.gov.cn/zsjk-web/app/saveFace?data=" + data, 10000);
            return dzjkk;
        }
        #endregion

        #region 用户建档
        /// <summary>
        /// 用户建档
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="Phone"></param>
        /// <param name="IDno"></param>
        /// <param name="RealName"></param>
        /// <param name="LoginPass"></param>
        /// <param name="VerifyFlag"></param>
        /// <param name="CardType"></param>
        /// <param name="MedicareNo"></param>
        /// <param name="Address"></param>
        /// <returns></returns>
        [WebMethod]
        public int UserRegister(byte[] bytes, string Phone, string IDno, string RealName, string LoginPass, int VerifyFlag, int CardType, string MedicareNo, string Address, string Healthcard)
        {
            UserInfoBLL usbll = new UserInfoBLL();
            Models.UserInfoModel.UserInfo user = new Models.UserInfoModel.UserInfo();
            user.Phone = Phone;
            user.IDno = IDno;
            user.RealName = RealName;
            user.CardType = CardType;
            user.MedicareNo = MedicareNo;
            user.LoginPass = LoginPass;
            user.VerifyFlag = VerifyFlag;
            user.Address = Address;
            user.Healthcard = Healthcard;
            int Number = 0;
            try
            {
                //密码加密
                Random random = new Random();
                int random_value = random.Next(10000000, 99999999);//随机一个数字
                string SecretKey = PASSWORD_MODIFY.APP_Encode(random_value.ToString());//生成秘钥
                string Pass = PASSWORD_MODIFY.DES_Encode(LoginPass, random_value.ToString());//根据秘钥生成密码
                //说明符合注册条件(验证码和电话号码存在)
                user.SecretKey = SecretKey;
                user.LoginPass = Pass;
                //string birthNumber = user.IDno.Substring(6, 8); //获取出生日期
                string birthDate = "";
                string strSex = "";

                if (user.IDno.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
                {
                    birthDate = user.IDno.Substring(6, 4) + "-" + user.IDno.Substring(10, 2) + "-" + user.IDno.Substring(12, 2);
                    strSex = user.IDno.Substring(14, 3);
                }
                if (user.IDno.Length == 15)
                {
                    birthDate = "19" + user.IDno.Substring(6, 2) + "-" + user.IDno.Substring(8, 2) + "-" + user.IDno.Substring(10, 2);
                    strSex = user.IDno.Substring(12, 3);
                }

                if (int.Parse(strSex) % 2 == 0)//性别代码为偶数是女性奇数为男性
                {
                    user.Sex = "F"; //"女";
                }
                else
                {
                    user.Sex = "M"; //"男";
                }
                user.BirthDate = birthDate;

                DataTable dt = usbll.GetUserInfo(user.Phone, user.IDno);//检查数据库是否已经存在 电话号码和身份证
                if (dt.Rows.Count == 0)
                {
                    Number = usbll.AddUser(user);
                }

                //byte[] bmpData = System.IO.File.ReadAllBytes(System.IO.Path.GetFullPath(".\\") + "temp.jpg");
                string group = "usergroup";  //湘潭中心医院
                                             //string group = "xtdygroup";　　//湘潭第一医院
                string userInfoStr = "{\"UserId\":\"" + user.IDno + "\",\"RealName\":\"" + user.RealName + "\",\"Img\":\"" + user.Img +
                    "\",\"MedicareNo\":\"" + user.MedicareNo + "\",\"BirthDate\":\"" + user.BirthDate + "\",\"Phone\":\"" + user.Phone +
                    "\",\"VerifyFlag\":\"" + user.VerifyFlag + "\",\"FaceImg\":\"" + user.FaceImg + "\",\"Sex\":\"" + user.Sex + "\"}";

                string faceReg = BaiDuAuthent.RegUserFace(user.IDno, userInfoStr, group, bytes);


            }
            catch (Exception ex)
            {
                //PUBLIC_FUNCTION.LogFile("用户注册出现异常[UserRegister],原因:" + ex.Message);

            }
            return Number;
        }
        #endregion

        #region 三步查询验证信息
        /// <summary>
        /// 用户注册前身份验证
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [WebMethod]
        public string YZinfo(string cardid, string phone)
        {
            BLL.UserInfoBLL user = new UserInfoBLL();
            DataTable dt1 = user.YZbyPhoneNumber(phone);
            DataTable dt2 = user.YZbyIDno(cardid);
            if (dt1.Rows.Count == 1 && dt2.Rows.Count == 1)
            {
                DataTable dt3 = user.YZbyPhoneandIdno(phone, cardid);
                if (dt3.Rows.Count == 1)
                {
                    return "更新";
                }
                else
                {
                    return "身份证号或手机号已被注册！";
                }
            }
            else if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
            {
                return "注册";
            }
            else
            {
                return "身份证号或手机号已被注册！";
            }
        }
        #endregion

        #region 对已建档无健康卡的用户进行健康卡更新
        [WebMethod]
        public int updateJKK(string Healthcard, string IDno)
        {
            UserInfoBLL bll = new UserInfoBLL();
            Models.UserInfoModel.UserInfo user = new Models.UserInfoModel.UserInfo();
            user.Healthcard = Healthcard;
            user.IDno = IDno;
            int row = bll.updateJKK(user.Healthcard, user.IDno);
            return row;
        }
        #endregion
    }


}
