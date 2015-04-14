using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    class FileDownloader : IAsyncDownloader
    {
        private static readonly int singleStep = 500;
        private static readonly Object syncObj = new Object();

        public Task<byte[]> DownloadAsync(string url)
        {
            lock(syncObj)
            {
                List<byte> result = new List<byte>();
                Random randomizer = new Random();
                return Task.Factory.StartNew(() =>
                {
                    for (int currentByte = randomizer.Next(1, 6); currentByte > 0; currentByte--)
                    {
                        result.Add((Byte)randomizer.Next(Byte.MinValue, Byte.MaxValue));
                    }
                    Thread.Sleep(url.Length * FileDownloader.singleStep);
                    return result.ToArray<byte>();
                });
            }
            
        }
    }
}
