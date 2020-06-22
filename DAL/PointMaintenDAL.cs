using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
   public class PointMaintenDAL
    {
        /// <summary>
        /// 获取设备是否启用
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public int GetEnable(string PMHospName, int EquipmentNumber)
        {
            int num = 0;
            string sql = "select * from UserInfo ";
            if (PMHospName != "")
            {
                sql += " Where PMHospName='" + PMHospName + "'AND EquipmentNumber=' "+EquipmentNumber+"'";
                num = 1;
            }
            DataTable dt = new DataTable();
            try
            {
                dt = Core.SQLHelper.QUERY_DATE_TABLE(sql);
                num = Convert.ToInt32(dt.Rows[0][8].ToString());
            }
            catch (Exception ex)
            {
                Core.Logging.LogFile("查询数据失败[GetEnable],原因:" + ex.Message);
            }
            return num;
        }
    }
}
