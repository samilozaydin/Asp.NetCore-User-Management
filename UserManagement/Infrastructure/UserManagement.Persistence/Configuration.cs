using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace UserManagement.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get {
                ConfigurationManager manager = new ConfigurationManager();
                manager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"../../Presantation/UserManagement.Presantation"));
                manager.AddJsonFile("appsettings.json");
                
                return manager.GetConnectionString("ConnectionString");
            }
        }
    }
}
