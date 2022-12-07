using AutoMapper;
using ProjectT.Repository.Dtos.Condition;
using ProjectT.Repository.Dtos.DataModel;
using ProjectT.Service.Dtos.Info;
using ProjectT.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Service.Infrastructure.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // 如果不是一對一轉換, 而是其他手法ex: int -> string, 請詳細敘述轉換的內容, 以提醒日後的你

            // Info -> Condition
            this.CreateMap<CardInfo, CardCondition>()
                .ForMember(to => to.Card_Type, from => from.Ignore());
            this.CreateMap<CardInfo, CardUpdateCondition>();
            this.CreateMap<CardSearchInfo, CardSearchCondition>();

            // DataModel -> ResultModel
            this.CreateMap<CardDataModel, CardResultModel>()
                .ForMember(to => to.CardType, from => from.MapFrom(elm => elm.Card_Type))
                .ForMember(to => to.CardTypeName, from => from.Ignore());
        }
    }
}