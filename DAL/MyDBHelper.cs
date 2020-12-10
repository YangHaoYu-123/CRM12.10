
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data;
using Newtonsoft.Json;

namespace DAL
{
    public class MyDBHelper
    {
        //实例化(目的是 获取数据库的连接字符串)
        private readonly IConfiguration configuration;
        public MyDBHelper(IConfiguration configurations)
        {
            this.configuration = configurations;
        }

       
        //添加(删除,修改)
        public int Add<T>(string sql) where T:class,new()
        {
            //连接数据库字符串
            string strsql = configuration.GetSection("sqlstr").Value;
            using (SqlConnection sqlconntion=new SqlConnection(strsql))
            {
                sqlconntion.Open();
                using (SqlCommand command=new SqlCommand(sql,sqlconntion))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }


        //显示(绑定下拉)
        public List<T> Show<T>(string sql) where T:class,new()
        {
            //数据表格
            DataTable dataTable = new DataTable();
            //连接数据库字符串
            string strsql = configuration.GetSection("sqlstr").Value;
            using (SqlConnection sqlconntion = new SqlConnection(strsql))
            {
                sqlconntion.Open();
                using (SqlCommand command = new SqlCommand(sql, sqlconntion))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            string s = JsonConvert.SerializeObject(dataTable);
            return JsonConvert.DeserializeObject<List<T>>(s);
        }


        //反填
        public T FanTian<T>(string sql) where T : class, new()
        {
            List<T> list = Show<T>(sql);
            if (list.Count > 0)
            {
                return Show<T>(sql)[0];
            }
            else
            {
                return default(T);
            }
        }


    }
}
