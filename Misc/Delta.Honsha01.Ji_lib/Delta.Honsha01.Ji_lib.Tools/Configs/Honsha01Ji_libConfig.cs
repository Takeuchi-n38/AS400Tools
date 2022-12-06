using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Honsha01.Ji_lib.Tools.Configs
{
    public class Honsha01Ji_libConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = Honsha01LibraryList.Ji_lib;

        public static Honsha01Ji_libConfig Of() => new Honsha01Ji_libConfig();

        Honsha01Ji_libConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = Ji_libLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();

            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIB05D4"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIC10D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE15D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE20D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE30D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE30D1"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE30D2"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIE40D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIF00D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIF20D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIH05D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIH06D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIJ05D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIK05D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL05D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL06D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL10D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL15D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL30D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIX05D0"));
            //rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIX10D0"));
            rtn.Add(Honsha01LibraryList.Ji_lib.ObjectIDOf("JJIL05W0"));

            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();


            //rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE000"));//1	CL	HONSHA01	DSLDLIB	CJIE000	107	38	0


            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();



            //rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE000"));//HONSHA01,DSLDLIB,PJIE000,26,14,0,1,0 // 別途方法にて移行済みなので、Serviceとしては移行しない
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE001"));//HONSHA01,DSLDLIB,PJIE001,43,20,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE002"));//HONSHA01,DSLDLIB,PJIE002,64,27,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE29X"));//HONSHA01,DSLDLIB,PJIE29X,24,10,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE300"));//HONSHA01,DSLDLIB,PJIE300,825,244,0,10,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE310"));//HONSHA01,DSLDLIB,PJIE310,356,104,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE31B"));//HONSHA01,DSLDLIB,PJIE31B,106,32,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE32A"));//HONSHA01,DSLDLIB,PJIE32A,77,29,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE330"));//HONSHA01,DSLDLIB,PJIE330,296,79,0,6,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE340"));//HONSHA01,DSLDLIB,PJIE340,173,44,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE341"));//HONSHA01,DSLDLIB,PJIE341,172,42,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE350"));//HONSHA01,DSLDLIB,PJIE350,674,76,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE351"));//HONSHA01,DSLDLIB,PJIE351,744,77,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE361"));//HONSHA01,DSLDLIB,PJIE361,275,58,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE36X"));//HONSHA01,DSLDLIB,PJIE36X,296,41,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE36Y"));//HONSHA01,DSLDLIB,PJIE36Y,332,43,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE39D"));//HONSHA01,DSLDLIB,PJIE39D,869,258,0,10,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE44B"));//HONSHA01,DSLDLIB,PJIE44B,621,152,0,3,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE600"));//3,R3,HONSHA01,DSLDLIB,PJIE600,1193,307,1,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIEDAT"));//HONSHA01,DSLDLIB,PJIEDAT,39,14,0,2,0






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
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Comnlib, "LIBDTAARA"));
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Comnlib, "SRCDTAARA"));
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Lfelib, "DTAJUN"));
            return rtn;
        }

        List<Library> IGeneratorConfig.LibrariesOfDB2foriRepository()
        {
            return new List<Library>() {
                //Honsha01LibraryList.Prodlib,
                //Honsha01LibraryList.Oplib,
                //Honsha01LibraryList.Master2,
                //Honsha01LibraryList.Toyolib,
                //Honsha01LibraryList.Seatlib,
                //Honsha01LibraryList.Usl2lib,
                //Honsha01LibraryList.Wrkcat1,
                //Honsha01LibraryList.Wrkcat2
                };

        }

    }
}
