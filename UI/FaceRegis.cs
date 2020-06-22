using AForge.Video;
using AForge.Video.DirectShow;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WebServer;

namespace UI
{
    public partial class FaceRegis : Form
    {
        public String yPhone = "";//接受手机验证码的号码
        public String yCode = "";//验证码
        public String Address = "";//家庭地址
        public static string HospCode = "1001"; //湘潭第一医院
        public static string Mge = "";
        public static string MedicalId = "";

        public static string PragramID = "2"; //程序编号

        public static string SName = ""; //完整姓名
        public static string Sidnum = "";//完整身份证信息

        public static int staus = 1;//timer 状态 0：启用计时器  1：关闭计时器


        Test.WSFaces wf = new Test.WSFaces();



        public FaceRegis()
        {
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            #region 给软键盘按钮附加click事件
            this.button1.Click += new System.EventHandler(this.button0_Click);
            this.button2.Click += new System.EventHandler(this.button0_Click);
            this.button3.Click += new System.EventHandler(this.button0_Click);
            this.button4.Click += new System.EventHandler(this.button0_Click);
            this.button5.Click += new System.EventHandler(this.button0_Click);
            this.button6.Click += new System.EventHandler(this.button0_Click);
            this.button7.Click += new System.EventHandler(this.button0_Click);
            this.button8.Click += new System.EventHandler(this.button0_Click);
            this.button9.Click += new System.EventHandler(this.button0_Click);
            #endregion

        }


        #region 显示遮罩层
        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);
        #endregion


        private void FaceRegis_Shown(object sender, EventArgs e)
        {
            #region 启用timer3计时器记录用户界面无操作的总时间
            timer3 = new System.Windows.Forms.Timer();
            x = Control.MousePosition.X;
            y = Control.MousePosition.Y;

            timer3.Interval = 1000;
            timer3.Tick += new EventHandler(timer3_Tick);



            #endregion

            #region 显示MDI遮罩层写法

            #region 程序初始化MDI遮罩界面
            Mask m = new Mask();
            m.MdiParent = this;
            m.StartPosition = FormStartPosition.CenterScreen;
            m.Show();
            SetParent((int)m.Handle, (int)this.Handle);
            #endregion

            #region 提示信息MDI遮罩界面
            //TS_Mask tsm = new TS_Mask();
            //tsm.MdiParent = this;
            //tsm.StartPosition = FormStartPosition.CenterScreen;
            //tsm.Show();
            //SetParent((int)tsm.Handle, (int)this.Handle); 
            #endregion

            #endregion

            #region 打开摄像头
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
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

            #region 寻找身份证读卡器
            string stmp;
            int i, nRet;
            uint[] iBaud = new uint[1];
            i = Syn_FindReader();
            m_iPort = i;
            if (i > 0)
            {
                if (i > 1000)
                {
                    stmp = Convert.ToString(i);
                    stmp = Convert.ToString(System.DateTime.Now) + "  读卡器连接在USB " + stmp;
                }
                else
                {
                    System.Threading.Thread.Sleep(200);
                    nRet = Syn_GetCOMBaud(i, ref iBaud[0]);
                    stmp = Convert.ToString(i);
                    stmp = Convert.ToString(System.DateTime.Now) + "  读卡器连接在COM " + stmp + ";当前波特率为 " + Convert.ToString(iBaud[0]);
                }
            }
            else
            {
                stmp = Convert.ToString(System.DateTime.Now) + "  没有找到读卡器";
                MessageBox.Show(stmp);
            }
            #endregion

        }


        private FilterInfoCollection videoDevices;
        public static Bitmap imgFace;




        private void FaceRegis_Load(object sender, EventArgs e)
        {


            #region 窗体最大化
            this.SetVisibleCore(false);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.SetVisibleCore(true);
            #endregion

            //在窗体加载时隐藏lbl3标签
            linkLabel3.Visible = false;

            //启用timer2控件
            timer2.Start();
            //窗体加载时隐藏软键盘
            Paneljp.Visible = false;

        }


        #region 分别给显示时间的lbl控件设置年月日，时分秒
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            string ts1 = txtOneName.Text.Trim();

            if (ts1 != "请将您的身份证放置于读卡区域")
            {
                timer4.Stop();
            }

            if (staus == 0)
            {
                timer3.Start();
                timer4.Start();
            }
            lbltime.Text = DateTime.Now.ToString("HH:mm:ss").Trim();
            lbltime1.Text = DateTime.Now.ToString("yyyy.MM.dd").Trim();

        }
        #endregion

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
                Logging.LogFile(s);
            }
        }

        #region 人脸图片上传
        /// <summary>
        /// 人脸图片上传
        /// </summary>
        /// <param name="sfz">身份证号码</param>
        public void ImgUpdate(Object sfz)
        {
            #region 保存注册图片，用于手动输入
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            //myWebClient.UploadFile("http://192.168.2.249:123/ImgUpdate.ashx?sfz=" + sfz + "&yy=usergroup", "POST", System.IO.Path.GetFullPath(".\\") + "temp.jpg");
            myWebClient.UploadFile("http://localhost:4862/ImgUpdate.ashx?sfz=" + sfz + "&yy=usergroup", "POST", System.IO.Path.GetFullPath(".\\") + "temp.jpg");
            #endregion
        }
        #endregion

        #region 提示信息MDI遮罩界面
        public void Ts_Show(String typ, String msg)
        {
            TS_Mask tsm = new TS_Mask();
            tsm.Type = typ;
            tsm.msg = msg;
            tsm.MdiParent = this;
            tsm.StartPosition = FormStartPosition.CenterScreen;
            tsm.Show();
            SetParent((int)tsm.Handle, (int)this.Handle);
        }
        #endregion

        #region 文本框内容清空
        public void Empty()
        {
            txtOneIDNum.Text = "";
            txtOneName.Text = "";
            txtOnePhone.Text = "";
            textCode.Text = "";
            yPhone = "";
            yCode = "";
            linkLabel2.Enabled = true;
        }
        #endregion

        #region 身份证号脱敏
        public String replaceStr(String param)
        {
            int len = param.Length;
            if (len < 9)
            {
                return param;
            }
            return Regex.Replace(param, "(.{" + (len < 12 ? 3 : 6) + "})(.*)(.{4})", "$1" + "****" + "$3");

        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {


            #region   检验是否正确

            
            String Mge = "";
            //#region 非空验证
            //if (String.IsNullOrEmpty(txtOneName.Text))
            //{
            //    MessageBox.Show("请输入姓名");
            //    return;
            //}
            //if (String.IsNullOrEmpty(txtOneIDNum.Text))
            //{
            //    MessageBox.Show("请输入身份证号码");
            //    return;
            //}
            //if (String.IsNullOrEmpty(txtOnePhone.Text))
            //{
            //    MessageBox.Show("请输入手机号码");
            //    return;
            //}
            //if (String.IsNullOrEmpty(textCode.Text))
            //{
            //    MessageBox.Show("请输入验证码");
            //    return;
            //}
            //#endregion
            #region 验证身份证是否合法
            Sidnum = "142727199802101033";
            SName = "付玲琪";
            string cid = CheckCidInfo18(Sidnum);
            if (cid != "")
            {
                Mge = cid;
                Ts_Show("2", Mge);//显示提示界面
                return;
            }
            #endregion
            //#region 验证手机号码是否合法
            //if (!Phone(txtOnePhone.Text.Trim()))
            //{
            //    Mge = "手机号码格式错误";
            //    Ts_Show("2", Mge);//显示提示界面
            //    return;
            //}
            //#endregion
            //#region 判断手机号与验证码是否合法
            //if (!String.IsNullOrEmpty(txtOnePhone.Text))
            //{
            //    if (txtOnePhone.Text != yPhone)//如果接收验证码的手机与文本框的手机不一致
            //    {
            //        MessageBox.Show("手机号码不一致");
            //        return;
            //    }
            //    if (String.IsNullOrEmpty(textCode.Text))//如果验证码为空
            //    {
            //        MessageBox.Show("请输入验证码");
            //        return;
            //    }
            //    if (textCode.Text.Trim() != yCode)//与发送的验证码不一致
            //    {
            //        MessageBox.Show("验证码错误");
            //        return;
            //    }
            //}
            //#endregion




            #region 抓取图片
            if (File.Exists(System.IO.Path.GetFullPath(".\\") + "temp.jpg"))
            {
                File.Delete(System.IO.Path.GetFullPath(".\\") + "temp.jpg");
            }
            imgFace.Save(System.IO.Path.GetFullPath(".\\") + "temp.jpg");
            imgFace.Dispose();
            #endregion

            Test.WSFaces wsf = new Test.WSFaces();
            #region 调用公安验证
            FileStream jpgStream = new FileStream(System.IO.Path.GetFullPath(".\\") + "temp.jpg", FileMode.Open);
            byte[] bytes = StreamToBytes(jpgStream);
            string result = "222";//wsf.AuthenPliceFace(Sidnum, SName, bytes);
            jpgStream.Close();
            jpgStream.Dispose();
            #endregion

            #region 业务逻辑


            if (!result.Contains("error_code"))//如果识别通过
            {
                if (Convert.ToDouble(result) >= 0.8)//大于等于0.8表明身份验证通过
                {
                    //获取身份证号码和手机号
                    string cardid = Sidnum;
                    string phone = this.txtOnePhone.Text.Trim();
                    //两步查询确保用户信息唯一
                    LiShuiServer ls = new LiShuiServer();
                    string lsresult = ls.YZinfo(cardid, phone);
                    if (lsresult == "身份证号或手机号已被注册！")
                    {
                        Ts_Show("2", lsresult);
                        Empty();
                        //timer4.Start();
                        //string ts1 = txtOneName.Text.Trim();

                        //if (ts1 != "请将您的身份证放置于读卡区域")
                        //{
                        //    timer4.Stop();
                        //}
                        return;

                    }

                    #region 获取基本信息
                    string Sexnum = "";
                    string strSex = "";
                    if (cardid.Length == 18)
                    {
                        Sexnum = cardid.Substring(14, 3);
                    }
                    if (cardid.Length == 15)
                    {
                        Sexnum = cardid.Substring(12, 3);
                    }
                    if (int.Parse(Sexnum) % 2 == 0)
                    {
                        strSex = "2";//女2
                    }
                    else
                    {
                        strSex = "1"; //男1
                    }

                    //丽水RSA加密的公钥
                    //String key = "<RSAKeyValue><Modulus>yiOA0woIRt0KIHv1DauhGRMyxMu/sxKz345ilwJbbRk38tPF+sJzB1+N3C0f5izAtO31nXQ2JO6ekKRneEvhiaiIBk3E2iui20hBPLE+z1Y7ycezCwpKqh4Xr7GMbmi+MDMCvBDGcVaS6xp43cE29swxe+N0VipvFVIcKDRil18=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                    string jjk = ls.getJKK(SName, strSex, Sidnum, txtOnePhone.Text.Trim());
                    // JObject jsonObj = JsonConvert.DeserializeObject<JObject>(dzjkk);//jsonArrayText必须是带[]数组格式字符串 string str = jArray[0]["a"].ToString();


                    JObject jsonObj = JsonConvert.DeserializeObject<JObject>(jjk);//jsonArrayText必须是带[]数组格式字符串 string str = jArray[0]["a"].ToString();
                    string error_code = jsonObj["resultCode"].ToString();//获取错误代码 

                    string dzjkk = jsonObj["entity"].ToString();//获取健康卡号 
                    #endregion

                    if (lsresult == "更新")//说明是老用户 
                    {
                        #region 老用户健康卡更新

                        if (ls.updateJKK(dzjkk, Sidnum) > 0)
                        {
                            #region 通过线程启动图片上传功能,解决了图片上传造成的性能慢的问题
                            Thread t1 = new Thread(new ParameterizedThreadStart(ImgUpdate));//线程
                            t1.Start(Sidnum);
                            #endregion
                            Empty();//清空文本框内容
                            Ts_Show("0", "");//显示提示注册成功界面
                            string ts1 = txtOneName.Text.Trim();

                            if (ts1 != "请将您的身份证放置于读卡区域")
                            {
                                timer4.Stop();
                            }
                            return;
                        }
                        else
                        {
                            Mge = "用户已经注册！!";
                            Ts_Show("1", Mge);
                            Empty();
                        }
                        #endregion
                    }
                    else if (lsresult == "注册")
                    {
                        //用户信息没有注册，进行添加操作
                        #region 用户注册
                        if (error_code == "0")
                        {
                            //给Model赋值
                            Models.UserInfoModel.UserInfo user = new Models.UserInfoModel.UserInfo();
                            user.Phone = phone;
                            user.RealName = SName;
                            user.IDno = Sidnum;
                            user.VerifyFlag = 2;
                            user.LoginPass = "123456";
                            user.Address = Address;
                            user.Healthcard = dzjkk;
                            //user.InsertDate = DateTime.Now;

                            try
                            {
                                int res = ls.UserRegister(bytes, user.Phone, user.IDno, user.RealName, user.LoginPass, user.VerifyFlag, 1, "", user.Address, user.Healthcard);
                                if (res > 0)
                                {
                                    #region 通过线程启动图片上传功能,解决了图片上传造成的性能慢的问题
                                    Thread t1 = new Thread(new ParameterizedThreadStart(ImgUpdate));//线程
                                    t1.Start(Sidnum);
                                    #endregion
                                    Empty();//清空文本框内容
                                    Ts_Show("0", "");//显示提示注册成功界面
                                    string ts1 = txtOneName.Text.Trim();

                                    if (ts1 != "请将您的身份证放置于读卡区域")
                                    {
                                        timer4.Stop();
                                    }
                                    return;
                                }
                                else
                                {
                                    Mge = "创建档案失败．";
                                    Ts_Show("2", Mge);
                                    Empty();
                                }
                            }
                            catch (Exception ex)
                            {
                                Mge = "创建档案失败．";
                                Ts_Show("2", Mge);
                                Empty();
                            }
                        }
                        else
                        {
                            JObject jsonObj1 = JsonConvert.DeserializeObject<JObject>(dzjkk);//jsonArrayText必须是带[]数组格式字符串 string str = jArray[0]["a"].ToString();
                            string error_code1 = jsonObj["entity"].ToString();//获取错误代码
                            Ts_Show("2", error_code1);
                            Empty();
                        }
                        #endregion
                    }

                }
                else
                {
                    Double ImNum = Convert.ToDouble(result);
                    ImNum = ImNum * 100;
                    Mge = "照片与本人的相似度只有" + ImNum.ToString() + "%";
                    //MessageBox.Show(Mge);
                    Ts_Show("2", Mge);//显示提示界面
                    Empty();
                }
             

            }
            else//如果识别不通过
            {
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);//jsonArrayText必须是带[]数组格式字符串 string str = jArray[0]["a"].ToString();
                string error_code = jsonObj["error_code"].ToString();//获取错误代码
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
                Ts_Show("2", Mge);//显示提示界面
            }
            #endregion
            #endregion

        }

        #region 文件转二进制流
        public byte[] StreamToBytes(Stream stream)
        {

            byte[] bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 

            stream.Seek(0, SeekOrigin.Begin);

            return bytes;

        }
        #endregion

        #region 手机号码验证
        public bool Phone(string phone)
        {
            Regex rx = new Regex(@"((^13[0-9]{1}[0-9]{8}|^15[0-9]{1}[0-9]{8}|^14[0-9]{1}[0-9]{8}|^16[0-9]{1}[0-9]{8}|^17[0-9]{1}[0-9]{8}|^18[0-9]{1}[0-9]{8}|^19[0-9]{1}[0-9]{8})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
            return rx.IsMatch(phone);
        }
        #endregion

        #region 获取验证码倒计时
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (linkLabel3.Text != "获取验证码" && int.Parse(linkLabel3.Text.Substring(0, linkLabel3.Text.Length - 5)) > 0)
            {

                linkLabel3.Text = int.Parse(linkLabel3.Text.Substring(0, linkLabel3.Text.Length - 5)) - 1 + " 秒后重试";
            }
            else
            {
                linkLabel2.Enabled = true;
                linkLabel3.Visible = false;
                linkLabel2.Visible = true;
                timer1.Enabled = false;
            }
        }
        #endregion

        #region 获取验证码
        private void linkLabel2_LinkClicked(object sender, EventArgs e)
        {
            try
            { //WebReference.WSFaces wsf = new WebReference.WSFaces();//我们的接口服务
                Paneljp.Visible = false;


                Test.WSFaces wsf = new Test.WSFaces();
                yPhone = txtOnePhone.Text.Trim();
                //Regex rx = new Regex(@"((^13[0-9]{1}[0-9]{8}|^15[0-9]{1}[0-9]{8}|^14[0-9]{1}[0-9]{8}|^16[0-9]{1}[0-9]{8}|^17[0-9]{1}[0-9]{8}|^18[0-9]{1}[0-9]{8}|^19[0-9]{1}[0-9]{8})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
                if (!Phone(txtOnePhone.Text.Trim()))
                {
                    MessageBox.Show("手机号码格式有误", "温馨提示", MessageBoxButtons.OK);
                    return;
                }
                yCode = wsf.VerificationCode(txtOnePhone.Text.Trim());//验证码
                linkLabel2.Enabled = false;
                linkLabel3.Text = "30 秒后重试";
                linkLabel2.Visible = false;
                linkLabel3.Visible = true;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Logging.LogFile(s);
            }


            #region 倒计时事件
            timer1.Enabled = true;
            #endregion
        }
        #endregion

        #region 身份证格式验证,以及15.18位互转方法
        /// <summary>
        /// 验证18位身份证格式
        /// </summary>
        /// <param name="cid"></param>
        /// <returns>返回字符串,出错信息</returns>
        public string CheckCidInfo18(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|X|x)$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return "- 您的身份证号码格式有误!";
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return "- 您的身份证号码格式有误!";//非法生日
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);

            }
            if (iSum % 11 != 1)
                return ("- 您的身份证号码格式有误!");//非法证号

            return "";

        }

        /// <summary>
        /// 验证15位身份证格式
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public string CheckCidInfo15(string cid)
        {
            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };

            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{15}$");
            System.Text.RegularExpressions.Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return "- 您的身份证号码格式有误!";
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (int.Parse(cid.Substring(0, 2)) > aCity.Length)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "- 您的身份证号码格式有误!";//非法地区
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 2) + "-" + cid.Substring(8, 2) + "-" + cid.Substring(10, 2));
            }
            catch
            {
                return "- 您的身份证号码格式有误!";//非法生日
            }
            return "";
        }

        /// <summary>
        /// 15位转18位身份证号
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public string per15To18(string perIDSrc)
        {
            int iS = 0;
            //加权因子常数
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数
            string LastCode = "10X98765432";
            //新身份证号
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字
            perIDNew += "19";
            perIDNew += perIDSrc.Substring(6, 9);
            //进行加权求和
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

        /// <summary>
        /// 18位转15位身份证号
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public string per18To15(string perIDSrc)
        {
            //前6位
            string str1 = perIDSrc.Substring(0, 6);
            //后9位
            string str2 = perIDSrc.Substring(8, 9);
            //新字符串
            string perIDNew = str1 + str2;
            return perIDNew;

        }
        #endregion

        #region 刷身份证所用到的基类
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct IDCardData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name; //姓名   
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string Sex;   //性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string Nation; //名族
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string Born; //出生日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
            public string Address; //住址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string IDCardNo; //身份证号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string GrantDept; //发证机关
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeBegin; // 有效开始日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeEnd;  // 有效截止日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string reserved; // 保留
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string PhotoFileName; // 照片路径
        }
        /************************端口类API *************************/
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetCOMBaud(int iPort, ref uint puiBaudRate);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetCOMBaud(int iPort, uint uiCurrBaud, uint uiSetBaud);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        public static extern int Syn_OpenPort(int iPort);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ClosePort", CharSet = CharSet.Ansi)]
        public static extern int Syn_ClosePort(int iPort);
        /**************************SAM类函数 **************************/
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetMaxRFByte", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetMaxRFByte(int iPort, byte ucByte, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ResetSAM", CharSet = CharSet.Ansi)]
        public static extern int Syn_ResetSAM(int iPort, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetSAMStatus", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMStatus(int iPort, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetSAMID", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMID(int iPort, ref byte pucSAMID, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetSAMIDToStr", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMIDToStr(int iPort, ref byte pcSAMID, int iIfOpen);
        /*************************身份证卡类函数 ***************************/
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_StartFindIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_StartFindIDCard(int iPort, ref byte pucIIN, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SelectIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_SelectIDCard(int iPort, ref byte pucSN, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadBaseMsg(int iPort, ref byte pucCHMsg, ref uint puiCHMsgLen, ref byte pucPHMsg, ref uint puiPHMsgLen, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseMsgToFile", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadBaseMsgToFile(int iPort, ref byte pcCHMsgFileName, ref uint puiCHMsgFileLen, ref byte pcPHMsgFileName, ref uint puiPHMsgFileLen, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseFPMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadBaseFPMsg(int iPort, ref byte pucCHMsg, ref uint puiCHMsgLen, ref byte pucPHMsg, ref uint puiPHMsgLen, ref byte pucFPMsg, ref uint puiFPMsgLen, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseFPMsgToFile", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadBaseFPMsgToFile(int iPort, ref byte pcCHMsgFileName, ref uint puiCHMsgFileLen, ref byte pcPHMsgFileName, ref uint puiPHMsgFileLen, ref byte pcFPMsgFileName, ref uint puiFPMsgFileLen, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadNewAppMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadNewAppMsg(int iPort, ref byte pucAppMsg, ref uint puiAppMsgLen, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_GetBmp", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetBmp(int iPort, ref byte Wlt_File);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadFPMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadFPMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData, ref byte cFPhotoname);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindReader", CharSet = CharSet.Ansi)]
        public static extern int Syn_FindReader();
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindUSBReader", CharSet = CharSet.Ansi)]
        public static extern int Syn_FindUSBReader();
        /***********************设置附加功能函数 ************************/
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoPath", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoPath(int iOption, ref byte cPhotoPath);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoName", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoName(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetSexType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetSexType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetNationType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetNationType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetBornType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetBornType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeBType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetUserLifeBType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeEType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetUserLifeEType(int iType, int iOption);

        int m_iPort;

        #endregion

        #region 软键盘手动输入数字
        TextBox tmpTextBox = null;//定义全局变量储存光标所在的textbox

        private void txtOnePhone_Enter(object sender, EventArgs e)
        {
            tmpTextBox = (TextBox)sender;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (tmpTextBox == null)
            {
                return;
            }
            //button1.TabIndex

            Button but = (Button)sender;


            switch (but.TabIndex.ToString())
            {
                case "38":
                    tmpTextBox.Text += "0";
                    break;
                case "29":
                    tmpTextBox.Text += "1";
                    break;
                case "30":
                    tmpTextBox.Text += "2";
                    break;
                case "31":
                    tmpTextBox.Text += "3";
                    break;
                case "32":
                    tmpTextBox.Text += "4";
                    break;
                case "33":
                    tmpTextBox.Text += "5";
                    break;
                case "34":
                    tmpTextBox.Text += "6";
                    break;
                case "35":
                    tmpTextBox.Text += "7";
                    break;
                case "36":
                    tmpTextBox.Text += "8";
                    break;
                case "37":
                    tmpTextBox.Text += "9";
                    break;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {


            if (tmpTextBox.Text.Length > 1)
            {
                string numm = tmpTextBox.Text.Trim();
                tmpTextBox.Text = numm.Substring(0, numm.Length - 1);
            }
            else
            {
                tmpTextBox.Text = "";
            }
        }
        #endregion

        #region 控制软键盘的显示与隐藏
        private void btnook_Click(object sender, EventArgs e)
        {
            Paneljp.Visible = false;
        }

        private void txtOnePhone_Click(object sender, EventArgs e)
        {
            Paneljp.Visible = true;
        }

        private void textCode_Click(object sender, EventArgs e)
        {
            Paneljp.Visible = true;
        }
        #endregion

        #region 验证码输入框调用软键盘
        private void textCode_Enter(object sender, EventArgs e)
        {
            tmpTextBox = (TextBox)sender;
        }
        #endregion

        #region 点击屏幕区域隐藏软键盘
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Paneljp.Visible = false;
        }
        #endregion

        #region 关闭窗体时清理内存
        private void FaceRegis_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                // this.Close();
                Environment.Exit(0);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 设置若用户30秒内无任何操作则显示MDI遮罩层

        int x, y;
        DateTime start;
        bool ff = true;

        private void timer3_Tick(object sender, EventArgs e)
        {
            int x1 = Control.MousePosition.X;
            int y1 = Control.MousePosition.Y;

            if ((x == x1) && (y == y1) && ff)
            {
                start = DateTime.Now;
                ff = false;
            }
            if (x != x1 || y != y1)
            {
                x = x1;
                y = y1;
                start = DateTime.Now;
                ff = true;
            }
            TimeSpan ts = DateTime.Now.Subtract(start);

            if (ts.Seconds == 30)
            {
                timer3.Stop();
                timer4.Stop();
                Empty();
                staus = 1;

                #region 显示MDI遮罩层写法

                #region 程序初始化MDI遮罩界面
                Mask m = new Mask();
                m.MdiParent = this;
                m.StartPosition = FormStartPosition.CenterScreen;
                m.Show();
                SetParent((int)m.Handle, (int)this.Handle);
                #endregion

                #region 提示信息MDI遮罩界面
                //TS_Mask tsm = new TS_Mask();
                //tsm.MdiParent = this;
                //tsm.StartPosition = FormStartPosition.CenterScreen;
                //tsm.Show();
                //SetParent((int)tsm.Handle, (int)this.Handle); 
                #endregion

                #endregion

                txtOneName.ForeColor = Color.Red;
                txtOneIDNum.ForeColor = Color.Red;
                txtOneName.Text = "请将您的身份证放置于读卡区域";
                txtOneIDNum.Text = "请等待片刻读取身份信息";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            start = DateTime.Now;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 通过timer4实现每隔规定时间自动读取身份证信息
        private void timer4_Tick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ReadIdcardInfo, null);

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void txtOneIDNum_TextChanged(object sender, EventArgs e)
        {

        }

        public void ReadIdcardInfo(object s)
        {
            #region 读取身份证信息
            IDCardData CardMsg = new IDCardData();
            int nRet, nPort, iPhotoType;
            string stmp;
            byte[] cPath = new byte[255];
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            nPort = m_iPort;
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            //Syn_SetPhotoPath(0,ref cPath[0]);	//设置照片路径	iOption 路径选项	0=C:	1=当前路径	2=指定路径
            //cPhotoPath	绝对路径,仅在iOption=2时有效
            iPhotoType = 0;
            Syn_SetPhotoType(0); //0 = bmp ,1 = jpg , 2 = base64 , 3 = WLT ,4 = 不生成
            Syn_SetPhotoName(2); // 生成照片文件名 0=tmp 1=姓名 2=身份证号 3=姓名_身份证号 

            Syn_SetSexType(1);  // 0=卡中存储的数据	1=解释之后的数据,男、女、未知
            Syn_SetNationType(1);// 0=卡中存储的数据	1=解释之后的数据 2=解释之后加"族"
            Syn_SetBornType(1);         // 0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD
            Syn_SetUserLifeBType(1);    // 0=YYYYMMDD,1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD
            Syn_SetUserLifeEType(1, 1); // 0=YYYYMMDD(不转换),1=YYYY年MM月DD日,2=YYYY.MM.DD,3=YYYY-MM-DD,4=YYYY/MM/DD,
                                        // 0=长期 不转换,	1=长期转换为 有效期开始+50年

            if (Syn_OpenPort(nPort) == 0)
            {
                if (Syn_SetMaxRFByte(nPort, 80, 0) == 0)
                {
                    nRet = Syn_StartFindIDCard(nPort, ref pucIIN[0], 0);
                    nRet = Syn_SelectIDCard(nPort, ref pucSN[0], 0);
                    int i = Syn_SetPhotoType(4);//不生成图片
                    nRet = Syn_ReadMsg(nPort, 0, ref CardMsg);

                    if (nRet == 0)
                    {
                        txtOneIDNum.ForeColor = Color.Black;
                        txtOneName.ForeColor = Color.Black;

                        SName = CardMsg.Name.Trim(); //完整姓名
                        txtOneName.Text = "*" + SName.Substring(1, SName.Length - 1); //加密后的姓名

                        //stmp = Convert.ToString(System.DateTime.Now) + "  性别:" + CardMsg.Sex;
                        stmp = Convert.ToString(System.DateTime.Now) + "  民族:" + CardMsg.Nation;
                        //stmp = Convert.ToString(System.DateTime.Now) + "  出生日期:" + CardMsg.Born;
                        Address = CardMsg.Address.Trim();

                        Sidnum = CardMsg.IDCardNo.Trim();   //完整身份证信息
                        txtOneIDNum.Text = replaceStr(CardMsg.IDCardNo.Trim());//加密后的身份证信息
                        //stmp = Convert.ToString(System.DateTime.Now) + "  发证机关:" + CardMsg.GrantDept;
                        //stmp = Convert.ToString(System.DateTime.Now) + "  有效期开始:" + CardMsg.UserLifeBegin;
                        //stmp = Convert.ToString(System.DateTime.Now) + "  有效期结束:" + CardMsg.UserLifeEnd;
                        //stmp = Convert.ToString(System.DateTime.Now) + "  照片文件名:" + CardMsg.PhotoFileName;
                        //if (iPhotoType == 0 || iPhotoType == 1)
                        //{
                        //    pictureBox1.Image = Image.FromFile(CardMsg.PhotoFileName);
                        //}
                        ClearMemory();
                    }
                    else
                    {
                        stmp = Convert.ToString(System.DateTime.Now) + "  读取身份证信息错误";
                    }
                }
            }
            #endregion
        }
        #endregion

    }

}
