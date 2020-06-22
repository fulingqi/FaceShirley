using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Machine.DLL
{
    /// <summary>
    /// 激活程序
    /// </summary>
    public class ActivationDAL
    {
        /// <summary>
        /// 设备激活
        /// </summary>
        /// <param name="PMHospName">医院名称</param>
        /// <param name="PMDep">科室名称</param>
        /// <param name="DepPosition">科室位置</param>
        /// <param name="cpu">CPU编号</param>
        /// <returns></returns>
        public Object Act(String PMHospName, String PMDep, String DepPosition, String cpu)
        {
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@PMHospName", SqlDbType.NVarChar,100),
                new SqlParameter("@PMDep", SqlDbType.NVarChar,100),
                new SqlParameter("@DepPosition", SqlDbType.NVarChar,100),
                new SqlParameter("@cpu", SqlDbType.NVarChar,100)
            };
            parms[0].Value = PMHospName;
            parms[1].Value = PMDep;
            parms[2].Value = DepPosition;
            parms[3].Value = cpu;
            Object dta = SQLHelper.EXECUTE_SCALAR("procAct", CommandType.StoredProcedure, parms);
            return dta;
        }

        /// <summary>
        /// 删除设备信息
        /// </summary>
        /// <param name="cpu"></param>
        /// <returns></returns>
        public int Del(String cpu)
        {
            int row = 0;
            string sSQL = "delete PointMainten  where CPU='" + cpu + "'";
            try
            {
                row = SQLHelper.EXECUTE_NONQUERY(sSQL);
            }
            catch (Exception ex)
            {

            }
            return row;
        }
    }
}
