using Baidu.Aip;
using Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebServer
{
    public class BaiDuAuthent
    {
        //http://aixiaoshuai.mydoc.io/?t=239873
        private static System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
        private static string FaceKey = "f4L1FGoF76RY9uARnO0A0tc3";
        private static string FaceSecretKey = "RmkhYuWGqf6IErW9ZcmQ99H7o3GSQaGK";
        private static string TokenUrl = "https://aip.baidubce.com/oauth/2.0/token";

        public static string getAccessToken()
        {
            string result = "";
            try
            {
                string encodeUrl = Utils.UriEncode(TokenUrl);
                HttpWebRequest request = WebRequest.Create(TokenUrl) as HttpWebRequest;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                //https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id=Va5yQRHlA4Fq5eR3LT0vuXV4&client_secret=0rDSjzQ20XUj5itV6WRtznPQSzr5pVw2&
                string postData = "grant_type=" + Utils.UriEncode("client_credentials") + "&client_id=" + Utils.UriEncode(FaceKey)
                    + "&client_secret=" + Utils.UriEncode(FaceSecretKey);
                var data = Encoding.UTF8.GetBytes(postData);
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
                //发送请求并获取相应回应数据  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                result = sr.ReadToEnd();
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);
                result = jsonObj["access_token"].ToString();
            }
            catch (Exception ex)
            {
                Logging.LogFile("BaiDuAuthent-getAccessToken:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 公安验证
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="name"></param>
        /// <param name="imgHead"></param>
        /// <returns></returns>
        public static string AuthenPliceFaceTwo(string idcard, string name,string Phone,string Nation,string Address, Stream imgHead)
        {
            string result = "0";
            try
            {
                
                string reqUrl = "http://121.42.164.134:9918/API/HealthCard/HealthCardGiter";
                string encodeUrl = Utils.UriEncode(reqUrl);
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
                //request.ContentType = "application/json;charset=utf-8";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                byte[] img = new byte[imgHead.Length];
                imgHead.Read(img, 0, img.Length);
                string postData = "Idno=" + idcard + "&Name=" + name
                    + "&Phone=" + Phone + "&Nation=" + Nation + "&Address=" + Address +
                    "&Base64=" + Utils.UriEncode(Convert.ToBase64String(img));
                var data = Encoding.UTF8.GetBytes(postData);
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
                //发送请求并获取相应回应数据  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream instream = response.GetResponseStream();
                result = "过程";
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                result = sr.ReadToEnd();
                //JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);
                //result = jsonObj["result"].ToString();


            }
            catch (Exception ex)
            {
                Logging.LogFile("BaiDuAuthent-AuthenPliceFaceTwo:" + ex.Message);
            }
            return result;
        }




        public static string AuthenPliceFaceTwos(string idcard, string name, Stream imgHead)
        {
            string result = "0";
            try
            {
                string token = getAccessToken();
                string reqUrl = "https://aip.baidubce.com/rest/2.0/face/v2/person/verify";
                reqUrl = "http://121.42.164.134:9918/API/HealthCard/HealthCardGiter";
                string encodeUrl = Utils.UriEncode(reqUrl);
                HttpWebRequest request = WebRequest.Create(reqUrl) as HttpWebRequest;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                byte[] img = new byte[imgHead.Length];
                imgHead.Read(img, 0, img.Length);
                string postData = "Idno=" + Utils.UriEncode(token) + "&id_card_number=" + Utils.UriEncode(idcard)
                    + "&name=" + Utils.UriEncode(name) + "&image=" + Utils.UriEncode(Convert.ToBase64String(img));
                var data = Encoding.UTF8.GetBytes(postData);
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
                //发送请求并获取相应回应数据  
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                result = sr.ReadToEnd();
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(result);
                result = jsonObj["result"].ToString();
            }
            catch (Exception ex)
            {
                Logging.LogFile("BaiDuAuthent-AuthenPliceFaceTwo:" + ex.Message);
            }
            return result;
        }
        #region MyRegion
        // 人脸查找一对N——识别
        public static string identify(Stream imgHead, string groupid)
        {
            string token = getAccessToken();
            string host = "https://aip.baidubce.com/rest/2.0/face/v2/identify?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;

            byte[] img = new byte[imgHead.Length];
            imgHead.Read(img, 0, img.Length);
            string base641 = Convert.ToBase64String(img);
            string base642 = Convert.ToBase64String(img);
            String str = "group_id=" + groupid + "&user_top_num=" + "1" + "&face_top_num=" + "1" + "&images=" + Utils.UriEncode(base641 + "," + base642);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();

            return result;
        }


        #region 人脸查找一对N   V3
        public static string search(Stream imgHead, string groupid)
        {
            string token = getAccessToken();
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/search?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;

            byte[] img = new byte[imgHead.Length];
            imgHead.Read(img, 0, img.Length);
            string base641 = Convert.ToBase64String(img);
            string base642 = Convert.ToBase64String(img);

            String str = "{\"image\":\"" + base641 + "\",\"image_type\":\"BASE64\",\"group_id_list\":\"" + groupid + "\",\"max_user_num\":\"1\"}";
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();

            return result;
        }
        #endregion
        #endregion

        // 人脸认证 一对一识别
        public static string verify(Stream imgHead, string uid)
        {
            string token = getAccessToken(); ;
            string host = "https://aip.baidubce.com/rest/2.0/face/v2/verify?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            byte[] img = new byte[imgHead.Length];
            imgHead.Read(img, 0, img.Length);
            string base641 = Convert.ToBase64String(img);
            string base642 = Convert.ToBase64String(img);
            String str = "uid=" + uid + "&top_num=" + 1 + "&images=" + Utils.UriEncode(base641 + "," + base642);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            return result;
        }
        public static string UidNameverify(string uid, string username)
        {
            string token = getAccessToken(); ;
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/person/idmatch?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            String str = "id_card_number=" + uid + "&name=" + username;
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            return result;
        }

        /// <summary>
        /// 用户人脸注册
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="groupid"></param>
        /// <param name="faceImg"></param>
        /// <returns></returns>
        public static string RegUserFace(string uid, string userInfo, string groupid, byte[] faceImg)
        {
            string result = null;
            try
            {
                string token = getAccessToken();
                string host = "https://aip.baidubce.com/rest/2.0/face/v2/faceset/user/add?access_token=" + token;
                //host = "https://aip.baidubce.com/rest/2.0/face/v3/faceset/user/add?access_token=" + token;
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
                request.Method = "post";
                request.KeepAlive = true;
                string base64Img = Convert.ToBase64String(faceImg);
                String str = "uid=" + uid + "&user_info=" + userInfo + "&group_id=" + groupid
                    + "&images=" + Utils.UriEncode(base64Img);
                byte[] buffer = encoding.GetBytes(str);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                Logging.LogFile("BaiDuAuthent-RegUserFace:" + ex.Message);
            }
            return result;

        }

        public static string getList()
        {
            string token = getAccessToken();
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/faceset/face/getlist?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            String str = "{\"user_id\":\"432524200009243815\",\"group_id\":\"xtdygroup\"}";
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("获取用户人脸列表:");
            Console.WriteLine(result);
            return result;
        }

    }
}