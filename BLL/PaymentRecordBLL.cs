using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PaymentRecordBLL
    {
        /// <summary>
        /// 添加缴费记录
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public int AddPaymentRecord(String HospCode, String HisOrderNum, String Billnumber, double Billmoney, String Source, int UserId, int Payment, String Sourceaccount)
        {
            PaymentRecordDAL bll = new PaymentRecordDAL();
            return bll.AddPaymentRecord(HospCode, HisOrderNum, Billnumber, Billmoney, Source, UserId, Payment, Sourceaccount);
        }
    }
}
