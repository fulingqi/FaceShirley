using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PaymentRecordDAL
    {
        /// <summary>
        /// 添加缴费记录
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public int AddPaymentRecord(String HospCode, String HisOrderNum, String Billnumber, double Billmoney, String Source, int UserId, int Payment, String Sourceaccount)
        {
            int row = 0;
            string sSQL = "Insert Into PaymentRecord(HospCode,HisOrderNumber,Billnumber,Billmoney,Source,UserId,Payment,Sourceaccount)" +
                          "Values('" + HospCode + "','" + HisOrderNum + "','" + Billnumber + "'," + Billmoney + ",'" + Source + "'," + UserId + "," + Payment + ",'" + Sourceaccount + "')";
            try
            {
                row = Core.SQLHelper.EXECUTE_NONQUERY(sSQL);
            }
            catch (Exception ex)
            {
                Core.Logging.LogFile("插入数据失败[AddUser],原因:" + ex.Message);
            }
            return row;
        }
    }
}
