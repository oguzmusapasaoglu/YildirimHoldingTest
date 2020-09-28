using System.Data;
using System.Data.Common;

namespace YildirimHoldingTest.Core.Data.Interfaces
{
    public interface IConnectionFactory
    {
        IDbTransaction dbTransaction { get; set; }
        DbConnection CreateConnection(bool parmNeedTrans = false);
        void CloseConnection(DbConnection conn);
    }
}