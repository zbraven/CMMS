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
    public class TaskMaterialProfile : Profile
    {
        public TaskMaterialProfile()
        {
            // TaskMaterial entity'sinden TaskMaterialDto'ya ve tersi dönüşüm
            CreateMap<TaskMaterial, TaskMaterialDto>().ReverseMap();
        }
    }
}
