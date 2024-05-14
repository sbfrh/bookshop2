using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookShop.DAL
{
   public static class BooksDAL
    {
        /// <summary>
        /// 获取图书列表
        /// </summary>
        /// <param name="cateid"></param>
        /// <returns>list</returns>
        public static List<BookInfo> GetBookList(int cateid)
        {
            //定义带参数的查询语句
            string sql = "select * from books where CategoryId=@id";
            //定义一个MySqlParameter参数并赋值。
            MySqlParameter pm = new MySqlParameter("@id", MySqlDbType.Int32) { Value = cateid };
            List<BookInfo> list = new List<BookInfo>();
            try
            {
                DataTable dt = MysqlHelper.ExcuteDataTable(sql, pm);
                foreach (DataRow dr in dt.Rows)
                {
                    BookInfo bookInfo = new BookInfo();
                    bookInfo.BookID = Convert.ToInt32(dr["BookID"]);
                    bookInfo.BookAuthor = dr["BookAuthor"].ToString();
                    bookInfo.BookTitle = dr["BookTitle"].ToString();
                    bookInfo.ImgUrl = dr["ImgUrl"].ToString();
                    bookInfo.PublishDate = DateTime.Parse(dr["PublishDate"].ToString());
                    bookInfo.UnitPrice = decimal.Parse(dr["UnitPrice"].ToString());
                    bookInfo.ISBN = dr["ISBN"].ToString();
                    bookInfo.Clicks = Convert.ToInt32(dr["Clicks"]);
                    list.Add(bookInfo);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
    }
}
