using System;
using System.Collections.Generic;

namespace DAL
{
    public class MyDal
    {
        //实例化db
        private readonly MyDBHelper db;

        public MyDal(MyDBHelper myDBHelper)
        {
            this.db = myDBHelper;
        }


        //添加(删除,修改)
        public int Add<T>(string sql) where T : class, new()
        {
            return db.Add<T>(sql);
        }
        //显示(绑定下拉)
        public List<T> Show<T>(string sql) where T : class, new()
        {
            return db.Show<T>(sql);
        }
    }
}
