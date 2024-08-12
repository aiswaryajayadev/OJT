using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.DTO
{
    public class VisitorCreationDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PurposeOfVisitId { get; set; }
        public string PersonInContact { get; set; }
        public int OfficeLocationId { get; set; }
        
    }

}
