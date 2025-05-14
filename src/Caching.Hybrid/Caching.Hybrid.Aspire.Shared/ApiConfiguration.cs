namespace Caching.Hybrid.Aspire.Shared
{
    public class ApiConfiguration
    {
        public int NumReplicas { get; set; }

        public bool RegisterCacheInvalidatorService { get; set; }
    }
}
