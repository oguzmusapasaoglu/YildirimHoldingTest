using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Core.Cache.Interfaces
{
    public interface ICacheFactory<TData>where TData : BaseModel
    {
        IEnumerable<TData> GetAllCachedData();
    }
}
