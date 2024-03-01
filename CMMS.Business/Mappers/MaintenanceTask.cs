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
    public class MaintenanceTaskProfile : Profile
    {
        public MaintenanceTaskProfile()
        {
            CreateMap<MaintenanceTask, MaintenanceTaskDto>().ReverseMap();
        }
    }
}
