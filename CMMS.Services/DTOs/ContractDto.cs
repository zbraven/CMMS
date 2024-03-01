using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Terms { get; set; }
        public int EmployeeId { get; set; }
    }
}
