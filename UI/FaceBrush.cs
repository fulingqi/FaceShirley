using AForge.Video;
using AForge.Video.DirectShow;
using BLL;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace UI
{
    public partial class FaceBrush : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public static Bitmap imgFace;


        public FaceBrush()
        {
            InitializeComponent();
        }

        private void FaceBrush_Load(object sender, EventArgs e)
        {
            #region 打开摄像头
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.DesiredFrameSize = new Size(260, 220);
                videoSource.DesiredFrameRate = 1;

                videPlayer.VideoSource = videoSource;
                videPlayer.Start();

                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            }
            catch (ApplicationException)
            {
                videoDevices = null;
            }
            #endregion
        }
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary> 
        /// 释放内存
        /// </summary> 
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                imgFace = (Bitmap)eventArgs.Frame.Clone();
                ClearMemory();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                #region 摄像头当扫码枪
                //if (imgFace == null)
                //{
                //    return;
                //}
                //#region 将图片转换成byte数组
                //MemoryStream ms = new MemoryStream();
                //imgFace.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //byte[] bt = ms.GetBuffer();
                //ms.Close();
                //#endregion
                //LuminanceSource source = new RGBLuminanceSource(bt, imgFace.Width, imgFace.Height);
                //BinaryBitmap bitmap = new BinaryBitmap(new ZXing.Common.HybridBinarizer(source));
                //Result result;
                //try
                //{
                //    //开始解码
                //    result = new MultiFormatReader().decode(bitmap);
                //    if (result != null)
                //    {
                //        label1.Text = result.ToString();
                //    }

                //}
                //catch (ReaderException re)
                //{
                //    return;
                //} 
                #endregion

                #region 抓取图片
                if (File.Exists(System.IO.Path.GetFullPath(".\\") + "tempface.jpg"))
                {
                    File.Delete(System.IO.Path.GetFullPath(".\\") + "tempface.jpg");
                }
                imgFace.Save(System.IO.Path.GetFullPath(".\\") + "tempface.jpg");
                imgFace.Dispose();
                FileStream jpgStream = new FileStream(System.IO.Path.GetFullPath(".\\") + "tempface.jpg", FileMode.Open);
                byte[] bytes = StreamToBytes(jpgStream);
                #endregion

                #region 一对N验证
                LS.LiShuiServer wf = new LS.LiShuiServer();//外网地址
                //WebNtit.LiShuiServer wf = new WebNtit.LiShuiServer();//内网
                string result = wf.CheckFacesMore(bytes, "");
                #endregion

                #region 安全验证逻辑取数
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);
                string error = jsonObj["error_code"].ToString();//错误代码

                #endregion

                if (error == "0")
                {
                    String StrJson = jsonObj["result"]["user_list"].ToString();
                    string scores = StrJson.Replace("[", "").Replace("]", "").Trim();
                    JObject StrObj = JsonConvert.DeserializeObject<JObject>(scores);
                    if (Convert.ToDouble(StrObj["score"].ToString()) > 80)
                    {
                        //查询用户信息
                        UserInfoBLL ubll = new UserInfoBLL();
                        // DataTable udt = ubll.Ct(StrObj["user_id"].ToString());//HIS查询
                        //MessageBox.Show("数据条数：" + udt.Rows.Count.ToString());
                        //DataTable dt = wf.UserInfo(StrObj["user_id"].ToString());//134查询
                        string entity = "";
                        //if (udt.Rows.Count == 0)//判断HIS是否存在用户数据
                        //{
                        //MessageBox.Show("请先注册！");//如果没有，请先注册
                        //HIS建档操作
                        string info = StrObj["user_info"].ToString();
                        JObject jsonObjInfo = JsonConvert.DeserializeObject<JObject>(info);
                        string SName = jsonObjInfo["RealName"].ToString(); //姓名
                                                                           //string SPhone = jsonObjInfo["Phone"].ToString(); //电话
                        string SIDno = StrObj["user_id"].ToString();   //身份证号码
                                                                       //string xb = jsonObjInfo["Sex"].ToString();         //性别
                        string birthDate = "";
                        string strSex = "";
                        string Ssex = "";
                        if (SIDno.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
                        {
                            birthDate = SIDno.Substring(6, 4) + "-" + SIDno.Substring(10, 2) + "-" + SIDno.Substring(12, 2);
                            strSex = SIDno.Substring(14, 3);
                        }
                        if (SIDno.Length == 15)
                        {
                            birthDate = "19" + SIDno.Substring(6, 2) + "-" + SIDno.Substring(8, 2) + "-" + SIDno.Substring(10, 2);
                            strSex = SIDno.Substring(12, 3);
                        }

                        if (int.Parse(strSex) % 2 == 0)//性别代码为偶数是女性奇数为男性
                        {
                            Ssex = "女"; //"女";
                        }
                        else
                        {
                            Ssex = "男"; //"男";
                        }
                        string brithday = birthDate; //出生日期


                        //传参his建档
                        string ceshi = ubll.sssa(SName, Ssex, SIDno, brithday);
                        entity = ceshi;
                        // DataTable udt = ubll.Ct(StrObj["user_id"].ToString());//HIS查询
                        //if (ceshi.Rows.Count == 0)
                        //{
                        //    MessageBox.Show("您所提供的信息有误，建档失败！");
                        //}
                        //else
                        //{
                        //    entity = ceshi.Rows[0][0].ToString();

                        //}

                        //}
                        //else
                        //{
                        //    if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))//如果有，判断134是否存在数据
                        //    {
                        //        MessageBox.Show("请先注册！");
                        //        return;
                        //    }

                        #region 健康卡
                        //string KeyID = dt.Rows[0]["Healthcard"].ToString();//获取健康卡ID
                        //String data = HttpUtility.UrlEncode(Crypto.RsaEncrypt(KeyID));//丽水电子健康卡参数加密
                        ////string dzjkk = GetHttpResponse("http://wx.jkls.gov.cn/zsjk-web/app/getQrData?data=" + data, 10000);//外网
                        //string dzjkk = GetHttpResponse("http://10.178.205.81/zsjk-web/app/getQrData?data=" + data, 10000);//内网
                        //JObject jsonObja = JsonConvert.DeserializeObject<JObject>(dzjkk);
                        //entity = jsonObja["entity"].ToString();
                        //if (jsonObja["resultCode"].ToString() != "0")
                        //{
                        //    MessageBox.Show(entity);//提示错误原因
                        //}
                        //entity = Crypto.DesDecrypt(entity.Replace(' ', '+'));//解密ID串 
                        #endregion

                        // entity = udt.Rows[0][0].ToString();//取HIS的门诊号

                        // }
                        //if (string.IsNullOrEmpty(entity))
                        //{
                        //    MessageBox.Show("没有获取到用户门诊号！");
                        //    return;
                        //}
                        Clipboard.SetDataObject(entity);


                        //IDataObject iData = Clipboard.GetDataObject();
                        //if (iData.GetDataPresent(DataFormats.Text))
                        //{
                        //    MessageBox.Show((String)iData.GetData(DataFormats.Text));
                        //}
                        #region Win32
                        //IntPtr i = FindWindow(null, "信息系统门户-门急诊");//寻找主窗体
                        //if (i.ToInt32() == 0)
                        //{
                        //    MessageBox.Show("没有找到工作站！");
                        //    return;
                        //}
                        //SetForegroundWindow(i);

                        #region 寻找子窗体
                        //IntPtr iBt = FindWindowEx(i, "病人刷卡", true);
                        //if (iBt.ToInt32() == 0)
                        //{
                        //    //this.lbMsg.Text = "病人刷卡";
                        //}
                        #endregion

                        #region 寻找主窗体
                        IntPtr iss = FindWindow(null, "LIS");
                        SetForegroundWindow(iss);
                        //if (iss.ToInt32() == 0)
                        //{
                        //    //this.lbMsg.Text = "没有找到0选取病人界面";
                        //    // return;
                        //}
                        #endregion

                        #region 寻找窗体控件
                        //IntPtr la = FindWindowEx(iss, IntPtr.Zero, null, "确定(&O)");
                        //IntPtr laa = FindWindowEx(iss, IntPtr.Zero, null, "");
                        //IntPtr laaa = FindWindowEx(iss, laa, null, ""); 
                        //IntPtr las = FindWindowEx(iss, IntPtr.Zero, null, "读卡(F1)");
                        #endregion

                        #region 测试代码

                        SetCursorPos(120, 168);

                        mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
                        //Thread.Sleep(1000);

                        //IntPtr la = FindWindowEx(iss, IntPtr.Zero, null, "刷卡：");

                        //SendMessage(iss, 0xF5, 0, 0);
                        //SetCursorPos(786, 378);

                        //mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
                        SetCursorPos(531, 360);
                        Thread.Sleep(300);


                        SendKeys.Send("^V");

                        //mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);
                        //SetCursorPos(531, 360);
                        Thread.Sleep(500);
                         
                        //Clipboard.SetDataObject("");
                        //SetCursorPos(531, 906);

                        //mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);

                        SendKeys.Send("{ENTER}");//回车
                                                 //Clipboard.SetDataObject(" ");
                        #endregion

                        #endregion

                    }
                    else
                    {
                        MessageBox.Show("请先注册！");
                    }
                }
                else
                {
                    MessageBox.Show(ErrorCode(error));
                }
                //Clipboard.SetDataObject("");
            }
            catch (Exception ex)
            {
                Logging.LogFile(ex.ToString());
            }
        }
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        #region 图片转二进制流
        public byte[] StreamToBytes(Stream stream)
        {

            byte[] bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 

            stream.Seek(0, SeekOrigin.Begin);

            return bytes;

        }
        #endregion

        #region Win32
        #region 获取主窗体句柄
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string strclassName, string strWindowName);
        #endregion

        #region 窗体前置
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region 寻找子窗体
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        public static IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            iResult = FindWindowEx(hwnd, 0, null, lpszWindow);
            if (iResult != IntPtr.Zero) return iResult;
            if (!bChild) return iResult;
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = FindWindowEx(h, 0, null, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    iResult = f1;
                    return false;
                }
            },
            0);
            return iResult;
        }


        #region 寻找子窗体控件
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string stringlpszClass, string lpszWindow);
        #endregion
        #endregion

        #region 鼠标定位
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        #endregion

        #region 鼠标点击
        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        #endregion
        #endregion

        #region V3百度错误代码
        /// <summary>
        /// V3百度错误代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public String ErrorCode(string code)
        {
            String txt = "";
            switch (code)
            {
                case "4":
                    txt = "集群超限额";
                    break;
                case "6":
                    txt = "没有接口权限";
                    break;
                case "17":
                    txt = "每天流量超限额";
                    break;
                case "18":
                    txt = "QPS超限额";
                    break;
                case "19":
                    txt = "请求总量超限额";
                    break;
                case "100":
                    txt = "无效的access_token参数";
                    break;
                case "110":
                    txt = "Access Token失效";
                    break;
                case "111":
                    txt = "Access token过期";
                    break;
                case "222001":
                    txt = "必要参数未传入";
                    break;
                case "222002":
                    txt = "参数格式错误";
                    break;
                case "222003":
                    txt = "参数格式错误";
                    break;
                case "222004":
                    txt = "参数格式错误";
                    break;
                case "222005":
                    txt = "参数格式错误";
                    break;
                case "222006":
                    txt = "参数格式错误";
                    break;
                case "222007":
                    txt = "参数格式错误";
                    break;
                case "222008":
                    txt = "参数格式错误";
                    break;
                case "222009":
                    txt = "参数格式错误";
                    break;
                case "222010":
                    txt = "参数格式错误";
                    break;
                case "222011":
                    txt = "参数格式错误";
                    break;
                case "222012":
                    txt = "参数格式错误";
                    break;
                case "222013":
                    txt = "参数格式错误";
                    break;
                case "222014":
                    txt = "参数格式错误";
                    break;
                case "222015":
                    txt = "参数格式错误";
                    break;
                case "222016":
                    txt = "参数格式错误";
                    break;
                case "222017":
                    txt = "参数格式错误";
                    break;
                case "222018":
                    txt = "参数格式错误";
                    break;
                case "222019":
                    txt = "参数格式错误";
                    break;
                case "222020":
                    txt = "参数格式错误";
                    break;
                case "222021":
                    txt = "参数格式错误";
                    break;
                case "222022":
                    txt = "参数格式错误";
                    break;
                case "222023":
                    txt = "参数格式错误";
                    break;
                case "222024":
                    txt = "参数格式错误";
                    break;
                case "222025":
                    txt = "参数格式错误";
                    break;
                case "222026":
                    txt = "参数格式错误";
                    break;
                case "222027":
                    txt = "验证码长度错误(最小值大于最大值)";
                    break;
                case "222028":
                    txt = "参数格式错误";
                    break;
                case "222029":
                    txt = "参数格式错误";
                    break;
                case "222030":
                    txt = "参数格式错误";
                    break;
                case "222200":
                    txt = "该接口需使用application/json的格式进行请求";
                    break;
                case "222201":
                    txt = "服务端请求失败";
                    break;
                case "222202":
                    txt = "图片中没有人脸";
                    break;
                case "222203":
                    txt = "无法解析人脸";
                    break;
                case "222204":
                    txt = "从图片的url下载图片失败";
                    break;
                case "222205":
                    txt = "服务端请求失败";
                    break;
                case "222206":
                    txt = "服务端请求失败";
                    break;
                case "222207":
                    txt = "未找到匹配的用户";
                    break;
                case "222208":
                    txt = "图片的数量错误";
                    break;
                case "222209":
                    txt = "face token不存在";
                    break;
                case "222210":
                    txt = "人脸库中用户下的人脸数目超过限制";
                    break;
                case "222300":
                    txt = "人脸图片添加失败";
                    break;
                case "222301":
                    txt = "获取人脸图片失败";
                    break;
                case "222302":
                    txt = "服务端请求失败";
                    break;
                case "222303":
                    txt = "获取人脸图片失败";
                    break;
                case "223100":
                    txt = "操作的用户组不存在";
                    break;
                case "223101":
                    txt = "该用户组已存在";
                    break;
                case "223102":
                    txt = "该用户已存在";
                    break;
                case "223103":
                    txt = "找不到该用户";
                    break;
                case "223104":
                    txt = "group_list包含组数量过多";
                    break;
                case "223105":
                    txt = "该人脸已存在";
                    break;
                case "223106":
                    txt = "该人脸不存在";
                    break;
                case "223110":
                    txt = "uid_list包含数量过多";
                    break;
                case "223111":
                    txt = "目标用户组不存在";
                    break;
                case "223112":
                    txt = "quality_conf格式不正确";
                    break;
                case "223113":
                    txt = "人脸有被遮挡";
                    break;
                case "223114":
                    txt = "人脸模糊";
                    break;
                case "223115":
                    txt = "人脸光照不好";
                    break;
                case "223116":
                    txt = "人脸不完整";
                    break;
                case "223117":
                    txt = "app_list包含app数量过多";
                    break;
                case "223118":
                    txt = "质量控制项错误";
                    break;
                case "223119":
                    txt = "活体控制项错误";
                    break;
                case "223120":
                    txt = "活体检测未通过";
                    break;
                case "223121":
                    txt = "质量检测未通过 左眼遮挡程度过高";
                    break;
                case "223122":
                    txt = "质量检测未通过 右眼遮挡程度过高";
                    break;
                case "223123":
                    txt = "质量检测未通过 左脸遮挡程度过高";
                    break;
                case "223124":
                    txt = "质量检测未通过 右脸遮挡程度过高";
                    break;
                case "223125":
                    txt = "质量检测未通过 下巴遮挡程度过高";
                    break;
                case "223126":
                    txt = "质量检测未通过 鼻子遮挡程度过高";
                    break;
                case "223127":
                    txt = "质量检测未通过 嘴巴遮挡程度过高";
                    break;
                case "222901":
                    txt = "参数校验初始化失败";
                    break;
                case "222902":
                    txt = "参数校验初始化失败";
                    break;
                case "222903":
                    txt = "参数校验初始化失败";
                    break;
                case "222904":
                    txt = "参数校验初始化失败";
                    break;
                case "222905":
                    txt = "接口初始化失败";
                    break;
                case "222906":
                    txt = "接口初始化失败";
                    break;
                case "222907":
                    txt = "缓存处理失败";
                    break;
                case "222908":
                    txt = "缓存处理失败";
                    break;
                case "222909":
                    txt = "缓存处理失败";
                    break;
                case "222910":
                    txt = "数据存储处理失败";
                    break;
                case "222911":
                    txt = "数据存储处理失败";
                    break;
                case "222912":
                    txt = "数据存储处理失败";
                    break;
                case "222913":
                    txt = "接口初始化失败";
                    break;
                case "222914":
                    txt = "接口初始化失败";
                    break;
                case "222915":
                    txt = "后端服务连接失败";
                    break;
                case "222916":
                    txt = "后端服务连接失败";
                    break;
                case "222304":
                    txt = "图片尺寸太大";
                    break;
                case "223128":
                    txt = "正在清理该用户组的数据";
                    break;
                case "222361":
                    txt = "公安服务连接失败";
                    break;
                case "222046":
                    txt = "参数格式错误";
                    break;
                case "222101":
                    txt = "参数格式错误";
                    break;
                case "222102":
                    txt = "参数格式错误";
                    break;
                case "222307":
                    txt = "图片非法 鉴黄未通过";
                    break;
                case "222308":
                    txt = "图片非法 含有政治敏感人物";
                    break;
                case "222211":
                    txt = "人脸融合失败 模板图质量不合格";
                    break;
                case "222212":
                    txt = "人脸融合失败";
                    break;
                case "223129":
                    txt = "人脸未面向正前方（人脸的角度信息大于30度）";
                    break;
                case "222350":
                    txt = "公安网图片不存在或质量过低";
                    break;
                case "222351":
                    txt = "身份证号与姓名不匹配或该身份证号不存在";
                    break;
                case "222352":
                    txt = "身份证名字格式错误";
                    break;
                case "222353":
                    txt = "身份证号码格式错误";
                    break;
                case "222354":
                    txt = "公安库里不存在此身份证号";
                    break;
                case "222355":
                    txt = "身份证号码正确，公安库里没有对应的照片";
                    break;
                case "222356":
                    txt = "验证的人脸图片质量不符合要求";
                    break;
                case "222360":
                    txt = "身份证号码或名字非法（公安网校验不通过）";
                    break;
                case "216600":
                    txt = "输入身份证格式错误";
                    break;
                case "216601":
                    txt = "身份证号和名字匹配失败";
                    break;
                case "216430":
                    txt = "rtse/face 服务异常";
                    break;
                case "216431":
                    txt = "语音识别服务异常";
                    break;
                case "216432":
                    txt = "视频解析服务调用失败";
                    break;
                case "216433":
                    txt = "视频解析服务发生错误";
                    break;
                case "216434":
                    txt = "活体检测失败";
                    break;
                case "216500":
                    txt = "验证码位数错误";
                    break;
                case "216501":
                    txt = "没有找到人脸";
                    break;
                case "216502":
                    txt = "当前会话已失效";
                    break;
                case "216505":
                    txt = "redis连接失败";
                    break;
                case "216506":
                    txt = "redis操作失败";
                    break;
                case "216507":
                    txt = "视频中有多张人脸";
                    break;
                case "216508":
                    txt = "没有找到视频信息";
                    break;
                case "216509":
                    txt = "视频中的声音无法识别（声音过低或者有杂音导致无法识别）";
                    break;
                case "216908":
                    txt = "视频中人脸质量较差(返回信息中包含具体原因)";
                    break;
                default:
                    break;
            }
            return txt;
        }
        #endregion

        private void FaceBrush_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();

            Application.Exit();
            System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(Process.GetCurrentProcess().Id);
            process.Kill();
        }
    }

}

