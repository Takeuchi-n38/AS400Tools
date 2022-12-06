﻿using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.Hi_lib.Tools.Configs
{
    class Hi_libLibraryFactory : LibraryFactory
    {

        Hi_libLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }

        public static Hi_libLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
                 Honsha01LibraryList.Hi_lib,
                 Honsha01LibraryList.Oplib,
                 Honsha01LibraryList.Wsvflib,
                 Honsha01LibraryList.Wklib,
                 Honsha01LibraryList.Master2,
                 Honsha01LibraryList.Wrkcat2,
                 Honsha01LibraryList.F__lib,
                 Honsha01LibraryList.Mduklib,
                 Honsha01LibraryList.Mknrlib,
                 Honsha01LibraryList.Prodlib,
                 Honsha01LibraryList.Qtemp,
                 Honsha01LibraryList.Dashlib,
                 Honsha01LibraryList.Ddmlib,
                 Honsha01LibraryList.Ji_lib,
                 Honsha01LibraryList.Seedslib,
                 Honsha01LibraryList.Ujjhlib,
                 Honsha01LibraryList.Seatlib,
                 Honsha01LibraryList.Wrkcat1,
                 Honsha01LibraryList.Hostlib,
                 Honsha01LibraryList.Carlib,
                 Honsha01LibraryList.Act2lib,
                 Honsha01LibraryList.Ijdlib,
                 Honsha01LibraryList.Siknqry,
                 Honsha01LibraryList.Optlib,
                 Honsha01LibraryList.Wrkcat6,
                 Honsha01LibraryList.Wrkcat7,

                //=========ここから下調査用===============
                /**
        Honsha01LibraryList.Dsldlib,
        Honsha01LibraryList.Lfblib,
        Honsha01LibraryList.Lfelib,
        Honsha01LibraryList.Prodlib,
        Honsha01LibraryList.Seatlib,
        Honsha01LibraryList.Kl_lib,
        Honsha01LibraryList.Qgpl,
        Honsha01LibraryList.Comnlib,
        Honsha01LibraryList.Oplib,
        Honsha01LibraryList.Ybclib,
        Honsha01LibraryList.Ytstilib,
        Honsha01LibraryList.Yagilib,
        Honsha01LibraryList.Usl2lib,
        Honsha01LibraryList.Hostlib,
        Honsha01LibraryList.Weblib,
        Honsha01LibraryList.Miblib,
        Honsha01LibraryList.Toyolib,
        Honsha01LibraryList.Master2,
        Honsha01LibraryList.Wrkcat1,
                **/
        
            };
            return new Hi_libLibraryFactory(ReferLibraries);
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
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&SEAT") return Honsha01LibraryList.Seatlib;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;
            //if (libraryName == "&LIB") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&LIB") return Honsha01LibraryList.Siknqry;
            if (libraryName == "&WEB") return Honsha01LibraryList.Weblib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            //if (libraryName == "&FLL") return Honsha01LibraryList.Dsldlib;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;
            if (libraryName == "&WC3") return Honsha01LibraryList.Wrkcat3;
            if (libraryName == "&WC6") return Honsha01LibraryList.Wrkcat6;
            if (libraryName == "&LIBWK") return Honsha01LibraryList.Oplib;
            if (libraryName == "&HI_") return Honsha01LibraryList.Hi_lib;
            if (libraryName == "&IJD") return Honsha01LibraryList.Ijdlib;
            if (libraryName == "&TC1") return Honsha01LibraryList.Tstcat1;

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
