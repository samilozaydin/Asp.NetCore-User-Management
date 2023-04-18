using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTOs
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
