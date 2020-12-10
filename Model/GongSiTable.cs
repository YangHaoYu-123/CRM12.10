using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    /// <summary>
    /// 公司表
    /// </summary>
    public class GongSiTable
    {
        [Key]
        public int GId { get; set; }
        public string GName { get; set; }
        public string GDiZhi { get; set; }
        public string GBianMa { get; set; }
        public string GPhone { get; set; }
        public int GSanJiId { get; set; }
        public string GJiBie { get; set; }
        public string GDengJi { get; set; }
        public string GQuYu { get; set; }
    }
}
