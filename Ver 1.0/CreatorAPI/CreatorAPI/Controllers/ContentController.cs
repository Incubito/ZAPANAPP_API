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
    [RoutePrefix("api/Content")]
    public class ContentController : ApiController
    {
        [Authorize]
        [Route("ContentList")]
        public IEnumerable<ContentModels> PostContentList([FromHeader]string LastDownloaded)
        {
            List<ContentModels> ListOfContent = new List<ContentModels>();

            SqlConnection sqlconn = new SqlConnection(CreatorAPI.Properties.Settings.Default.SQLConn);
            SqlCommand sqlcmd;
            SqlDataReader sqlrdr;

            sqlconn.Open();
            sqlcmd = sqlconn.CreateCommand();
            sqlcmd.CommandText = "MOBILE_RetrieveContentlist";
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(@"@Updated", System.Data.SqlDbType.VarChar).Value = LastDownloaded;
            sqlrdr = sqlcmd.ExecuteReader();

            if (sqlrdr.HasRows)
            {
                while (sqlrdr.Read())
                {
                    ContentModels Content = new ContentModels();

                    Content.ContentID = sqlrdr["ID"].ToString();
                    Content.ContentSubMenuID = sqlrdr["SubMenuID"].ToString();
                    Content.ContentDescription = sqlrdr["Description"].ToString();
                    Content.ContentType = sqlrdr["Type"].ToString();
                    Content.ContentCategory = sqlrdr["Category"].ToString();
                    Content.ContentName = sqlrdr["Name"].ToString();
                    Content.ContentUpdated = sqlrdr["Updated"].ToString();

                    ListOfContent.Add(Content);
                }
            }

            sqlrdr.Close();
            sqlconn.Close();

            return ListOfContent;
        }
    }
}
