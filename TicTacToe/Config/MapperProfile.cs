using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BL.Models;
using TicTacToe.WebApi.Models;

namespace TicTacToe.WebApi.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserBL>()
                //.ForMember(u => u.Id, u2 => u2.MapFrom(u3 => u3.Id))
                .ForMember(u => u.Name, u2 => u2.MapFrom(u3 => u3.Name))
                .ForMember(u => u.Email, u2 => u2.MapFrom(u3 => u3.Email))
                .ForMember(u => u.Password, u2 => u2.MapFrom(u3 => u3.Password));

            CreateMap<UserBL, UserModel>()
                .ForMember(u => u.Id, u2 => u2.MapFrom(u3 => u3.Id))
                .ForMember(u => u.Name, u2 => u2.MapFrom(u3 => u3.Name))
                .ForMember(u => u.Email, u2 => u2.MapFrom(u3 => u3.Email));
            //.ForMember(u => u.Password, u2 => u2.MapFrom(u3 => u3.Password));
            CreateMap<AuthUserModelBL, AuthUserModel>();

            CreateMap<GameHistoryModel, GameHistoryBL>()
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => g3.GameId))
                .ForMember(g => g.PlayerId, g2 => g2.MapFrom(g3 => g3.PlayerId))
                .ForMember(g => g.IsBot, g2 => g2.MapFrom(g3 => g3.IsBot))
                .ForMember(g => g.XAxis, g2 => g2.MapFrom(g3 => g3.XAxis))
                .ForMember(g => g.YAxis, g2 => g2.MapFrom(g3 => g3.YAxis))
                .ForMember(g => g.MoveDate, g2 => g2.MapFrom(g3 => g3.MoveDate ?? DateTime.Now));

            CreateMap<GameHistoryBL, GameHistoryModel>()
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => g3.GameId))
                .ForMember(g => g.PlayerId, g2 => g2.MapFrom(g3 => g3.PlayerId))
                .ForMember(g => g.IsBot, g2 => g2.MapFrom(g3 => g3.IsBot))
                .ForMember(g => g.XAxis, g2 => g2.MapFrom(g3 => g3.XAxis))
                .ForMember(g => g.YAxis, g2 => g2.MapFrom(g3 => g3.YAxis))
                .ForMember(g => g.MoveDate, g2 => g2.MapFrom(g3 => g3.MoveDate));

            CreateMap<GameBL, GameModel>();
            CreateMap<GameResultBL, GameResultModel>();
            CreateMap<UserGamesStatisticBL, UserGamesStatisticModel>();
        }
    }
}
