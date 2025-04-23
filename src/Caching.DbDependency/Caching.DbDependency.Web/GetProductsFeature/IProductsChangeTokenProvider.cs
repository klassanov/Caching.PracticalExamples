using Microsoft.Extensions.Primitives;

namespace Caching.DbDependency.Web.GetProductsFeature
{
    public interface IProductsChangeTokenProvider
    {
        IChangeToken GetChangeToken();

        void SignalChange();
    }
}