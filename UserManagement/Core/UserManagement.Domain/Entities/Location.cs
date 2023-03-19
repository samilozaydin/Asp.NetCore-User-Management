using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.Common;

namespace UserManagement.Domain.Entities
{
    public class Location: BaseEntity
    {
        public string StreetAddress { get;set;}
        public int PostalCode { get;set;}
        public string City { get;set;}
        public string StateProvince { get;set;}
        public int CountryId { get;set;}
        public ICollection<Department> Departments { get;set;}
        public Country Country { get;set;}
    }
}
