using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;

namespace YildirimHoldingTest.Core.Data.Factory
{
    public class DbFactory : IDbFactory
    {
        private IConnectionFactory connectionFactory;
        public DbFactory(IConnectionFactory _connectionFactory)
        {
            this.connectionFactory = _connectionFactory;
        }
        public IEnumerable<TResult> GetData<TResult>(StringBuilder queryScript, DynamicParameters parameters)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.Query<TResult>(queryScript.ToString(), parameters);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }

        public IEnumerable<TResult> GetData<TResult>(StringBuilder queryScript, object parameters)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.Query<TResult>(queryScript.ToString(), parameters);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }

        public IEnumerable<TResult> GetDataWithSp<TResult>(string SpName, object parm)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.Query<TResult>(SpName, parm, commandType: CommandType.StoredProcedure);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }

        public TResult GetSingleData<TResult>(StringBuilder queryScript, DynamicParameters parameters)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.QueryFirstOrDefault<TResult>(queryScript.ToString(), parameters, null, 1000, CommandType.Text);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }

        public TResult GetSingleDataWithSp<TResult>(string SpName, object parm)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.QueryFirst<TResult>(SpName, parm, commandType: CommandType.StoredProcedure);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }

        public TResult GetSingleDataWithId<TResult>(StringBuilder selectClause, int parmId, DbConnection dbConnection)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(false))
                {
                    var result = conn.QuerySingle<TResult>(selectClause.ToString(), new { Id = parmId });
                    if (dbConnection == null)
                        connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }
    }
}
