using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityMS.DAL
{
    public class ConnectionString
    {
        public string connectionString = WebConfigurationManager.ConnectionStrings["BitmuniversityWebAppContext"].ToString();
    }
}