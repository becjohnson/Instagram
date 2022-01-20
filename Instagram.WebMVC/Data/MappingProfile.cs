using AutoMapper;
using Instagram.Service;
using Instagram.WebMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.WebMVC.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostService, PostController>();
            CreateMap<PostController, PostService>();
        }
    }
}
