using Machine.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceFaces
{
    /// <summary>
    /// 操作设备
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Activation : System.Web.Services.WebService
    {

        /// <summary>
        /// 激活设备接口
        /// </summary>
        /// <param name="PMHospName">医院名称</param>
        /// <param name="cpu">cpu编号</param>
        /// <param name="PMDep">科室名称</param>
        /// <param name="DepPosition">科室位置</param>
        /// <returns></returns>
        [WebMethod]
        public String EquipActiva(String PMHospName, String cpu, String PMDep, String DepPosition)
        {
            String Result = "";
            try
            {
                ActivationBLL bll = new ActivationBLL();
                Object msg = bll.Act(PMHospName, PMDep, DepPosition, cpu);
                Result = "{\"Code\" : 1 ,\"msg\" : \"" + msg + "\"}";
                return Result;
            }
            catch (Exception)
            {
                return Result = "{\"Code\" : -1 ,\"msg\" : \"接口报错\"}";
            }
        }
        #region 调用例子
        //String txt = a.EquipActiva("湘潭", "123asdfg", "asfghb", "SDFGH");
        //JObject jo = (JObject)JsonConvert.DeserializeObject(txt);
        //string code = jo["Code"].ToString();
        //string msg = jo["msg"].ToString(); 
        #endregion


        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="cpu"></param>
        /// <returns></returns>
        [WebMethod]
        public String DelActiva(String cpu)
        {
            String Result = "";
            try
            {
                ActivationBLL bll = new ActivationBLL();
                int msg = bll.del(cpu);
                Result = "{\"Code\" : \"" + msg + "\" ,\"msg\" : \"\"}";
                return Result;
            }
            catch (Exception)
            {
                return Result = "{\"Code\" : -1 ,\"msg\" : \"接口报错\"}";
            }

        }

    }
}
