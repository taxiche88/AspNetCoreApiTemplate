using Dapper;
using Oracle.ManagedDataAccess.Client;
using ProjectT.Repository.Dtos.Condition;
using ProjectT.Repository.Dtos.DataModel;
using ProjectT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectT.Repository.Impletement
{
    /// <summary>
    /// 卡片管理
    /// </summary>
    /// <seealso cref="ProjectT.Repository.Interface.ICardRepository" />
    public class CardRepository : ICardRepository
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private readonly string _connectString;

        public CardRepository(string connectString)
        {
            this._connectString = connectString;
        }

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <param name="condition">卡片查詢參數</param>
        /// <returns></returns>
        public IEnumerable<CardDataModel> GetList(CardSearchCondition condition)
        {
            var sql = "SELECT * FROM CARD";

            var sqlQuery = new List<string>();
            var parameter = new DynamicParameters();

            if (condition.MinCost.HasValue)
            {
                sqlQuery.Add($" COST >= :MinCost ");
                parameter.Add("MinCost", condition.MinCost);
            }

            if (condition.MaxCost.HasValue)
            {
                sqlQuery.Add($" COST <= :MaxCost ");
                parameter.Add("MaxCost", condition.MaxCost);
            }

            if (condition.MinAttack.HasValue)
            {
                sqlQuery.Add($" ATTACK >= :MinAttack ");
                parameter.Add("MinAttack", condition.MinAttack);
            }

            if (condition.MaxAttack.HasValue)
            {
                sqlQuery.Add($" ATTACK <= :MaxAttack ");
                parameter.Add("MaxAttack", condition.MaxAttack);
            }

            if (condition.MinHealth.HasValue)
            {
                sqlQuery.Add($" HEALTH >= :MinHealth ");
                parameter.Add("MinHealth", condition.MinHealth);
            }

            if (condition.MaxHealth.HasValue)
            {
                sqlQuery.Add($" HEALTH <= :MaxHealth ");
                parameter.Add("MaxHealth", condition.MaxHealth);
            }

            if (string.IsNullOrWhiteSpace(condition.Name) is false)
            {
                sqlQuery.Add($" NAME LIKE :Name ");
                parameter.Add("Name", $"%{condition.Name}%");
            }

            if (sqlQuery.Any())
            {
                sql += $" WHERE {string.Join(" AND ", sqlQuery)} ";
            }

            using (var conn = new OracleConnection(_connectString))
            {
                var result = conn.Query<CardDataModel>(sql, parameter);
                return result;
            }
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public CardDataModel Get(int id)
        {
            using (var conn = new OracleConnection(_connectString))
            {
                var sql = @"SELECT * FROM CARD WHERE ID = :ID AND ROWNUM = 1";

                var parameters = new DynamicParameters();
                parameters.Add("ID", id, System.Data.DbType.Int32);

                var result = conn.QueryFirstOrDefault<CardDataModel>(sql, parameters);
                return result;
            }
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="condition">卡片參數</param>
        /// <returns></returns>
        public int Insert(CardCondition condition)
        {
            var sql =
                @"
                    INSERT INTO CARD (ID, NAME, DESCRIPTION, ATTACK, HEALTH, COST, CARD_TYPE) VALUES (MYSEQUENCE.NEXTVAL, :NAME, :DESCRIPTION, :ATTACK, :HEALTH, :COST, :CARD_TYPE)
                ";

            using (var conn = new OracleConnection(_connectString))
            {
                var result = conn.Execute(sql, condition);
                return result;
            }
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="condition">卡片參數</param>
        /// <returns></returns>
        public bool Update(int id, CardUpdateCondition condition)
        {
            var sql =
                @"
                    UPDATE CARD SET NAME=:NAME, DESCRIPTION=:DESCRIPTION, ATTACK=:ATTACK, HEALTH=:HEALTH, COST=:COST
                    WHERE ID=:ID
                ";

            var parameters = new DynamicParameters(condition);
            parameters.Add("ID", id, System.Data.DbType.Int32);

            using (var conn = new OracleConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var sql =
                @"
                    DELETE FROM CARD WHERE ID = :ID
                ";

            var parameters = new DynamicParameters();
            parameters.Add("ID", id, System.Data.DbType.Int32);

            using (var conn = new OracleConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }
    }
}
