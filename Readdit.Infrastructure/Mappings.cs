using AutoMapper;
using Readdit.Domain.Models;
using Readdit.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Readdit.Infrastructure
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Link, LinkDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count()));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<SubReaddit, SubReadditDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
