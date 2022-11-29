using Delta.AS400.Objects;
using Delta.Tools.AS400.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Delta.Tools.AS400.TXTs
{
    public class TXTSourceFactory : ISourceFactory
    {
        readonly string CometSourcePathTemplate;
        readonly string pathTemplate;

        public TXTSourceFactory(string aCometSourcePathTemplate, string pathTemplate, ReCreateTXTList ReCreateTXTList)
        {
            this.CometSourcePathTemplate=aCometSourcePathTemplate;
            this.pathTemplate = pathTemplate;
            this.ReCreateTXTList = ReCreateTXTList;
        }
        ReCreateTXTList ReCreateTXTList;

        Dictionary<string, string> objectListFiles = new Dictionary<string, string>();

        bool ISourceFactory.SourceExists(ObjectID objectID)
        {
            var souceFilePath = string.Format(pathTemplate, objectID.Library.Partition.Name.ToPascalCase());

            if (File.Exists(souceFilePath))
            {
                var AllTxt = string.Empty;
                if (!objectListFiles.TryGetValue(souceFilePath, out AllTxt))
                {
                    AllTxt = Shift_JISFile.ReadAllText(souceFilePath);
                    objectListFiles.Add(souceFilePath, AllTxt);
                }

                if (AllTxt.Contains(objectID.Name))
                {
                    return true;
                }
            }

            var txt = ReCreateTXTList.Find(objectID);
            if (txt != null)
            {
                return true;
            }
            return false;
        }

        Source ISourceFactory.Read(ObjectID objectID)
        {

            var souceFilePath = string.Format(pathTemplate, objectID.Library.Partition.Name.ToPascalCase());

            if (File.Exists(souceFilePath))
            {
                var AllTxt = string.Empty;
                if (!objectListFiles.TryGetValue(souceFilePath, out AllTxt))
                {
                    AllTxt = Shift_JISFile.ReadAllText(souceFilePath);
                    objectListFiles.Add(souceFilePath, AllTxt);
                }

                if (AllTxt.Contains(objectID.Name))
                {
                    return Source.Of(objectID, new string[] { $"{objectID.Name}.TXT" });
                }
            }

            var txt = ReCreateTXTList.Find(objectID);
            if (txt != null)
            {
                return txt.OriginalSource;
            }
            return Source.NullValue(objectID);
        }

    }
}
