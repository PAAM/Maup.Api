using AutoMapper;
using Maup.Core.DTO;
using Maup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Mappers
{
    public class Autommapper : Profile
    {
        public Autommapper()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            //CreateMap<PropertyDto, Property>();

            CreateMap<PropertyImage, PropertyImageDto>();

            CreateMap<PropertyImageDto, PropertyImage>()
                .ForMember(i => i.File, options => options.Ignore());
        }
    }
}
