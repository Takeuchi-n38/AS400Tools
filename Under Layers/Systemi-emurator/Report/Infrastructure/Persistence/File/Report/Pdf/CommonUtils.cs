using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class CommonUtils
    {
        public static List<List<string>> GetDataTest(string csvPath)
        {
            try
            {
                List<string> rawLines = System.IO.File.ReadAllLines(csvPath).ToList();

                List<List<string>> dataTestList = new List<List<string>>();

                foreach (string rawLine in rawLines)
                {
                    string[] texts = rawLine.Split(',');
                    List<string> entryDataTest = new List<string>();

                    foreach (string text in texts)
                    {
                        entryDataTest.Add(text);
                    }

                    dataTestList.Add(entryDataTest);
                }
                return dataTestList;
            }
            catch (Exception)
            {
                throw new SystemException();
            }
        }
    }
}
