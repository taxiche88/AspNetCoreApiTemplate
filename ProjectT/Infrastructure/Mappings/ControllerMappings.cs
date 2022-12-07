using AutoMapper;
using ProjectT.Dtos.Parameter;
using ProjectT.Dtos.ViewModel;
using ProjectT.Service.Dtos.Info;
using ProjectT.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectT.Infrastructure.Mappings
{
    /// <summary>
    /// automapper轉換
    /// </summary>
    public class ControllerMappings : Profile
    {
        /// <summary>
        /// automapper轉換
        /// </summary>
        public ControllerMappings()
        {
            // Parameter -> Info
            this.CreateMap<CardParameter, CardInfo>();
            this.CreateMap<CardSearchParameter, CardSearchInfo>();

            // ResultModel -> ViewModel
            this.CreateMap<CardResultModel, CardViewModel>();
        }
    }
}
