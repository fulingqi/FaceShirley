using BLL;
using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using Core;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services;
using System.Xml;

namespace WebServer
{
    /// <summary>
    /// WSFaces 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WSFaces : System.Web.Services.WebService
    {


        #region 根据身份证、手机号查询用户信息
        [WebMethod(Description = "根据身份证、手机号查询用户信息")]
        public DataTable GetUserIdAndPhone(String Phone, String ID)
        {
            UserInfoDAL dal = new UserInfoDAL();
            return dal.GetUserInfo(Phone, ID);
        }
        #endregion

        #region 发送验证码
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="txtOnePhone"></param>
        /// <returns></returns>
        [WebMethod(Description = "发送手机验证码")]
        public string VerificationCode(string Phone)
        {
            string result = "";

            Random random = new Random();
            string mialcode = random.Next(100000, 999999).ToString();//随机生成一个验证码
            string url = "http://121.42.164.134:11122/WSFaces.asmx/VerificationCode";  

            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) throw new ArgumentNullException();
            //request.ContentType = "application/json;charset=utf-8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "Post";
            string postData = "Phone=" + Phone;
            var data = Encoding.UTF8.GetBytes(postData);
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            postStream.Close();
            //发送请求并获取相应回应数据  

            var response = request.GetResponse() as HttpWebResponse;
            //return mialcode.ToString();

           
           
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            result = sr.ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            result = JsonConvert.SerializeXmlNode(doc);
            //JObject obj = JObject.Parse(result);
            //result = obj["text"].ToString();//.SelectSingleNode("string").InnerText;

            return result;
        }
        #endregion

        #region 添加用户信息

        //添加之前应该根据传过来的用户信息进行核验
        //判断数据库里是否存在相同的信息
        //如果存在相同的信息那就不Insert 

        //否则 进行Insert操作

        #endregion

        #region 一对一验证、一对N验证
        [WebMethod]
        //一对N验证
        public string CheckFacesMore(byte[] bytes, string keyCode)
        {
            try
            {
                string uid = "";
                string scores = "";
                string user_info = "";

                Stream jpgStream = BytesToStream(bytes);
                string result = BaiDuAuthent.search(jpgStream, "usergroup");

                return result;
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        #endregion

        #region 获取用户头像
        /// <summary>
        /// 获取用户 图片
        /// </summary>
        /// <param name="sfz"></param>
        /// <param name="sjh"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取用户头像信息")]
        public String GetAvatar(String sfz, String sjh)
        {
            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("sfz", sfz);
            dic.Add("sjh", sjh);
            String json = Get("http://121.42.164.134:998/api/UserInfo/Userimg", dic);
            return json;
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public string Get(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            //添加参数
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }
        #endregion


        #region 公安验证
        [WebMethod(Description = "公安验证")]
        public string AuthenPliceFace(string IdCard, string RealName, string Phone, string Nation, string Address, byte[] bytes)
        {
            string result = "";
            try
            {
                Stream jpgStream = BytesToStream(bytes);
                result = BaiDuAuthent.AuthenPliceFaceTwo(IdCard, RealName, Phone, Nation, Address, jpgStream);
            }
            catch
            {
                return "0";
            }
            return result;
        }

        public Stream BytesToStream(byte[] bytes)
        {

            Stream stream = new MemoryStream(bytes);

            return stream;

        }
        #endregion

        //#region 公安验证原来
        //[WebMethod(Description = "公安验证")]
        //public string AuthenPliceFace(string IdCard, string RealName, byte[] bytes)
        //{
        //    string result = "";
        //    try
        //    {
        //        Stream jpgStream = BytesToStream(bytes);
        //        result = "111";
        //        //result = BaiDuAuthent.AuthenPliceFaceTwo(IdCard, RealName, jpgStream);
        //    }
        //    catch
        //    {
        //        return "0";
        //    }
        //    return result;
        //}

        
        //#endregion


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

        [WebMethod]
        public void RealNameAuth(string IdCard, string RealName, string Phone, string HospCode)
        {
            UserInfoBLL RNAbll = new UserInfoBLL();
            DataTable dt = RNAbll.GetRealNameAuthByIdCardHospCode(IdCard, HospCode);
            if (dt.Rows.Count == 0)
            {
                int result_Ins = RNAbll.InsertRealNameAuth(HospCode, IdCard, RealName, Phone, "manage", 1);
                if (result_Ins > 0)
                {
                    //插入成功；                            
                }
            }
        }
        #endregion

        #region 支付宝接口
        //支付宝接口

        IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(Config.Gatewayurl, Config.AppId, Config.PrivateKey, Config.version, Config.SignType,
            Config.AlipayPublicKey, Config.CharSet);

        /// <summary>
        /// 生成支付二维码
        /// </summary>/
        // <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns>返回拉起支付宝的连接</returns>
        [WebMethod]
        public String ScanCodeGen(string orderName, string orderAmount, string outTradeNo)
        {

            AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(orderName, orderAmount, outTradeNo);
            //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
            //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。
            AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder);
            //string notify_url = "http://10.5.21.14/Pay/Notify"; //商户接收异步通知的地址
            //AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder, notify_url);

            //以下返回结果的处理供参考。
            //payResponse.QrCode即二维码对于的链接
            //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。
            String Result = "";
            switch (precreateResult.Status)
            {
                case ResultEnum.SUCCESS:
                    Result = "{'code':'1','msg':'";

                    Result += precreateResult.response.QrCode + "," + precreateResult.response.OutTradeNo + "'";
                    //轮询订单结果
                    //根据业务需要，选择是否新起线程进行轮询
                    //ParameterizedThreadStart parStart = new ParameterizedThreadStart(LoopQuery);
                    //Thread myThread = new Thread(parStart);
                    //object o = precreateResult.response.OutTradeNo;
                    //myThread.Start(o);
                    break;
                case ResultEnum.FAILED:
                    Result = "{'code':'2','msg':'";
                    // Console.WriteLine("生成二维码失败：" + precreateResult.response.Body);
                    Result += "生成二维码失败：" + precreateResult.response.Body + "'}";//
                    break;
                case ResultEnum.UNKNOWN:
                    Result = "{'code':'2','msg':'";
                    Result += "生成二维码失败：" + (precreateResult.response == null ? "配置或网络异常，请检查后重试" : "系统异常，请更新外部订单后重新发起请求") + "'}"; break;//
                                                                                                                                       //Console.WriteLine("生成二维码失败：" + (precreateResult.response == null ? "配置或网络异常，请检查后重试" : "系统异常，请更新外部订单后重新发起请求")); break;
            }

            return Result;
            //MemoryStream ms = new MemoryStream();
            //bitmap.Save(ms, ImageFormat.Png);
            //byte[] bytes = ms.GetBuffer();
            //return File(bytes, "image/png");
        }




        /// <summary>
        /// 构造支付请求数据
        /// </summary>
        /// <param name="orderName">订单名称</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="outTradeNo">订单编号</param>
        /// <returns>请求结果集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(string orderName, string orderAmount, string outTradeNo)
        {
            //线上联调时，请输入真实的外部订单号。
            if (string.IsNullOrEmpty(outTradeNo))
            {
                outTradeNo = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            }
            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号
            //builder.seller_id = Config.Uid;
            //订单编号
            builder.out_trade_no = outTradeNo;
            //订单总金额
            builder.total_amount = orderAmount;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = orderName;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";
            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "goods id";
            goods.goods_name = "goods name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;
            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;
            return builder;
        }

        /// <summary>
        /// 轮询
        /// </summary>
        /// <param name="o">订单号</param>
        [WebMethod]
        public String LoopQuery(object o)
        {
            AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();
            int count = 100;
            int interval = 10000;
            string out_trade_no = o.ToString();

            for (int i = 1; i <= count; i++)
            {
                Thread.Sleep(interval);
                queryResult = serviceClient.tradeQuery(out_trade_no);
                if (queryResult != null)
                {
                    if (queryResult.Status == ResultEnum.SUCCESS)
                    {
                        return DoSuccessProcess(queryResult);
                    }
                }
            }
            return DoFailedProcess(queryResult);
        }

        [WebMethod]
        public String LoopQuerys(object o)
        {
            AlipayF2FQueryResult queryResult = new AlipayF2FQueryResult();

            string out_trade_no = o.ToString();


            queryResult = serviceClient.tradeQuery(out_trade_no);
            if (queryResult != null)
            {
                if (queryResult.Status == ResultEnum.SUCCESS)
                {
                    return DoSuccessProcess(queryResult);
                }
            }
            return DoFailedProcess(queryResult);
        }

        /// <summary>
        /// 请添加支付成功后的处理
        /// </summary>
        private String DoSuccessProcess(AlipayF2FQueryResult queryResult)
        {
            //支付成功，请更新相应单据
            // log.WriteLine("扫码支付成功：外部订单号" + queryResult.response.OutTradeNo);
            return "{'code':'1','msg':'支付成功'}";
        }

        /// <summary>
        /// 请添加支付失败后的处理
        /// </summary>
        private String DoFailedProcess(AlipayF2FQueryResult queryResult)
        {
            //支付失败，请更新相应单据
            return "{'code':'2','msg':'支付失败,请重新支付'}";
        }

        #endregion

        #region 添加刷脸缴费记录
        [WebMethod]
        public int AddPaymentRecord(String HospCode, String HisOrderNum, String Billnumber, String Billmoney, String Source, String UserId, String Payment, String Sourceaccount)
        {
            #region 参数解密
            HospCode = PASSWORD_MODIFY.APP_Decode(HospCode.Replace(" ", "+"));
            HisOrderNum = PASSWORD_MODIFY.APP_Decode(HisOrderNum.Replace(" ", "+"));
            Billnumber = PASSWORD_MODIFY.APP_Decode(Billnumber.Replace(" ", "+"));
            Billmoney = PASSWORD_MODIFY.APP_Decode(Billmoney.Replace(" ", "+"));
            Source = PASSWORD_MODIFY.APP_Decode(Source.Replace(" ", "+"));
            UserId = PASSWORD_MODIFY.APP_Decode(UserId.Replace(" ", "+"));
            Payment = PASSWORD_MODIFY.APP_Decode(Payment.Replace(" ", "+"));
            Sourceaccount = PASSWORD_MODIFY.APP_Decode(Sourceaccount.Replace(" ", "+"));
            #endregion
            PaymentRecordBLL bll = new PaymentRecordBLL();
            int i = bll.AddPaymentRecord(HospCode, HisOrderNum, Billnumber, Convert.ToDouble(Billmoney), Source, Convert.ToInt32(UserId), Convert.ToInt32(Payment), Sourceaccount);
            return i;
        }
        #endregion

        #region 查询硬件是否启用
        [WebMethod]
        public int GetEnable(string PMHospName, int EquipmentNumber)
        {
            PointMaintenBLL bll = new PointMaintenBLL();
            return bll.GetEnable(PMHospName, EquipmentNumber);
        }
        #endregion
    }
}
