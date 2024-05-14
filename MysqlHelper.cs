using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace BookShop.DAL
{
  public static  class MysqlHelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
        //1.执行查询，返回第一行第一列：ExcuteScalar
        public static object ExcuteScalar(string sql, params MySqlParameter[] pms)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        //2.执行增删改：ExcuteNonQuery
        public static int ExcuteNonQuery(string sql, params MySqlParameter[] pms)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        //3.执行查询，返回多行多列：ExcuteReader
        public static MySqlDataReader ExcuteReader(string sql, params MySqlParameter[] pms)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader();
                }
                catch (Exception)
                {
                    conn.Close();
                    conn.Dispose();
                    throw;
                }
            }
        }

        //4.执行查询返回datatable
        public static DataTable ExcuteDataTable(string sql, params MySqlParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connStr))
            {
                if (pms != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                adapter.Fill(dt);
                return dt;
            }
        }


    }
}
