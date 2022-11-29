using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Tools.AS400.Honsha01.Jj_lib.Configs
{
    public class Honsha01Jj_libConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Honsha01Jj_libConfig Of() => new Honsha01Jj_libConfig();

        Honsha01Jj_libConfig()
        {
        }

        Library IToolConfig.MainLibrary => Honsha01LibraryList.Jj_lib;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        Honsha01LibraryList.Jj_lib,};

        LibraryFactory IToolConfig.LibraryFactory { get; } = Jj_libLibraryFactory.Of();

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JJJB15M0"));
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JJJB15M0"));//HONSHA01,JJ_LIB,JJJB15M0,475,152,1
            //rtn.Add(Honsha01LibraryList.Oplib.ObjectIDOf("CYAE320"));
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();

            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB010"));//HONSHA01,JJ_LIB,AJJB010,161,30,0,4,2
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB020A"));//HONSHA01,JJ_LIB,AJJB020A,347,64,0,7,1
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJB111A"));//HONSHA01,JJ_LIB,AJJB111A,159,29,0,2,2
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE01"));//HONSHA01,JJ_LIB,AJJBE01,75,25,0,2,0
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE02"));//HONSHA01,JJ_LIB,AJJBE02,77,22,0,1,0
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE03"));//HONSHA01,JJ_LIB,AJJBE03,57,15,0,2,0
            rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("AJJBE04"));//HONSHA01,JJ_LIB,AJJBE04,57,19,0,3,0

            //rtn.Add(Honsha01LibraryList.Oplib.ObjectIDOf("AYAE320"));

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

        List<DataArea> IToolConfig.DataAreas()
        {
            var rtn = new List<DataArea>();
            rtn.Add(Honsha01LibraryList.Comnlib.DataAreaOf("LIBDTAARA"));
            rtn.Add(Honsha01LibraryList.Comnlib.DataAreaOf("SRCDTAARA"));
            return rtn;
        }

        public List<Library> LibrariesOfDB2foriRepository()
        {
            return new List<Library>() {
                Honsha01LibraryList.Prodlib,
                Honsha01LibraryList.Oplib,
                };
        }
    }
}
