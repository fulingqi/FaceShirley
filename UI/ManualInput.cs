using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UI
{
    public partial class ManualInput : Form
    {
        public ManualInput()
        {
            InitializeComponent();

        }
        private void ManualInput_Load(object sender, EventArgs e)
        {
            textName.Enabled = false;
        }

        private void ManualInput_Shown(object sender, EventArgs e)
        {
        }


        private void btnTwoText_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textIDno.Text) && String.IsNullOrEmpty(textPhone.Text))//如果身份证号与手机号码都为空，那么不允许操作
            {
                MessageBox.Show("请输入手机号或者身份证号码");
            }

            #region 判断手机号与验证码是否合法
            if (!String.IsNullOrEmpty(textPhone.Text))
            {
                if (textPhone.Text != Phone)//如果接收验证码的手机与文本框的手机不一致
                {
                    MessageBox.Show("手机号码不一致");
                    return;
                }
                if (String.IsNullOrEmpty(textCode.Text))//如果验证码为空
                {
                    MessageBox.Show("请输入验证码");
                    return;
                }
                if (textCode.Text.Trim() != Code)//与发送的验证码不一致
                {
                    MessageBox.Show("验证码错误");
                    return;
                }
            }
            #endregion
            //if ((!Regex.IsMatch(textIDno.Text.Trim(), @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))//正则表达式验证身份证号码是否合法
            //{
            //    MessageBox.Show("身份证格式有误");
            //}

            #region 查询患者信息及绑定界面数据
            //UserInfoBLL bll = new UserInfoBLL();
            //DataTable dt = bll.GetUserInfo(textPhone.Text.Trim(), textIDno.Text.Trim());
            Test.WSFaces wsf = new Test.WSFaces();
            DataTable dt = wsf.GetUserIdAndPhone(textPhone.Text.Trim(), textIDno.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该患者未注册");
                return;
            }
            if (dt.Rows.Count > 1)
            {
                MessageBox.Show("该患者数目不对，请联系工程师进行核实");
                return;
            }
            textName.Text = dt.Rows[0]["RealName"].ToString();
            String idno = dt.Rows[0]["IDno"].ToString();
            String JMidno = idno.Substring(0, 6);//身份证号码
            JMidno += "********" + idno.Substring(14, 4);
            textIDno.Text = JMidno;//需要加**
            String phones = dt.Rows[0]["Phone"].ToString();//手机号
            String JMphone = phones.Substring(0, 3);
            JMphone += "****" + phones.Substring(7, 4);
            textPhone.Text = JMphone;//需要加**
            #endregion

            #region 获取用户头像
            String json = wsf.GetAvatar("420922199401141012", "17621251520");

            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            string jcode = jo["code"].ToString();
            string jmessage = jo["message"].ToString();
            if (jcode == "1")
            {
                string jjson = jo["data"].ToString();
                jjson = jjson.Substring(1, jjson.Length - 1);
                jjson = jjson.Substring(0, jjson.Length - 1);
                JObject obj = JObject.Parse(jjson);

                string base64 = obj["base64"].ToString();//用户头像的Base64数据

                byte[] bytes = Convert.FromBase64String(base64);//Base64转二进制流
                MemoryStream memStream = new MemoryStream(bytes);
                Image mImage = Image.FromStream(memStream);//流文件转文件
                pictureBox2.Image = mImage; //显示在界面上
            }
            else
            {
                MessageBox.Show(jmessage);//提示
            }
            #endregion
            Phone = "";
            Code = "";
        }

        public String Phone = "";
        public String Code = "";

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (textPhone.Text.Length != 11)
            {
                //手机位数不对
            }
            Phone = textPhone.Text;//记录手机号
            Code = "生成的验证码";//记录生成的验证码
            //发送验证码
        }
    }
}
