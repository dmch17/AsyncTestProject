using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    class FileDownloadExecutor
    {
        private readonly Object syncObject = new Object();
        private readonly FileDownloader fileDownloader = new FileDownloader();
        private readonly ConcurrentQueue<String> queue = new ConcurrentQueue<string>();
        private readonly ConcurrentDictionary<String, byte[]> results = new ConcurrentDictionary<String, byte[]>();
        private int downloadCounter = 0;

        public ConcurrentDictionary<String, byte[]> Results
        {
            private set { }
            get { return results; }
        }

        public async void AddNewDownload(String url)
        {
            lock(syncObject)
            {
                if (!queue.IsEmpty)
                {
                    queue.Enqueue(url);
                    Console.WriteLine("Url {0} was added to the queue.", url);
                    return;
                }
            }
            queue.Enqueue(url);
            Console.WriteLine("Url {0} was added to the queue.", url);
            while (!queue.IsEmpty)
            {
                String currentUrl = null;
                Boolean isAbleToExtract = queue.TryPeek(out currentUrl);
                Console.WriteLine("Download of {0} starts.", currentUrl);
                results.TryAdd(String.Concat(++downloadCounter, ". ", currentUrl), await fileDownloader.DownloadAsync(currentUrl));
                Console.WriteLine("Download of {0} ends.", currentUrl);
                lock (syncObject) { isAbleToExtract = queue.TryDequeue(out currentUrl); }
            }
        }

        public Boolean isAllDownloadsCompleted()
        {
            return queue.IsEmpty;
        }
    }
}
