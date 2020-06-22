using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class UserInfoModel
    {
        public class UserInfo
        {
            public Int32 id { get; set; }

            public String SecretKey { get; set; }

            public String LoginName { get; set; }

            public String LoginPass { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            public String Sex { get; set; }

            public String RealName { get; set; }

            public String OsoPerson { get; set; }

            public String OsoPhone { get; set; }

            public String BirthDate { get; set; }

            /// <summary>
            /// 身份证号码
            /// </summary>
            public String IDno { get; set; }

            public Int32 CardType { get; set; }

            public String Nation { get; set; }

            /// <summary>
            /// Email
            /// </summary>
            public String Email { get; set; }

            public String Img { get; set; }

            public String MedicareNo { get; set; }

            /// <summary>
            /// 手机号
            /// </summary>
            public String Phone { get; set; }

            public Int32 VerifyFlag { get; set; }

            public String VerifyReason { get; set; }

            public String SchoolCode { get; set; }

            public String StudentNo { get; set; }

            public String SchoolName { get; set; }

            public DateTime InsertDate { get; set; }

            /// <summary>
            /// 家庭地址
            /// </summary>
            public String Address { get; set; }

            public String FaceImg { get; set; }

            public String huanzheid { get; set; }
            public String Healthcard { get; set; }
        }
    }
}
