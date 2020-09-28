using Mapster;

namespace YildirimHoldingTest.Core.Common.Map
{
    public class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Adapt<TDestination>();
        }
    }
}
