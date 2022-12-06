using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Honsha01.Iialib.Tools.Configs
{
    public class Honsha01IialibConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = Honsha01LibraryList.Iialib;

        public static Honsha01IialibConfig Of() => new Honsha01IialibConfig();

        Honsha01IialibConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = IialibLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();

            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA00W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA01W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA02W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA03W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA04W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA05W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA06W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA07W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA08O0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA09W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA10W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA11W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA12W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA13W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA16W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA17W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA19W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA20W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA21W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA24W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA27W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA35W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA36W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA40W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA41W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA42W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA43W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA44W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA45W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA46W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA50W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA80W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA93W0"));
            rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA94W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIA95W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAA1W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIABNW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIABUP1"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIABUP2"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIABUW7"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAGRAPH"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAMDEL"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIANWW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAP1W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAP2W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAP5W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAP6W0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAPPW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAXXW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIAZNW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIBDEL"));
            rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIBXXW0"));
            //rtn.Add(Honsha01LibraryList.Iialib.ObjectIDOf("JIIBXYW0"));

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
