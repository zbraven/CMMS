using AutoMapper;
using CMMS.Domain.Entities;
using CMMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMS.Business.Mappers
{
    public class ActivityLogProfile : Profile
    {
        public ActivityLogProfile()
        {
            // ActivityLog entity'sinden ActivityLogDto'ya ve tersi dönüşüm
            CreateMap<ActivityLog, ActivityLogDto>().ReverseMap();
        }
    }
}
