using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class MyBLL
    {
        //实例化db
        private readonly MyDal dal;

        public MyBLL(MyDal dal)
        {
            this.dal = dal;
        }


        //添加(删除,修改)
        public int Add<T>(string sql) where T : class, new()
        {
            return dal.Add<T>(sql);
        }
        //显示(绑定下拉)
        public List<T> Show<T>(string sql) where T : class, new()
        {
            return dal.Show<T>(sql);
        }
    }
}
