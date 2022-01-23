using AutoMapper;
using Blog.Repositories.Abstractions.Entities;
using Blog.Services.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Extensions
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<ArticleModel, ArticleEntities>()
                .ReverseMap();
        }
    }
}
