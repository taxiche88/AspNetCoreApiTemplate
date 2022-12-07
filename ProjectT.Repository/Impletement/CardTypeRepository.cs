
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ProjectT.Repository.Dtos.DataModel;
using ProjectT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectT.Repository.Impletement
{
    /// <summary>
    /// 卡片種類, 僅限定查詢, 其他操作需透過DB修改
    /// </summary>
    /// <seealso cref="ProjectT.Repository.Interface.ICardTypeRepository" />
    public class CardTypeRepository : ICardTypeRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private readonly string _connectString;

        public CardTypeRepository(string connectString)
        {
            this._connectString = connectString;
        }

        /// <summary>
        /// 查詢卡片種類
        /// </summary>
        /// <param name="id">卡片種類代號</param>
        /// <returns></returns>   
        public CardTypeDataModel Get(string cardType)
        {
            using (var conn = new OracleConnection(_connectString))
            {
                var sql = @"SELECT * FROM CARD_TYPE WHERE CARD_TYPE = :CARD_TYPE AND ROWNUM = 1";

                var parameters = new DynamicParameters();
                parameters.Add("CARD_TYPE", cardType, System.Data.DbType.String);

                var result = conn.QueryFirstOrDefault<CardTypeDataModel>(sql, parameters);
                return result;
            }
        }

        /// <summary>
        /// 查詢所有的卡片種類
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CardTypeDataModel> GetList()
        {
            using (var conn = new OracleConnection(_connectString))
            {
                var sql = @"SELECT * FROM CARD_TYPE";

                var result = conn.Query<CardTypeDataModel>(sql);
                return result;
            }
        }
    }
}
