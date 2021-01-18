using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessModel = TicTacToe.BusinessComponent.Models;
using ApiModel = TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApiModel.User, BusinessModel.User>()
                //.ForMember(u => u.Id, u2 => u2.MapFrom(u3 => u3.Id))
                .ForMember(u => u.Name, u2 => u2.MapFrom(u3 => u3.Name))
                .ForMember(u => u.Email, u2 => u2.MapFrom(u3 => u3.Email))
                .ForMember(u => u.Password, u2 => u2.MapFrom(u3 => u3.Password));

            CreateMap<BusinessModel.User, ApiModel.User>()
                .ForMember(u => u.Id, u2 => u2.MapFrom(u3 => u3.Id))
                .ForMember(u => u.Name, u2 => u2.MapFrom(u3 => u3.Name))
                .ForMember(u => u.Email, u2 => u2.MapFrom(u3 => u3.Email));
            //.ForMember(u => u.Password, u2 => u2.MapFrom(u3 => u3.Password));
            CreateMap<BusinessModel.AuthUser, ApiModel.AuthUser>();

            CreateMap<ApiModel.GameHistory, BusinessModel.GameHistory>()
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => g3.GameId))
                .ForMember(g => g.PlayerId, g2 => g2.MapFrom(g3 => g3.PlayerId))
                //.ForMember(g => g.IsBot, g2 => g2.MapFrom(g3 => g3.IsBot))
                .ForMember(g => g.XAxis, g2 => g2.MapFrom(g3 => g3.XAxis))
                .ForMember(g => g.YAxis, g2 => g2.MapFrom(g3 => g3.YAxis))
                .ForMember(g => g.MoveDate, g2 => g2.MapFrom(g3 => g3.MoveDate ?? DateTime.Now));

            CreateMap<BusinessModel.GameHistory, ApiModel.GameHistory>()
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => g3.GameId))
                .ForMember(g => g.PlayerId, g2 => g2.MapFrom(g3 => g3.PlayerId))
                //.ForMember(g => g.IsBot, g2 => g2.MapFrom(g3 => g3.IsBot))
                .ForMember(g => g.XAxis, g2 => g2.MapFrom(g3 => g3.XAxis))
                .ForMember(g => g.YAxis, g2 => g2.MapFrom(g3 => g3.YAxis))
                .ForMember(g => g.MoveDate, g2 => g2.MapFrom(g3 => g3.MoveDate));

            CreateMap<BusinessModel.Game, ApiModel.Game>();
            CreateMap<BusinessModel.GameResult, ApiModel.GameResult>();
            CreateMap<BusinessModel.UserGamesStatistic, ApiModel.UserGamesStatistic>();
        }
    }
}
