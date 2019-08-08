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
    [RoutePrefix("api/SubMenus")]
    public class SubMenusController : ApiController
    {
        [Authorize]
        [Route("SubMenusList")]
        public IEnumerable<SubMenusModels> PostSubMenuList([FromHeader]string LastDownloaded)
        {
            List<SubMenusModels> ListOfSubMenus = new List<SubMenusModels>();

            SqlConnection sqlconn = new SqlConnection(CreatorAPI.Properties.Settings.Default.SQLConn);
            SqlCommand sqlcmd;
            SqlDataReader sqlrdr;

            sqlconn.Open();
            sqlcmd = sqlconn.CreateCommand();
            sqlcmd.CommandText = "MOBILE_RetrieveSubMenulist";
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(@"@Updated", System.Data.SqlDbType.VarChar).Value = LastDownloaded;
            sqlrdr = sqlcmd.ExecuteReader();

            if (sqlrdr.HasRows)
            {
                while (sqlrdr.Read())
                {
                    SubMenusModels SubMenu = new SubMenusModels();

                    SubMenu.SubMenuID = sqlrdr["ID"].ToString();
                    SubMenu.SubMenuMenuID = sqlrdr["MenuID"].ToString();
                    SubMenu.SubMenuName = sqlrdr["Name"].ToString();
                    SubMenu.SubMenuImage = sqlrdr["Image"].ToString();
                    SubMenu.SubMenuUpdated = sqlrdr["Updated"].ToString();

                    ListOfSubMenus.Add(SubMenu);
                }
            }

            sqlrdr.Close();
            sqlconn.Close();

            return ListOfSubMenus;
        }
    }
}
