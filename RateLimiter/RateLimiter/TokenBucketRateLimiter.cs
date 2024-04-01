using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class TokenBucketRateLimiter
    {
        private object _lock = new object();

        private int _capacity;

        public void PeriodicFill()
        {
            while (true)
            {
                lock (_lock)
                {
                    _capacity += 1;
                    Console.WriteLine("Added 1 token to the bucket. Current capacity: " + _capacity);
                }

                Thread.Sleep(1000);
            }
        }

        public bool isAllowed(HttpRequestMessage request)
        {
            lock (_lock)
            {
                if (_capacity > 0)
                {
                    _capacity -= 1;
                    Console.WriteLine("One token consumed. Current capacity: " + _capacity);
                    return true;
                }
                else
                {
                    Console.WriteLine("No token available. Request denied.");
                    return false;
                }
            }
        }

        public static void Main()
        {
            var rateLimiter = new TokenBucketRateLimiter();
            var fillerJob = new Thread(() => rateLimiter.PeriodicFill());
            fillerJob.Start();

            for (int i = 0; i < 10; i++)
            {
                var request = new HttpRequestMessage();
                var requestJob = new Thread(() => rateLimiter.isAllowed(request));
                requestJob.Start();
            }

        }
    }


}
