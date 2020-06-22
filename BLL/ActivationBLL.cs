using Machine.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Machine.BLL
{
    public class ActivationBLL
    {
        /// <summary>
        /// 激活设备
        /// </summary>
        /// <param name="PMHospName">医院名称</param>
        /// <param name="PMDep">科室名称</param>
        /// <param name="DepPosition">科室位置</param>
        /// <param name="cpu">CPU编号</param>
        /// <returns></returns>
        public Object Act(String PMHospName, String PMDep, String DepPosition, String cpu)
        {
            ActivationDAL dal = new ActivationDAL();
            return dal.Act(PMHospName, PMDep, DepPosition, cpu);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="cpu"></param>
        /// <returns></returns>
        public int del(String cpu)
        {
            ActivationDAL dal = new ActivationDAL();
            return dal.Del(cpu);
        }
    }
}
