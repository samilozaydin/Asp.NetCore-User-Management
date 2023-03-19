using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities.Common;

namespace UserManagement.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public int RegionId { get; set; }
        public ICollection <Location> Locations { get; set; }
        public Region Region { get; set; }
    }
}
