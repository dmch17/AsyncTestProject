using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    interface IAsyncDownloader
    {
        Task<byte[]> DownloadAsync(string url);
    }
}
