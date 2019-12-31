using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversitycourseResultManagementSystem.GateWay
{
    public class GateWay
    {
        private string connectingstring =
         WebConfigurationManager.ConnectionStrings["BitmuniversityWebAppContext"].ConnectionString;

        public SqlConnection Connection { get; set; }
        public SqlDataReader Reader { get; set; }
        public SqlCommand Command { get; set; }
        public string Query { get; set; }
        public GateWay()
        {
            Connection = new SqlConnection(connectingstring);
        }
    }
}