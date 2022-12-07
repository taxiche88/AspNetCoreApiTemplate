using AutoMapper;
using ProjectT.Repository.Dtos.Condition;
using ProjectT.Repository.Dtos.DataModel;
using ProjectT.Repository.Impletement;
using ProjectT.Repository.Interface;
using ProjectT.Service.Dtos.Info;
using ProjectT.Service.Dtos.ResultModel;
using ProjectT.Service.Infrastructure.Mappings;
using ProjectT.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectT.Service.Impletement
{
    /// <summary>
    /// 卡片管理
    /// </summary>
    /// <seealso cref="ProjectT.Service.Interface.ICardService" />
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        private readonly ICardTypeRepository _cardTypeRepository;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardService(IMapper mapper, ICardRepository cardRepository, ICardTypeRepository cardTypeRepository)
        {
            this._mapper = mapper;
            this._cardRepository = cardRepository;
            this._cardTypeRepository = cardTypeRepository;
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var result = this._cardRepository.Delete(id);
            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public CardResultModel Get(int id)
        {
            var card = this._cardRepository.Get(id);
            var result = this._mapper.Map<CardDataModel, CardResultModel>(card);

            // 利用邏輯運算來抓取另一個TABLE的資料做資料整合, 而不是在repository的層面做兩個TABLE的join回傳整理後的資料
            // 好處是保持repository的乾淨/可重複使用率, 壞處是效能低
            // 因此要使用哪一種方法實作, 要看當時使用情境
            var cardType = this._cardTypeRepository.Get(result.CardType);
            result.CardTypeName = cardType.Name;

            return result;
        }

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<CardResultModel> GetList(CardSearchInfo info)
        {
            var condition = this._mapper.Map<CardSearchInfo, CardSearchCondition>(info);
            var data = this._cardRepository.GetList(condition);
            var results = this._mapper.Map<IEnumerable<CardDataModel>, IEnumerable<CardResultModel>>(data);

            // 利用邏輯運算來抓取另一個TABLE的資料做資料整合, 而不是在repository的層面做兩個TABLE的join回傳整理後的資料
            // 好處是保持repository的乾淨/可重複使用率, 壞處是效能低
            // 因此要使用哪一種方法實作, 要看當時使用情境
            var listResults = results.ToList();
            var cardTypes = this._cardTypeRepository.GetList();
            listResults.ForEach(x => x.CardTypeName = cardTypes.Where(y => y.Card_Type == x.CardType).Select(z => z.Name).FirstOrDefault());

            return listResults;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Insert(CardInfo info)
        {
            var condition = this._mapper.Map<CardInfo, CardCondition>(info);

            // 給予種類預設值
            condition.Card_Type = "ATT";

            var result = this._cardRepository.Insert(condition);

            // 將insert回傳數字轉換成pass/fail
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Update(int id, CardInfo info)
        {
            var condition = this._mapper.Map<CardInfo, CardUpdateCondition>(info);
            var result = this._cardRepository.Update(id, condition);
            return result;
        }
    }
}
