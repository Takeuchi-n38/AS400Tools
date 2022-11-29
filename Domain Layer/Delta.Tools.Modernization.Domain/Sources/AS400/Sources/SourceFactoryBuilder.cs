using Delta.Tools.AS400.TXTs;
using System.IO;
using Delta.Modernization;

namespace Delta.Tools.AS400.Sources
{
    public class SourceFactoryBuilder
    {
        readonly string CometSourcesFolderPath;
        SourceFactoryBuilder(string aCometSourcesFolderPath)
        {
            this.CometSourcesFolderPath = aCometSourcesFolderPath;
        }
        public static SourceFactoryBuilder Of(string aCometSourcesFolderPath)
        {
            return new SourceFactoryBuilder(aCometSourcesFolderPath);

        }

        //public static SourceFactoryBuilder Of(IPathInfo pathInfo, IToolConfig toolConfig)
        //{
        //    return Of(pathInfo.CometSourcesDirectory.FullName, toolConfig.FileReader);
        //}

        public ISourceFactory CLSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("CL", "QCLSRC"));
        public ISourceFactory COBOLSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("COBOL", "QCBLSRC"));
        public ISourceFactory ILECOBOLSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("ILECOBOL", "QCBLSRC"));
        public ISourceFactory LFSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("LF", "QDDSSRC"));
        public ISourceFactory PFSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("PF", "QDDSSRC"));
        public ISourceFactory PRTSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("PRT", "QPRTSRC"));
        public ISourceFactory DSPSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("DSP", "QDSPSRC"));
        
        public ISourceFactory FMTSourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("SRT", "QFMTSRC"), CreatePathTemplateForWithCommet("MBR", "QFMTSRC"));
        public ISourceFactory RPG3SourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("RPG", "QRPGSRC"));
        public ISourceFactory RPG4SourceFileReader() => SourceFactoryWithComet.Of(CreatePathTemplateForWithCommet("ILERPG", "QRPGLESRC"));

        string CreatePathTemplateForWithCommet(string Folder, string Prefix) => string.Format("{0}\\{{0}}\\{{1}}\\{1}\\{{1}}\\{2}.{{2}}", CometSourcesFolderPath, Folder, Prefix);
        ////            this.pathTemplate = CreatePathTemplate(SourceRootFolderPath, Folder,Prefix);

        public ISourceFactory TXTSourceFileReader(ReCreateTXTList ReCreateTXTList) => new TXTSourceFactory(CreatePathTemplateForWithCommet("TXT", "QTXTSRC"), TxtSourceObjectListPathTemplate, ReCreateTXTList);

        //ReCreateTXTList
        string TxtSourceObjectListPathTemplate => string.Format("{0}\\{{0}}\\ObjectList.csv", CometSourcesFolderPath);

    }
}
