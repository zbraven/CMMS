using CMMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Services.DTOs
{
    public class AssetTypeDistributionDto
    {
        public AssetType AssetType { get; set; }
        public int Count { get; set; }
    }
}
