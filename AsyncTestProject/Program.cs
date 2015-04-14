using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTestProject
{
    class Program
    {
        private static readonly FileDownloadExecutor fileDownloadExecutor = new FileDownloadExecutor();
        private static readonly String yesAnswer = "y";
        private static readonly String noAnswer = "n";
        private static readonly String resultAnswer = "r";
        private static readonly String userInputMessage = "Do you want to download a file? y/n Or get current download results? r";

        static void Main(string[] args)
        {
            UserInput userInput;
            Console.WriteLine(userInputMessage);
            while ((userInput = UserInputAnalyze(Console.ReadLine())) != UserInput.No)
            {
                if (userInput == UserInput.Yes)
                {
                    fileDownloadExecutor.AddNewDownload(generateRandomString());
                }
                else if(userInput == UserInput.CurrentResult)
                {
                    ShowResults();
                }
                else if (userInput == UserInput.Unknown)
                {
                    Console.WriteLine("Please, input y/n/r.");
                }
                Console.WriteLine(userInputMessage);
            }
            Console.WriteLine("Waiting for all downloads to be completed...");
            while (!fileDownloadExecutor.isAllDownloadsCompleted()) { }
            Console.WriteLine("Downloads result listing:");
            ShowResults();
            Console.WriteLine("Ending of main thread.");
            Console.ReadKey();
        }

        private static void ShowResults()
        {
            foreach (var currentDownload in fileDownloadExecutor.Results.OrderBy(item => item.Key))
            {
                Console.WriteLine(String.Format("url: {0}; result: {1};", currentDownload.Key, String.Join(" ", currentDownload.Value)));
            }
        }

        private static UserInput UserInputAnalyze(String userInput)
        {
            if (userInput == null)
            {
                return UserInput.Unknown;
            }
            else if (userInput == yesAnswer || userInput.ToLower() == yesAnswer)
            {
                return UserInput.Yes;
            }
            else if (userInput == noAnswer || userInput.ToLower() == noAnswer)
            {
                return UserInput.No;
            }
            else if (userInput == resultAnswer || userInput.ToLower() == resultAnswer)
            {
                return UserInput.CurrentResult;
            }
            else
            {
                return UserInput.Unknown;
            }
        }

        private enum UserInput
        {
            Yes, No, Unknown, CurrentResult
        }

        private static String generateRandomString()
        {
            int stringLength = (new Random()).Next(1, 6);
            StringBuilder result = new StringBuilder();
            for (int currentSymbol = 0; currentSymbol < stringLength; currentSymbol++)
            {
                result.Append("a");
            }
            return String.Concat("www.", result.ToString(), ".com");
        }
    }
}
