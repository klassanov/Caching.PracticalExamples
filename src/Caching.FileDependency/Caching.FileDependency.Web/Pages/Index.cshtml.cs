using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace Caching.FileDependency.Web.Pages
{
    public class IndexModel(IMemoryCache cache, ILogger<IndexModel> logger, IFileProvider fileProvider) : PageModel
    {
        private const string fileKey = "FileKey";
        private const string fileName = "test.txt";
        private const string fileRootPath = @"C:\tmp";

        private readonly ILogger<IndexModel> logger = logger;
        private readonly IMemoryCache cache = cache;
        private readonly IFileProvider fileProvider = fileProvider;

        [BindProperty]
        public string FilePath => Path.Combine(fileRootPath, fileName);


        [BindProperty]
        public string? FileContent { get; set; }

        public async Task OnGet()
        {
            if (cache.TryGetValue(fileKey, out string? cachedContent))
            {
                FileContent = cachedContent!;
                logger.LogInformation("Cache hit: File Content Loaded from the cache");
            }
            else
            {
                FileContent = await System.IO.File.ReadAllTextAsync(FilePath);
                AddFileContentToCache();
                logger.LogInformation($"Cache miss: File Content Loaded from the file {FilePath}");
            }
        }

        private void AddFileContentToCache()
        {
            var changeToken = fileProvider.Watch(fileName);

            // Combine more than one condition for cache expiration - OR logic

            // NOTE: With IMemory cache and SetAbsoluteExpiration(..) or SetSlidingExpiration(...) methods
            // the eviction is lazy, meaning that
            // the cache entry is not actively evicted when the sliding expiration time elapses.
            // Instead, the entry is marked as expired, and eviction only occurs when the cache is accessed.

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(20))                   
                   .AddExpirationToken(changeToken)
                   .RegisterPostEvictionCallback((key, value, reason, state) =>
                   {
                       logger.LogInformation("Cache entry '{key}' was evicted. Reason: {reason}", key, reason);
                   });

            cache.Set(fileKey, FileContent, cacheEntryOptions);
        }
    }
}
