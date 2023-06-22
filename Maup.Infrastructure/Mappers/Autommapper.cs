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
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();

            CreateMap<PropertyImage, PropertyImageDto>()
            .ForMember(i => i.File, options => options.Ignore());
            CreateMap<PropertyImageDto, PropertyImage>();

            CreateMap<Owner, OwnerDto>()
                .ForMember(i => i.Photo, options => options.Ignore());
            CreateMap<OwnerDto, Owner>()
                .ForMember(i => i.Photo, options => options.Ignore());

          

        }
    }
}
