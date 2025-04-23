using Microsoft.Extensions.Primitives;

namespace Caching.DbDependency.Web.GetProductsFeature
{
    public class ProductsChangeTokenProvider : IProductsChangeTokenProvider
    {
        private static CancellationTokenSource tokenSource = new();
        private static readonly Lock lockObject = new();

        public IChangeToken GetChangeToken()
        {
            lock (lockObject)
            {
                return new CancellationChangeToken(tokenSource.Token);
            }
        }

        public void SignalChange()
        {
            lock (lockObject)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = new CancellationTokenSource();
            }
        }

    }
}
