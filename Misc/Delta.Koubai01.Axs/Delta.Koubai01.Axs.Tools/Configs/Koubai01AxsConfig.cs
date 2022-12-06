using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Koubai01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;
using Delta.Koubai01.Axs.Tools.Configs;

namespace Delta.Koubai01.Axs.Tools.Configs
{
    public class Koubai01AxsConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = AxsLibraryFactory.Axs;

        public static Koubai01AxsConfig Of() => new Koubai01AxsConfig();

        Koubai01AxsConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = AxsLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Koubai01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();


            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXX110C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS010C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS040C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS120C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS050C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS060C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS070C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS080C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS090C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK030C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK040C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK060C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK070C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK090C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK140C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK110C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK740C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXBACK1"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK150C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK160C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK170C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK200C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK250C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK800C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK810C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK820C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK510C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK520C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK530C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK540C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK550C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK560C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK570C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK580C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK590C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK600C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK610C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK620C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK630C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK640C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK650C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK660C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK680C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK690C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK700C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK710C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK720C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK730C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK790C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM010"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM020"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM030"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM100C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM050"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM061C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM070"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM080"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM090"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM120"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM150"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM130"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM450"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM460"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM140"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM011"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM310C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM320C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM330C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM390C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM350C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM360C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM370C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM380C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM400C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM410C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM430C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM440C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXM420C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT010C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT030C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT210"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT100C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT049C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT069C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT089C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT099C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT222C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT223C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT260C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT279C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT289C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT120C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT020C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT110C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT059C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT079C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT130C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT159C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT179C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT140C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT169C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT189C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT230C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT259C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXT249C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXX800C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK750C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK751C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK752C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK760C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK761C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK762C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK770C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK771C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK772C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK780C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK781C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK782C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK850C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK851C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXK852C"));
            rtn.Add(AxsLibraryFactory.Axs.ObjectIDOf("AXS110C"));


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
