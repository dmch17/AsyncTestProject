using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    class FileDownloadExecutor : IAsyncDownloader
    {
        private readonly FileDownloader fileDownloader = new FileDownloader();
        private readonly Object syncObj = new Object();

        public Task<byte[]> DownloadAsync(String url)
        {
            //На всякий случай :) Но, как по мне, из логики main следует, что данный метод одновременно не смогут использовать несколько потоков.
            lock(syncObj)
            {
                return Task.Factory.StartNew(() =>
                {
                    return fileDownloader.Download(url);
                });
            }
        }
    }
}
