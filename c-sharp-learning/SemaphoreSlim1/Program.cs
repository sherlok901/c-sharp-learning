using System.Collections.Concurrent;

namespace SemaphoreSlim1;

class Program
{
    static int _count = 0;
    
    static async Task Main(string[] args)
    {
        var providerIds = Enumerable.Range(1, 15);
        var successPrices = new ConcurrentDictionary<int, int>();
        var bound = new SemaphoreSlim(3, 3);
        
        var tasks = providerIds.Select(async id =>
        {
            try
            {
                await bound.WaitAsync();
                var result = await GetPriceFromProviderAsync(id);
                successPrices.GetOrAdd(id, result);
                return new Result(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {id}: {e}");
            }
            finally
            {
                bound.Release();
            }

            return new Result(false);
        }).ToArray();
        
        var t = await Task.WhenAll(tasks);
        
        Console.WriteLine($"Price sum: {t.Sum(i=>i.result)}{Environment.NewLine}Success:{t.Count(i=>i.isSuccess)}" +
                          $"{Environment.NewLine}Failed:{t.Count(i=>!i.isSuccess)}");
    }
    
    static async Task<int> GetPriceFromProviderAsync(int providerId)
    {
        var rand = new Random();
        
        await Task.Delay(rand.Next(500, 2000));
        
        Interlocked.Increment(ref _count);
        if (_count % 5 == 0)
        {
            throw new Exception("Provider timeout");
        }
        
        return rand.Next(100, 500);
    }
    
    public class Result
    {
        public int result;
        public bool isSuccess;

        public Result(int result, bool success = true)
        {
            this.result = result;
            this.isSuccess = success;
        }
        public Result(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }
    }
}