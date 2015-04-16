using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    class FileDownloader
    {
        private static readonly int singleStep = 500;

        public byte[] Download(string url)
        {
            List<byte> result = new List<byte>();
            Random randomizer = new Random();
            for (int currentByte = randomizer.Next(1, 6); currentByte > 0; currentByte--)
            {
                result.Add((Byte)randomizer.Next(Byte.MinValue, Byte.MaxValue));
            }
            Thread.Sleep(url.Length * FileDownloader.singleStep);
            return result.ToArray<byte>();
        }
    }
}
