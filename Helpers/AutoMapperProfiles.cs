using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ezCloth.DTOs;
using ezCloth.Entities;

namespace ezCloth.Helpers
{
    // Just install AutoMapper Dependecy Injections

    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LoginDto, SystemUsers>();

        }
    }
}
