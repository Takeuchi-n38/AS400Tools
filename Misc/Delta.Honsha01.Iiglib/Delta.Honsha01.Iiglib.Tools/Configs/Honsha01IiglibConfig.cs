using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Honsha01.Iiglib.Tools.Configs
{
    public class Honsha01IiglibConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = Honsha01LibraryList.Iiglib;

        public static Honsha01IiglibConfig Of() => new Honsha01IiglibConfig();

        Honsha01IiglibConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = IiglibLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();

            rtn.Add(Honsha01LibraryList.Iiglib.ObjectIDOf("JIIG01D0")); 
            rtn.Add(Honsha01LibraryList.Iiglib.ObjectIDOf("JIIG10D0")); 

            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();

            /*

             CIIE010			
                PIIE015		
                CIIE020		1
                    PIIE010	
                CIIE020		2
                    PIIE020	
                CIIE020		3

             */
            rtn.Add(MainLibrary.ObjectIDOf("CIIE010"));
            rtn.Add(MainLibrary.ObjectIDOf("CIIE020"));
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();

            /*

             CIIE010			
                PIIE015		
                CIIE020		1
                    PIIE010	
                CIIE020		2
                    PIIE020	
                CIIE020		3

             */

            rtn.Add(MainLibrary.ObjectIDOf("PIIE015"));
            rtn.Add(MainLibrary.ObjectIDOf("PIIE010"));
            rtn.Add(MainLibrary.ObjectIDOf("PIIE020"));

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
                Honsha01LibraryList.Prodlib,
                //Honsha01LibraryList.Oplib,
                //Honsha01LibraryList.Master2,//更新ありのファイルもあるのでそちらは別途検討の必要あり
                //Honsha01LibraryList.Toyolib,
                //Honsha01LibraryList.Seatlib,
                //Honsha01LibraryList.Usl2lib,
                //Honsha01LibraryList.Wrkcat1//,
                //Honsha01LibraryList.Wrkcat2//Postgresのものは中間ファイルと思われるのでインメモリとして実装するかもしれないもの
                };

        }

    }
}
/*
 
 CIIE010			
	PIIE015		
	CIIE020		1
		PIIE010	
	CIIE020		2
		PIIE020	
	CIIE020		3

 */