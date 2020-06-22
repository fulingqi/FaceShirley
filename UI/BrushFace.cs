using AForge.Video;
using AForge.Video.DirectShow;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace UI
{
    public partial class BrushFace : Form
    {

        private FilterInfoCollection videoDevices;
        public static Bitmap imgFace;
        public BrushFace()
        {
            InitializeComponent();

            //防止窗体闪烁
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

        }

        private void BrushFace_Load(object sender, EventArgs e)
        {
            #region 打开摄像头
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
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


        #region 验证信息
        int frequency = 0;//刷脸次数
        private void btnFace_Click(object sender, EventArgs e)
        {
            #region 第三次显示手动验证界面
            if (frequency > 1)
            {
                linkLabel1.Visible = true;
            }
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
            //string base64 = Convert.ToBase64String(bytes);
            //Clipboard.SetDataObject(base64);
            //return;
            #endregion

            #region 一对N验证
            Test.WSFaces wf = new Test.WSFaces();
            string result = wf.CheckFacesMore(bytes, "");
            #endregion
            jpgStream.Close();
            jpgStream.Dispose();

            var data = GetData2JArray(result, "result");
            if (data != null && data.Count > 0)
            {
                string uid = "";
                string scores = "";
                string user_info = "";
                foreach (var item in data)
                {
                    uid = item["uid"].ToString();
                    scores = item["scores"].ToString();
                    user_info = item["user_info"].ToString();
                }
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(user_info);

                string userId = jsonObj["UserId"].ToString();
                string realName = jsonObj["RealName"].ToString();
                scores = scores.Replace("[", "").Replace("]", "").Trim();

                if (Convert.ToDouble(scores) > 80)
                {
                    #region Win32
                    IntPtr i = FindWindow(null, "湘潭第一人民医院信息系统门户");//寻找主窗体

                    SetForegroundWindow(i);//窗体前置

                    #region 寻找子窗体
                    IntPtr iBt = FindWindowEx(i, "病人刷卡", true);
                    if (iBt.ToInt32() == 0)
                    {
                        //this.lbMsg.Text = "病人刷卡";
                    }
                    #endregion

                    #region 寻找主窗体
                    IntPtr iss = FindWindow(null, "病人刷卡");
                    if (iss.ToInt32() == 0)
                    {
                        //this.lbMsg.Text = "没有找到0选取病人界面";
                        // return;
                    }
                    #endregion

                    #region 寻找窗体控件
                    //IntPtr la = FindWindowEx(iss, IntPtr.Zero, null, "确定(&O)");
                    //IntPtr laa = FindWindowEx(iss, IntPtr.Zero, null, "");
                    //IntPtr laaa = FindWindowEx(iss, laa, null, ""); 
                    //IntPtr las = FindWindowEx(iss, IntPtr.Zero, null, "读卡(F1)");
                    #endregion

                    SetCursorPos(22, 115);//鼠标定位

                    mouse_event(0x0002 | 0x0004, 0, 0, 0, 0);//鼠标点击

                    Thread.Sleep(200);//等待执行

                    Clipboard.SetDataObject("");//复制到剪切板

                    SendKeys.Send("^V");//粘贴

                    SendKeys.Send("{ENTER}");//回车
                    #endregion


                }
                else
                {
                    //请先注册
                    MessageBox.Show("请先注册");
                    return;
                }
            }
            else
            {
                //错误代码
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);//jsonArrayText必须是带[]数组格式字符串 string str = jArray[0]["a"].ToString();
                string error_code = jsonObj["error_code"].ToString();//获取错误代码
                String Mge = "";
                #region 错误代码
                switch (error_code)
                {
                    case "4":
                        Mge = "集群超限额";
                        break;
                    case "6":
                        Mge = "没有接口权限";
                        break;
                    case "17":
                        Mge = "每天流量超限额";
                        break;
                    case "18":
                        Mge = "QPS超限额";
                        break;
                    case "19":
                        Mge = "请求总量超限额";
                        break;
                    case "100":
                        Mge = "无效的access_token参数";
                        break;
                    case "110":
                        Mge = "Access Token失效";
                        break;
                    case "111":
                        Mge = "Access token过期";
                        break;
                    case "216100":
                        Mge = "参数异常，具体异常原因详见error_msg";
                        break;
                    case "216101":
                        Mge = "缺少必须的参数，具体异常原因详见error_msg";
                        break;
                    case "216102":
                        Mge = "请求了不支持的服务";
                        break;
                    case "216103":
                        Mge = "请求超长";
                        break;
                    case "216110":
                        Mge = "appid不存在";
                        break;
                    case "216111":
                        Mge = "userid信息非法";
                        break;
                    case "216200":
                        Mge = "图片为空或者base64解码错误";
                        break;
                    case "216201":
                        Mge = "图片格式错误";
                        break;
                    case "216202":
                        Mge = "图片大小错误";
                        break;
                    case "216300":
                        Mge = "数据库异常";
                        break;
                    case "216400":
                        Mge = "后端识别服务异常";
                        break;
                    case "216402":
                        Mge = "未找到人脸";
                        break;
                    case "216500":
                        Mge = "未知错误";
                        break;
                    case "216611":
                        Mge = "用户不存在";
                        break;
                    case "216613":
                        Mge = "删除用户图片记录失败";
                        break;
                    case "216614":
                        Mge = "两两比对中图片数少于2张，无法比较";
                        break;
                    case "216615":
                        Mge = "服务处理该图片失败";
                        break;
                    case "216616":
                        Mge = "图片已存在";
                        break;
                    case "216617":
                        Mge = "新增用户图片失败";
                        break;
                    case "216618":
                        Mge = "组内用户为空";
                        break;
                    case "216631":
                        Mge = "本次请求添加的用户数量超限";
                        break;
                    case "216501":
                        Mge = "传入的生活照中没有找到人脸";
                        break;
                    case "216600":
                        Mge = "身份证格式错误，请检查后重新输入";
                        break;
                    case "216601":
                        Mge = "身份证号码与姓名不匹配，或无法找到此身份证号码";
                        break;
                    case "216602":
                        Mge = "输入生活照人脸遮挡，质量检测不通过";
                        break;
                    case "216603":
                        Mge = "人脸光照不好，质量检测不通过";
                        break;
                    case "216604":
                        Mge = "人脸不完整，质量检测不通过";
                        break;
                    case "216605":
                        Mge = "质量检测不通过";
                        break;
                    case "216606":
                        Mge = "人脸模糊，质量检测不通过";
                        break;
                    case "216607":
                        Mge = "公安库无此人图片或公安小图质量不佳";
                        break;
                    case "216608":
                        Mge = "输入的生活照活体校验不通过";
                        break;
                    case "216609":
                        Mge = "人脸左右角度过大";
                        break;
                    case "216610":
                        Mge = "人脸俯仰角度过大	";
                        break;
                    default:
                        break;
                }
                #endregion
                MessageBox.Show(Mge);
            }
            frequency = frequency + 1;
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

        #region 字符串转Json
        public static JArray GetData2JArray(string result, string key)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            return (JArray)obj[key];
        }
        #endregion

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

        #region 显示手动验证界面
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManualInput mi = new ManualInput();
            mi.ShowDialog();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            AttendPeople ap = new AttendPeople();
            ap.ShowDialog();
        }

        #region 启动双显—定时器
        int Nu = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Nu < 2)
            {
                SendKeys.Send("%{F1}");
            }
            else
            {
                timer1.Enabled = false;
            }
            Nu = Nu + 1;
        }
        #endregion

        private void BrushFace_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }


        #region 刷脸缴费
        /// <summary>
        /// DateTime时间格式转换为13位带毫秒的Unix时间戳
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public long ConvertDateTimeLong(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }


        /// <summary>
        /// 根据图片路径转换为二进制流
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        public byte[] GetPictureData(string imagepath)
        {
            /**/
            ////根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        /// <summary>
        /// 生成二维码并展示到页面上
        /// </summary>
        /// <param name="precreateResult">二维码串</param>
        private void DoWaitProcess(String QrCode)
        {
            string filename = "";
            String data = QrCode;//如果是链接会跳转
            if (!string.IsNullOrEmpty(data))
            {
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap imgBack = encoder.Encode(data, System.Text.Encoding.UTF8);
                Image logoImg = Image.FromFile(System.IO.Path.GetFullPath("../../Img/zfb.jpg"));//logo路径
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(92, 92);//二维码大小
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                g.DrawImage(imgBack, 0, 0, 92, 92);
                g.DrawImage(logoImg, 50 - 20, 50 - 20, 32, 32);//计算Logo的位置及大小
                filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString() + ".jpg";
                bitmap.Save(System.IO.Path.GetFullPath(".\\") + filename);
            }
            byte[] bit = GetPictureData(filename);
            MemoryStream memStream = new MemoryStream(bit);
            Image mImage = Image.FromStream(memStream);//流文件转文件
            pictureBox1.Image = mImage;//二维码显示到控件上
            File.Delete(System.IO.Path.GetFullPath(".\\") + filename);//删除二维码
        }

        public String res = "";

        //定时器判断用户是否支付成功
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (res != "")
            {
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(res);
                if (jsonObj["code"].ToString() == "1")
                {
                    timer2.Enabled = false;
                    MessageBox.Show(jsonObj["msg"].ToString());//支付成功
                    #region 记录刷脸支付的数据
                    Test.WSFaces wsf = new Test.WSFaces();
                    String HospCode = PASSWORD_MODIFY.APP_Encode("1001");
                    HisOrderNum = PASSWORD_MODIFY.APP_Encode(HisOrderNum);
                    String Source = PASSWORD_MODIFY.APP_Encode("刷脸支付");
                    SJC = PASSWORD_MODIFY.APP_Encode(SJC);//Billnumber
                    DDJE = PASSWORD_MODIFY.APP_Encode(DDJE);//订单金额
                    UserID = PASSWORD_MODIFY.APP_Encode(UserID);
                    String Payment = PASSWORD_MODIFY.APP_Encode("1");
                    XMMC = PASSWORD_MODIFY.APP_Encode(XMMC);
                    int i = wsf.AddPaymentRecord(HospCode, HisOrderNum, SJC, DDJE, Source, UserID, Payment, XMMC);
                    #endregion
                    return;
                }
                else
                {
                    timer2.Enabled = false;
                    MessageBox.Show(jsonObj["msg"].ToString());
                    return;
                }
            }
        }

        /// <summary>
        /// 开启轮询
        /// </summary>
        /// <param name="qye"></param>
        public void LoopQuery(object qye)
        {
            Test.WSFaces wsf = new Test.WSFaces();
            try
            {
                res = wsf.LoopQuery(qye.ToString());
            }
            catch (Exception)
            {
                res = wsf.LoopQuery(qye.ToString());
            }
        }
        String SJC = "";//生成时间戳
        String XMMC = "珍间支付—";//订单名称
        String DDJE = "0.01";//订单金额
        String HisOrderNum = "";//识别序号
        String UserID = "194";//明医用户ID

        private void button1_Click_1(object sender, EventArgs e)
        {

            //获取用户信息，取数




            #region 生成支付宝二维码
            SJC = ConvertDateTimeLong(DateTime.Now).ToString();//生成时间戳
            Test.WSFaces wsf = new Test.WSFaces();
            String PayJson = wsf.ScanCodeGen(XMMC, DDJE, SJC);//获取生成支付宝二维码的数据

            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(PayJson);
            string code = jsonObj["code"].ToString();//获取返回code 
            if (code == "1")
            {
                String[] jso = jsonObj["msg"].ToString().Split(',');
                DoWaitProcess(jso[0]);//生成二维码

                timer2.Enabled = true;
                Thread t1 = new Thread(new ParameterizedThreadStart(LoopQuery));//线程—开启轮询
                t1.Start(jso[1]);
            }
            else
            {
                MessageBox.Show("二维码生成失败");
            }
            #endregion
        }
        #endregion


    }
}
