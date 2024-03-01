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
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            // Contract entity'sinden ContractDto'ya ve tersi dönüşüm
            CreateMap<Contract, ContractDto>().ReverseMap();
        }
    }
}
