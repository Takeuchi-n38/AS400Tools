using Delta.AS400.Libraries;
using Delta.AS400.Partitions;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.F__lib.Tools.Configs
{
    class F__libLibraryFactory : LibraryFactory
    {

        F__libLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }
        public static Library Pdmlib = Library.Of(Partition.Honsha01, "PDMLIB");
        public static F__libLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
        Honsha01LibraryList.F__lib,
        Honsha01LibraryList.Pialib,
        Honsha01LibraryList.Hi_lib,
        Honsha01LibraryList.Ijalib,
        Honsha01LibraryList.Prodlib,
        Honsha01LibraryList.Comnlib,
        Honsha01LibraryList.Qgpl,
        Honsha01LibraryList.Oplib,
        Pdmlib,

        };
            return new F__libLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;

            if (libraryName == "&CMN") return Honsha01LibraryList.Comnlib;
            if (libraryName == "&KL_") return Honsha01LibraryList.Kl_lib;
            if (libraryName == "&LFB") return Honsha01LibraryList.Lfblib;
            if (libraryName == "&LFE") return Honsha01LibraryList.Lfelib;
            if (libraryName == "&MS2") return Honsha01LibraryList.Master2;
            if (libraryName == "&MS9") return Honsha01LibraryList.Master9;
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&SEAT") return Honsha01LibraryList.Seatlib;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;
            if (libraryName == "&LIB") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&WEB") return Honsha01LibraryList.Weblib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&FLL") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;
            if (libraryName == "&F__") return Honsha01LibraryList.F__lib;

            return Library.OfUnKnown(libraryName);
        }

        //HONSHA01 SRCDTAARA
        //ZZZLIB    HI_LIB    F__LIB    YB_LIB    QADLIB    QKDLIB    QKBLIB    QKGLIB    QKALIB    QKKLIB    
        //YBCLIB    KL_LIB    KI_LIB    JI_LIB    JJ_LIB    LKALIB    MIALIB    MIBLIB    IIALIB    IICLIB    
        //IJALIB    IJDLIB    IJFLIB    EIALIB    EICLIB    OACLIB    LIBLIB    PIALIB    PIBLIB    PICLIB    
        //PIDLIB    PIELIB    PIFLIB    PIGLIB    PIHLIB    PIJLIB    KWXTLIB   IJGLIB    QKCLIB    OPLIB

        //HONSHA01 LIBDTAARA
        //COMNLIB   MASTER2   MASTER9   PRODLIB   WPRODLIB                                                    
        //WRKCAT1   WRKCAT2   WRKCAT3   WRKCAT4   WRKCAT5   WRKCAT6   WRKCAT7                                 
        //TSTCAT1   TSTCAT2                                                                                   
        //WSVFLIB
    }
}
