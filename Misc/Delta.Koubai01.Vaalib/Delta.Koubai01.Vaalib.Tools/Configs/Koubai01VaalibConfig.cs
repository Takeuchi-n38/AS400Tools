using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Koubai01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Koubai01.Vaalib.Tools.Configs
{
    public class Koubai01VaalibConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = Koubai01LibraryList.Vaalib;

        public static Koubai01VaalibConfig Of() => new Koubai01VaalibConfig();

        Koubai01VaalibConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = VaalibLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Koubai01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();


            rtn.Add(Koubai01LibraryList.Vaalib.ObjectIDOf("JVAA70DK"));


            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();


            //rtn.Add(Koubai01LibraryList.Dsldlib.ObjectIDOf("CJIE000"));//1	CL	HONSHA01	DSLDLIB	CJIE000	107	38	0


            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();






            return rtn;


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
            //rtn.Add(DataArea.Of(Koubai01LibraryList.Comnlib, "LIBDTAARA"));
            //rtn.Add(DataArea.Of(Koubai01LibraryList.Comnlib, "SRCDTAARA"));
            //rtn.Add(DataArea.Of(Koubai01LibraryList.Lfelib, "DTAJUN"));
            return rtn;
        }

        List<Library> IGeneratorConfig.LibrariesOfDB2foriRepository()
        {
            return new List<Library>() {
                //Koubai01LibraryList.Prodlib,
                //Koubai01LibraryList.Oplib,
                //Koubai01LibraryList.Master2,
                //Koubai01LibraryList.Toyolib,
                //Koubai01LibraryList.Seatlib,
                //Koubai01LibraryList.Usl2lib,
                //Koubai01LibraryList.Wrkcat1,
                //Koubai01LibraryList.Wrkcat2
                };

        }

    }
}
