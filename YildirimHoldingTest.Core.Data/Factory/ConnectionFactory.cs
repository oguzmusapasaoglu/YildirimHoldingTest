using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using YildirimHoldingTest.Core.Common.ConfigHelper;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;

namespace YildirimHoldingTest.Core.Data.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbTransaction dbTransaction { get; set; }
        public DbConnection CreateConnection(bool parmNeedTrans = false)
        {
            try
            {
                var connection = new SqlConnection(AppConfigHelper.GetRDBConnectionString());
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();
                if (parmNeedTrans)
                    dbTransaction = connection.BeginTransaction();
                return connection;
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
        public void CloseConnection(DbConnection conn)
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch (DbException ex)
            {
                throw new FattalDbException(ErrorTypeEnum.DbOperationException, MethodBase.GetCurrentMethod().GetMethodName(), ex.Message, ex);
            }
        }
    }
}
