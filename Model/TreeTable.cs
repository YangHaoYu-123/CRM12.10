using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 树形导航
    /// </summary>
    public class TreeTable
    {
        [Key]
        public int ShuId { get; set; }
        public string ShuName { get; set; }
        public string Url { get; set; }
        public int SZId { get; set; }
    }
}
