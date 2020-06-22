using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
 public   class PointMaintenBLL
    {
        /// <summary>
        /// 获取设备是否启用
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetEnable(string PMHospName, int EquipmentNumber)
        {
            PointMaintenDAL pointMaintenDAL = new PointMaintenDAL();
            return pointMaintenDAL.GetEnable(PMHospName, EquipmentNumber);
        }
    }
}
