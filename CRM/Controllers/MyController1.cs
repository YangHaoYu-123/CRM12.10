using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;

namespace CRM.Controllers
{
    public class MyController1 : Controller
    {
        //实例化db
        private readonly MyBLL bLL;

        public MyController1(MyBLL bLL)
        {
            this.bLL = bLL;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/GetTree")]
        public IActionResult GetTree()
        {
            return View();
        }



        /// <summary>
        /// 获取所有树形数据
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/GetTrees")]
        public string GetTrees()
        {
            //linq获取数据
            var list = bLL.Show<TreeTable>("select * from TreeTable");
            //返回拼接好的父级和子级的字符串(Replace(替换符号))
            return "[" + GetTreeD(list, 0) + "]".Replace("},]", "}]");
        }


        /// <summary>
        /// 递归返回菜单(拼接字符串)
        /// </summary>
        /// <param name="viewModels"></param>
        /// <param name="SZId"></param>
        /// <returns></returns>
        private string GetTreeD(List<TreeTable> treeTables, int SZId)
        {
            //定义一个字符串(来拼接)
            var strjson = "";
            //初始化为空
            List<TreeTable> list = null;
            //判断外键等于0的(说明上面没有菜单了为父级)
            if (SZId == 0)
            {
                //查询出为父级的菜单
                list = (from s in treeTables where s.SZId == 0 select s).ToList();
            }
            else
            {
                //查询出为子级的菜单
                list = (from s in treeTables where s.SZId == SZId select s).ToList();
            }

            //循环父级和子级
            foreach (var item in list)
            {
                strjson += "{ShuId:" + item.ShuId + ",ShuName:\"" + item.ShuName + "\",SZId:" + item.SZId + ",Url:\"" + item.Url + "\",ZiJi:[" + GetTreeD(treeTables, item.ShuId) + "]},";
            }
            //返回拼接好的父级和子级的字符串
            return strjson;
        }

        //分布视图(通过地址跳到页面)
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/Add")]
        public PartialViewResult Add(string url)
        {
            return PartialView(url);
        }

        //添加信息数据
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("api/Adds")]
        public int Adds(XinXiTable x)
        {
            string s = string.Format("insert into XinXiTable values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",x.XJingBanRen,x.XNianFei,x.XDZJingFei,x.XCaiGouDataTime,x.XGId,x.XCJId,x.XCFId,x.XCTId,x.XFQ,x.XSZ,x.XGZId,x.XJieDuanId,x.ShangJiTiXiang,x.BeiZhu);
            return bLL.Add<XinXiTable>(s);
        }


        //获取公司信息
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/GongSiTable")]
        public JsonResult GongSiLit(string name)
        {
            string s = string.Format("select * from GongSiTable");
            var list = bLL.Show<GongSiTable>(s);
            GongSiTable slist = null;
            if (!string.IsNullOrEmpty(name))
            {
                slist = list.Where(x => x.GName.Contains(name)).FirstOrDefault();
            }
            return Json(slist);
        }
        //获取联系人
        //[Microsoft.AspNetCore.Mvc.HttpGet]
        //[Microsoft.AspNetCore.Mvc.Route("api/GetLianXiRen")]
        //public JsonResult GetLianXiRen()
        //{
        //    string s = string.Format("select * from XinXiTable");
        //    var list = bLL.Show<XinXiTable>(s);
        //    return Json(list);
        //}




        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/Getfang")]
        public JsonResult Getfang()
        {
            string sql = $"select * from CaiGouFangShiTable";
            return Json(bLL.Show<CaiGouFangShiTable>(sql));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/Getjibie")]
        public JsonResult Getjibie()//级别
        {
            string sql = $"select * from CaiGouJiBieTable";
            return Json(bLL.Show<CaiGouJiBieTable>(sql));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/GetTujing")]
        public JsonResult GetTujing()//途径
        {
            string sql = $"select * from CaiGouTuJingTable";
            return Json(bLL.Show<CaiGouTuJingTable>(sql));
        }
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/getgenjin")]
        public JsonResult getgenjin()//跟进
        {
            string sql = $"select * from GenJInZhuangTaiTable";
            return Json(bLL.Show<GenJInZhuangTaiTable>(sql));
        }



        //显示
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("api/GetShow")]
        public IActionResult GetShow()
        {
            var list = bLL.Show<XinXiTable>("select * from XinXiTable x join GongSiTable g on x.XGId=g.GId");
            return Ok(new { code = 0, msg = "", count = 1000, data = list });
        }


    }
}
