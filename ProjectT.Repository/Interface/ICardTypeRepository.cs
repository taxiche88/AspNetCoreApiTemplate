using ProjectT.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Repository.Interface
{
    /// <summary>
    /// 卡片種類, 僅限定查詢, 其他操作需透過DB修改
    /// </summary>
    public interface ICardTypeRepository
    {
        /// <summary>
        /// 查詢所有的卡片種類
        /// </summary>
        /// <returns></returns>
        IEnumerable<CardTypeDataModel> GetList();

        /// <summary>
        /// 查詢卡片種類
        /// </summary>
        /// <param name="id">卡片種類代號</param>
        /// <returns></returns>   
        CardTypeDataModel Get(string cardType);
    }
}
