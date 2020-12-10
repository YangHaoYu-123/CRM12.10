using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 信息表(主表)
    /// </summary>
    public class XinXiTable
    {
        public int XId { get; set; }
        public string XJingBanRen { get; set; }
        public decimal XNianFei { get; set; }
        public decimal XDZJingFei { get; set; }
        public DateTime XCaiGouDataTime { get; set; }
        public int XGId { get; set; }
        public int XCJId { get; set; }
        public int XCFId { get; set; }
        public int XCTId { get; set; }
        public int XFQ { get; set; }
        public int XSZ { get; set; }
        public int XGZId { get; set; }
        public int XJieDuanId { get; set; }

        public bool ShangJiTiXiang { get; set; }
        public string BeiZhu { get; set; }
        public string LName { get; set; }
        public string LPhone { get; set; }
    }
}
