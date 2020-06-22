using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class TS_Mask : Form
    {
        public TS_Mask()
        {
            InitializeComponent();

            #region 防止窗体闪烁
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            #endregion
        }

        #region Type类型说明
        /// <summary>
        /// Type类型说明
        /// </summary>
        /// <param Type="0">通过注册</param>
        /// <param Type="1">已注册</param>
        /// <param Type="2">注册失败</param>
        public String Type = "";
        #endregion

        SpeechSynthesizer ss = new SpeechSynthesizer();//文字转语音

        public String msg = "";
        private void Mask_Load(object sender, EventArgs e)
        {

            if (Type == "0")
            {
                label1.Visible = false;
                panel1.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath("../../Img/ts-ok.png"));//   获取路径
                msg = "注册成功，请到窗口或App进行刷脸挂号";
            }
            else if (Type == "1")
            {
                label1.Visible = false;
                panel1.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath("../../Img/ts-01.png"));//   获取路径
                msg = "您已注册，请勿重复注册";
            }
            else if (Type == "2")
            {
                label1.Visible = true;
                label1.Text = msg;
                panel1.BackgroundImage = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath("../../Img/ts-02.png"));//   获取路径
                msg += "认证失败，" + msg;
            }
            if (msg != "")
            {
                ss.Resume();
                ss.SpeakAsync(msg);//异步朗读
            }

            #region 计时关闭窗体
            Timer timer1 = new Timer();
            timer1.Interval = 5000;
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(panel1_Click);//添加事件 
            #endregion
        }

        #region 点击任意位置关闭
        /// <summary>
        /// 点击任意位置关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Click(object sender, EventArgs e)
        {
            ss.Pause();//停止朗读
            this.Close();//关闭窗体
            

        }
        #endregion
    }
}
