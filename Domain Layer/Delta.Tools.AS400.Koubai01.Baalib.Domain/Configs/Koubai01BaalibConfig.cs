using Delta.Tools.AS400.Configs;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Koubai01;

namespace Delta.Tools.AS400.Koubai01.Baalib.Configs
{
    public class Koubai01BaalibConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Koubai01BaalibConfig Of() => new Koubai01BaalibConfig();

        Koubai01BaalibConfig()
        {
        }

        Library IToolConfig.MainLibrary => Koubai01LibraryList.Baalib;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        ((IToolConfig)this).MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = BaalibLibraryFactory.Of();

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JBAA05M0"));
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JBAA10M0"));
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JBAA15M0"));
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JBAA20M0"));
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JJJB15M0"));//HONSHA01,JJ_LIB,JJJB15M0,475,152,1
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();

            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB010"));//HONSHA01,JJ_LIB,AJJB010,161,30,0,4,2
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB020A"));//HONSHA01,JJ_LIB,AJJB020A,347,64,0,7,1
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB111A"));//HONSHA01,JJ_LIB,AJJB111A,159,29,0,2,2
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE01"));//HONSHA01,JJ_LIB,AJJBE01,75,25,0,2,0
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE02"));//HONSHA01,JJ_LIB,AJJBE02,77,22,0,1,0
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE03"));//HONSHA01,JJ_LIB,AJJBE03,57,15,0,2,0
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE04"));//HONSHA01,JJ_LIB,AJJBE04,57,19,0,3,0

            return rtn;
            /*         
            */
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG4ObjectIDs()
        {
            var rtn = new List<ObjectID>();
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.NoGenerateServiceObjectIDs()
        {
            var rtn = new List<ObjectID>();
            return rtn;
        }

        public List<Library> LibrariesOfDB2foriRepository()
        {
            return new List<Library>() {
                Koubai01LibraryList.Prodlib,
                Koubai01LibraryList.Oplib
                };
        }
    }

}
