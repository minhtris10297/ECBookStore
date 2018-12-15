using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeStore.Areas.AdminArea.Controllers
{
    public class DuLieuController : BaseController
    {
        // GET: AdminArea/DuLieu
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BackUpData(string TenSaoLuu)
        {
            //SqlConnection sqlConnection = db.Database.Connection.Open();
            //string exec = "use KnowledgeStore exec BackUpDatabaseBUD";
            //SqlCommand sqlCommand = new SqlCommand(exec, );
            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();
            //return View("Index");

            using (SqlConnection con = new SqlConnection("data source=.;initial catalog=KnowledgeStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"use KnowledgeStore exec BackUpDatabaseBUD '" + TenSaoLuu + "'" ;
                    cmd.ExecuteNonQuery();
                }
            }
            return View("Index");
        }
    }
}