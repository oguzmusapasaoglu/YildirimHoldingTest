using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace YildirimHoldingTest.Core.Data.Interfaces
{
    public interface IDbFactory
    {
        IEnumerable<TResult> GetData<TResult>(StringBuilder queryScript, DynamicParameters parameters);
        IEnumerable<TResult> GetData<TResult>(StringBuilder queryScript, object parameters);
        IEnumerable<TResult> GetDataWithSp<TResult>(string SpName, object parm);
        TResult GetSingleData<TResult>(StringBuilder queryScript, DynamicParameters parameters);
        TResult GetSingleDataWithSp<TResult>(string SpName, object parm);
        TResult GetSingleDataWithId<TResult>(StringBuilder selectClause, int parmId, DbConnection dbConnection);
    }
}
