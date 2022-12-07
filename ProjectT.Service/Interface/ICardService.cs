using ProjectT.Service.Dtos.Info;
using ProjectT.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Service.Interface
{
    /// <summary>
    /// 卡片管理服務
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// /// <param name="info">卡片查詢參數</param>
        /// <returns></returns>
        List<CardResultModel> GetList(CardSearchInfo info);

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>   
        CardResultModel Get(int id);

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="info">卡片參數</param>
        /// <returns></returns>
        bool Insert(CardInfo info);

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="info">卡片參數</param>
        /// <returns></returns>
        bool Update(int id, CardInfo info);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
