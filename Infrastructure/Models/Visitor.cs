using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int PurposeId { get; set; }
        public string HostName { get; set; }
        public int OfficeLocationId { get; set; }
        public int StaffId { get; set; }
        public int VisitorPassCode { get; set; }
        public DateTime VisitDate { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
       
    }

}
