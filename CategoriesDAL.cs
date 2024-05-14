using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Model;
using System.Data;
using MySql.Data.MySqlClient;

namespace BookShop.DAL
{
   public static class CategoriesDAL
    {
        ///<summary>
        ///获取所有可用的图书种类
        /// </summary>
        /// <return>list<CategoryInfo></return>
        public static List<CategoryInfo> GetCategories()
        {
            //定义查询语句
            string strsql = "select * from Categories where DelFlag=0";
            //定义一个Categoryinfo类型的list
            List<CategoryInfo> list = new List<CategoryInfo>();
            try
            {
                //定义一个datatable对象，并调用mysqlhelper的ExcuteDatatable方法赋值。
                DataTable dt = MysqlHelper.ExcuteDataTable(strsql);
                //遍历这个datatable对象，每一行对应一个CategoryInfo对象，每读出一行就对一个CategoryInfo属性赋值。然后添加到list中。
                foreach (DataRow dr in dt.Rows)
                {
                    CategoryInfo cate = new CategoryInfo();
                    cate.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cate.CategoryName = dr["CategoryName"].ToString();
                    list.Add(cate);
                }
            }
            catch (Exception)
            {
                throw;
            }
            //返回这个list
            return list;
        }
        /// <summary>
        /// 通过id获取一条记录
        /// </summary>
        /// <param name="cateid"></param>
        /// <returns></returns>
        public static CategoryInfo GetCategorybyID(int cateid)
        {
            string strsql = "select * from Categories where CategoryID=@CategoryID";
            MySqlParameter pm = new MySqlParameter("@CategoryID", MySqlDbType.Int32) { Value = cateid };
            try
            {
                DataTable dt = MysqlHelper.ExcuteDataTable(strsql,pm);
                CategoryInfo cate = new CategoryInfo();
                cate.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
                cate.CategoryName = dt.Rows[0]["CategoryName"].ToString();
                return cate;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        /// <summary>
        /// 更新图书种类信息
        /// </summary>
        /// <param name="cate"></param>
        /// <returns></returns>
        public static int UpdateCategory(CategoryInfo category)
        {
            string strsql = "update categories set CategoryName=@CategoryName where CategoryID=@CategoryID";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("@CategoryID",category.CategoryID),
                new MySqlParameter("@CategoryName",category.CategoryName)
            };
            int i = MysqlHelper.ExcuteNonQuery(strsql,pms);
            return i;
        }
        ///<summary>
        ///删除图书种类（DelFlag更新为1）
        /// </summary>
        /// <return>int</return>
        /// 
        public static int DelCategory(int categoryid)
        {
            string strsql = "update categories set DelFlag=1 where CategoryID=@categoryid";
            MySqlParameter pms= new MySqlParameter("@categoryid", MySqlDbType.Int32) { Value= categoryid };
            int DelFlag = MysqlHelper.ExcuteNonQuery(strsql, pms);
            
            return DelFlag;
        }
    }
}
