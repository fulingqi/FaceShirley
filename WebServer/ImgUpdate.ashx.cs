using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServer
{
    /// <summary>
    /// ImgUpdate 的摘要说明
    /// </summary>
    public class ImgUpdate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string path = String.Empty;
            //string filename = context.Request.QueryString["filename"];
            String sfz = context.Request.QueryString["sfz"];
            String yy = context.Request.QueryString["yy"];
            string dir = context.Server.MapPath("~/Imgs");
            if (!Directory.Exists(dir))//文件夹不存在    
            {
                Directory.CreateDirectory(dir);//创建文件夹    
            }
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {

                    path = System.IO.Path.Combine(dir, System.IO.Path.GetFileName(sfz + ".jpg"));
                    if (File.Exists(System.IO.Path.GetFullPath(path)))
                    {
                        File.Delete(System.IO.Path.GetFullPath(".\\") + sfz + ".jpg");
                    }
                    files[i].SaveAs(path);
                }
                System.Net.WebClient myWebClient = new System.Net.WebClient();
                myWebClient.UploadFile("http://121.42.164.134:998/img.ashx?sfz=" + sfz + "&yy=xtdygroup", "POST", path);
                //myWebClient.UploadFile("http://localhost:12022/img.ashx?sfz=" + sfz + "&yy=xtdygroup", "POST", path);
                File.Delete(System.IO.Path.GetFullPath(path));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}