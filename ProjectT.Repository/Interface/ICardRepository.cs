using ProjectT.Repository.Dtos.Condition;
using ProjectT.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Repository.Interface
{
    /// <summary>
    /// 卡片管理服務
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <param name="condition">卡片查詢參數</param>
        /// <returns></returns>
        IEnumerable<CardDataModel> GetList(CardSearchCondition condition);

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>   
        CardDataModel Get(int id);

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="condition">卡片參數</param>
        /// <returns></returns>
        int Insert(CardCondition condition);

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="condition">卡片參數</param>
        /// <returns></returns>
        bool Update(int id, CardUpdateCondition condition);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
