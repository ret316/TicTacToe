using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.BL.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDL, UserBL>()
                .ForMember(u => u.Id, u2 => u2.MapFrom(u3 => u3.Id))
                .ForMember(u => u.Name, u2 => u2.MapFrom(u3 => u3.Name))
                .ForMember(u => u.Email, u2 => u2.MapFrom(u3 => u3.Email));

            CreateMap<GameHistoryDL, GameHistoryBL>();
            CreateMap<GameHistoryBL, GameHistoryDL>()
                .ForMember(g => g.Id, g2 => g2.MapFrom(g3 => Guid.NewGuid()))
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => g3.GameId))
                .ForMember(g => g.PlayerId, g2 => g2.MapFrom(g3 => g3.PlayerId))
                .ForMember(g => g.IsBot, g2 => g2.MapFrom(g3 => g3.IsBot))
                .ForMember(g => g.XAxis, g2 => g2.MapFrom(g3 => g3.XAxis))
                .ForMember(g => g.YAxis, g2 => g2.MapFrom(g3 => g3.YAxis))
                .ForMember(g => g.MoveDate, g2 => g2.MapFrom(g3 => g3.MoveDate));

            CreateMap<GameResultBL, GameResultDL>()
                .ForMember(h => h.Id, h2 => h2.MapFrom(h3 => Guid.NewGuid()))
                .ForMember(h => h.GameId, h2 => h2.MapFrom(h3 => h3.GameId))
                .ForMember(h => h.PlayerId, h2 => h2.MapFrom(h3 => h3.PlayerId))
                .ForMember(h => h.Result, h2 => h2.MapFrom(h3 => h3.Result));

            CreateMap<GameResultDL, GameResultBL>()
                .ForMember(h => h.GameId, h2 => h2.MapFrom(h3 => h3.GameId))
                .ForMember(h => h.PlayerId, h2 => h2.MapFrom(h3 => h3.PlayerId))
                .ForMember(h => h.Result, h2 => h2.MapFrom(h3 => h3.Result));

            CreateMap<GameBL, GameDL>()
                .ForMember(g => g.Id, g2 => g2.MapFrom(g3 => Guid.NewGuid()))
                .ForMember(g => g.GameId, g2 => g2.MapFrom(g3 => Guid.NewGuid()))
                .ForMember(g => g.Player1Id, g2 => g2.MapFrom(g3 => g3.Player1Id))
                .ForMember(g => g.Player2Id, g2 => g2.MapFrom(g3 => g3.Player2Id))
                .ForMember(g => g.IsPlayer2Bot, g2 => g2.MapFrom(g3 => g3.IsPlayer2Bot))
                .ForMember(g => g.IsGameFinished, g2 => g2.MapFrom(g3 => false));

            CreateMap<GameDL, GameBL>();
            CreateMap<UserGamesStatisticDL, UserGamesStatisticBL>();
        }
    }
}
