using System;
using System.Collections.Generic;
using Delta.AS400.Objects;
using System.Linq;
using System.IO;
using System.Text;

namespace Delta.Tools.AS400.Sources
{
    public class SourceFactoryWithComet : ISourceFactory
    {
        readonly List<string> CometSourcePathTemplates;
        IEnumerable<string> FilePaths(ObjectID objectID)
        {
            string partitionName = objectID.Library.Partition.Name;
            string libraryName = objectID.Library.Name;
            string className = objectID.Name;
            return CometSourcePathTemplates.Select(pathTemplate=> string.Format(pathTemplate, partitionName, libraryName, className));
        }

        SourceFactoryWithComet(List<string> aCometSourcePathTemplates)
        {
            this.CometSourcePathTemplates = aCometSourcePathTemplates;
        }

        public static SourceFactoryWithComet Of(string cometSourcePathTemplates)
        {
            return new SourceFactoryWithComet(new List<string>() { cometSourcePathTemplates });
        }

        public static SourceFactoryWithComet Of( string cometSourcePathTemplate, string cometSourcePathTemplateOld)
        {
            return new SourceFactoryWithComet(new List<string>() { cometSourcePathTemplate, cometSourcePathTemplateOld });
        }

        Dictionary<string, Source> SourceDictionary = new Dictionary<string, Source>();


        bool ISourceFactory.SourceExists(ObjectID objectID)
        {
            return FilePaths(objectID).Any(sourceFilePath=> SourceExistsBy(sourceFilePath));
        }

        bool SourceExistsBy(string sourceFilePath)
        {
            if (!File.Exists(sourceFilePath)) return false;
            var sourceLines = File.ReadAllLines(sourceFilePath);
            return (sourceLines != null && sourceLines.Length > 0) ;//Commetの中身空っぽファイル対応                    
        }

        Source ISourceFactory.Read(ObjectID objectID)
        {
            foreach (string sourceFilePath in FilePaths(objectID))
            {
                if (SourceDictionary.TryGetValue(sourceFilePath, out Source? readSoure)) return readSoure;

                if (!SourceExistsBy(sourceFilePath))
                {
                    SourceDictionary.Add(sourceFilePath, Source.NullValue(objectID));
                    continue;
                }

                var sourceLines = Shift_JISFile.ReadAllLines(sourceFilePath);

                if (sourceLines != null && sourceLines.Length > 0)
                {
                    var newSource = Source.Create(objectID, sourceLines);
                    SourceDictionary.Add(sourceFilePath, newSource);
                    return newSource;
                }

            }

            var nullSource = Source.NullValue(objectID);
            var lastFilePath = FilePaths(objectID).Last();
            if (!SourceDictionary.ContainsKey(lastFilePath))
            {
                SourceDictionary.Add(lastFilePath, nullSource);
            }

            return nullSource;
        }

    }

}
