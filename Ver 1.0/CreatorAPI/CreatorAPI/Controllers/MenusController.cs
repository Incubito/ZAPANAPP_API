using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using System.Data.SqlClient;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Menus")]
    public class MenusController : ApiController
    {
        [Authorize]
        [Route("MenusList")]
        public IEnumerable<MenusModels> PostMenuList ([FromHeader]string LastDownloaded)
        {
            List<MenusModels> ListOfMenus = new List<MenusModels>();

            SqlConnection sqlconn = new SqlConnection(CreatorAPI.Properties.Settings.Default.SQLConn);
            SqlCommand sqlcmd;
            SqlDataReader sqlrdr;

            sqlconn.Open();
            sqlcmd = sqlconn.CreateCommand();
            sqlcmd.CommandText = "MOBILE_RetrieveMenulist";
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(@"@Updated", System.Data.SqlDbType.VarChar).Value = LastDownloaded;
            sqlrdr = sqlcmd.ExecuteReader();

            if (sqlrdr.HasRows)
            {
                while (sqlrdr.Read())
                {
                    MenusModels Menu = new MenusModels();
                    Menu.MenuID = sqlrdr["ID"].ToString();
                    Menu.MenuName = sqlrdr["Name"].ToString();
                    Menu.MenuColour = sqlrdr["Colour"].ToString();
                    Menu.MenuIcon = sqlrdr["Icon"].ToString();
                    Menu.MenuUpdated = sqlrdr["Updated"].ToString();

                    ListOfMenus.Add(Menu);
                }
            }

            sqlrdr.Close();
            sqlconn.Close();

            return ListOfMenus;
        }
    }
}
