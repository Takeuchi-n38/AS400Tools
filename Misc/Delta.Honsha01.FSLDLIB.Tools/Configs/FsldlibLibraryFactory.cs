﻿using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.Fsldlib.Tools.Configs
{
    class FsldlibLibraryFactory : LibraryFactory
    {

        FsldlibLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }

        public static FsldlibLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
                Honsha01LibraryList.Fsldlib,
                Honsha01LibraryList.Usl2lib,
                Honsha01LibraryList.Usl3lib,
                Honsha01LibraryList.Dsldlib,
                Honsha01LibraryList.Hostlib,
                Honsha01LibraryList.Toyolib,
                Honsha01LibraryList.Master2,
                Honsha01LibraryList.Prodlib,
                Honsha01LibraryList.Oplib
                /**,

                Honsha01LibraryList.Usldlib,
                Honsha01LibraryList.Wrkcat2,


                Honsha01LibraryList.Ji_lib,
                Honsha01LibraryList.Qgpl,

                Honsha01LibraryList.Act2lib,
                Honsha01LibraryList.Rrnlib,
                Honsha01LibraryList.Wrkcat2,

                **/

                /**,
                Honsha01LibraryList.Ujjelib,
                Honsha01LibraryList.Ukelib,
                Honsha01LibraryList.Prodlib,

                Honsha01LibraryList.Weblib,
                Honsha01LibraryList.Wrkcat2,
                Honsha01LibraryList.Lfblib,
                Honsha01LibraryList.Cslib,
                Honsha01LibraryList.Jj_lib,//AUTOSTARTで呼んでいそう

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

        
        Honsha01LibraryList.Wrkcat1
        **/
        };
            return new FsldlibLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;
            //if (libraryName == "*CURLIB") return Honsha01LibraryList.Usl2lib;
            //if (libraryName == "&LIB") return Honsha01LibraryList.Usl2lib;
            if (libraryName == "&LIB") return Honsha01LibraryList.Fsldlib;

            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&MS2") return Honsha01LibraryList.Master2;
            if (libraryName == "&US3") return Honsha01LibraryList.Usl3lib;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;

            if (libraryName == "&CMN") return Honsha01LibraryList.Comnlib;
            if (libraryName == "&KL_") return Honsha01LibraryList.Kl_lib;
            if (libraryName == "&LFB") return Honsha01LibraryList.Lfblib;
            if (libraryName == "&LFE") return Honsha01LibraryList.Lfelib;
            
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&SEAT") return Honsha01LibraryList.Seatlib;

            if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;

            if (libraryName == "&WEB") return Honsha01LibraryList.Weblib;

            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&FLL") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;

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
