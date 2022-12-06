using Delta.AS400.Libraries;
using Delta.AS400.Partitions;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.Ji_lib.Tools.Configs
{
    class Ji_libLibraryFactory : LibraryFactory
    {

        Ji_libLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }
        public static Library Trlib = Library.Of(Partition.Honsha01, "TRLIB");
        public static Ji_libLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){

                Honsha01LibraryList.Ji_lib,
                Honsha01LibraryList.Oplib,
                Honsha01LibraryList.Prodlib,
                Honsha01LibraryList.Actlib,
                Honsha01LibraryList.Act2lib,
                Honsha01LibraryList.Carlib,
                Honsha01LibraryList.Comnlib,
                Honsha01LibraryList.Fsldlib,
                Honsha01LibraryList.F__lib,
                Honsha01LibraryList.Hi_lib,
                Honsha01LibraryList.Hostlib,
                Honsha01LibraryList.Iialib,
                Honsha01LibraryList.Knllib,
                Honsha01LibraryList.Lfdlib,
                Honsha01LibraryList.Lfelib,
                Honsha01LibraryList.Master2,
                Honsha01LibraryList.Mduklib,
                Honsha01LibraryList.Mknrlib,
                Honsha01LibraryList.Mialib,
                Honsha01LibraryList.Miblib,
                Honsha01LibraryList.Msyklib,
                Honsha01LibraryList.Qtemp,
                Honsha01LibraryList.Seedslib,
                Honsha01LibraryList.Seatlib,
                Honsha01LibraryList.Tachilib,
                Trlib,
                Honsha01LibraryList.Tstech2lib,
                Honsha01LibraryList.Usldlib,
                Honsha01LibraryList.Usl2lib,
                Honsha01LibraryList.Ujjhlib,
                Honsha01LibraryList.Wklib,
                Honsha01LibraryList.Wrkcat1,
                Honsha01LibraryList.Wrkcat2,
                Honsha01LibraryList.Wrkcat7,

                /**
                Honsha01LibraryList.Usl2lib,
                Honsha01LibraryList.Usl3lib,
                Honsha01LibraryList.Usldlib,
                Honsha01LibraryList.Wrkcat2,
                Honsha01LibraryList.Master2,
                Honsha01LibraryList.Ji_lib,
                Honsha01LibraryList.Toyolib,

                Honsha01LibraryList.Act2lib,
                Honsha01LibraryList.Rrnlib,
                Honsha01LibraryList.Wrkcat2,
                Honsha01LibraryList.Oplib
                Honsha01LibraryList.Ujjelib,
                Honsha01LibraryList.Ukelib,

                Honsha01LibraryList.Weblib,
                Honsha01LibraryList.Wrkcat2,
                Honsha01LibraryList.Lfblib,
                Honsha01LibraryList.Cslib,
                Honsha01LibraryList.Jj_lib,
                **/
        /**
        Honsha01LibraryList.Dsldlib,
        Honsha01LibraryList.Lfblib,
        Honsha01LibraryList.Lfelib,

        Honsha01LibraryList.Seatlib,
        Honsha01LibraryList.Kl_lib,
        Honsha01LibraryList.Qgpl,

        Honsha01LibraryList.Ybclib,
        Honsha01LibraryList.Ytstilib,
        Honsha01LibraryList.Yagilib,
        Honsha01LibraryList.Usl2lib,

        Honsha01LibraryList.Weblib,
        Honsha01LibraryList.Miblib,
        Honsha01LibraryList.Toyolib,
        
        Honsha01LibraryList.Wrkcat1
        **/
        };
            return new Ji_libLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&MS2") return Honsha01LibraryList.Master2;
            if (libraryName == "&US3") return Honsha01LibraryList.Usl3lib;
            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            if (libraryName == "&WC3") return Honsha01LibraryList.Wrkcat3;
            if (libraryName == "&WC4") return Honsha01LibraryList.Wrkcat4;
            if (libraryName == "&WC7") return Honsha01LibraryList.Wrkcat7;
            if (libraryName == "&CMN") return Honsha01LibraryList.Comnlib;
            if (libraryName == "&IJF") return Honsha01LibraryList.Ijflib;
            if (libraryName == "&IJG") return Honsha01LibraryList.Ijglib;
            if (libraryName == "&TC1") return Honsha01LibraryList.Tstcat1;
            if (libraryName == "&TC2") return Honsha01LibraryList.Tstcat2;
            if (libraryName == "&KL_") return Honsha01LibraryList.Kl_lib;
            if (libraryName == "&LFB") return Honsha01LibraryList.Lfblib;
            if (libraryName == "&LFE") return Honsha01LibraryList.Lfelib;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;
            if (libraryName == "&ILG") return Honsha01LibraryList.Ilglib;
            if (libraryName == "&JI_") return Honsha01LibraryList.Ji_lib;
            if (libraryName == "&F__") return Honsha01LibraryList.F__lib;
            if (libraryName == "&LFD") return Honsha01LibraryList.Lfdlib;
            if (libraryName == "&LFE") return Honsha01LibraryList.Lfelib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&LIBWK") return Honsha01LibraryList.Oplib;
            //if (libraryName == "&FLL") return Honsha01LibraryList.Wrkcat1; //JJIC10D0 JJIE20D0 JIIE30D0
            //if (libraryName == "&LIB") return Honsha01LibraryList.Wrkcat2;
            //if (libraryName == "*CURLIB") return Honsha01LibraryList.Usl2lib;
            //if (libraryName == "&FLL") return Honsha01LibraryList.Wrkcat2; //JJIC15D0
            /**

            
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&SEAT") return Honsha01LibraryList.Seatlib;

            if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;
            if (libraryName == "&LIB") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&WEB") return Honsha01LibraryList.Weblib;

            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&FLL") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;
            **/
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
