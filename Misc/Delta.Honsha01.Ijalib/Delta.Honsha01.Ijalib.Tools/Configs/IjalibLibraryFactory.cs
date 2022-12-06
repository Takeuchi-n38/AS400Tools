using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.Ijalib.Tools.Configs
{
    class IjalibLibraryFactory : LibraryFactory
    {

        IjalibLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }

        public static IjalibLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
                 Honsha01LibraryList.Ijalib,
                 Honsha01LibraryList.Oplib,
                 Honsha01LibraryList.Jj_lib,
                 Honsha01LibraryList.Prodlib,
                 Honsha01LibraryList.Master2,
                 Honsha01LibraryList.Miblib,
                 Honsha01LibraryList.Qgpl,
                 Honsha01LibraryList.Eialib,
                 Honsha01LibraryList.Siknqry,
                 Honsha01LibraryList.Wrkcat1,
                 Honsha01LibraryList.Wrkcat2,
                 Honsha01LibraryList.Wrkcat4,
                 Honsha01LibraryList.Wklib,
                 Honsha01LibraryList.Ybclib,
                 Honsha01LibraryList.Weblib,
                 Honsha01LibraryList.Ddmlib,
                 Honsha01LibraryList.Nblib,
                 Honsha01LibraryList.Eialib,
                 Honsha01LibraryList.Toyolib,
                 Honsha01LibraryList.Oaclib,
                 Honsha01LibraryList.F__lib,
                 Honsha01LibraryList.Qtemp,
                 Honsha01LibraryList.Pialib,
                 Honsha01LibraryList.Actlib,
                 Honsha01LibraryList.Iielib,

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
            return new IjalibLibraryFactory(ReferLibraries);
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
            //if (libraryName == "&LIB") return Honsha01LibraryList.Master2; //JEIF05D0
            if (libraryName == "&LIB") return Honsha01LibraryList.Miblib; //JIJA05D0
            if (libraryName == "&WEB") return Honsha01LibraryList.Weblib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&FLL") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&LBLCHK") return Honsha01LibraryList.Lblchklib;
            if (libraryName == "&WC3") return Honsha01LibraryList.Wrkcat3;
            if (libraryName == "&WC4") return Honsha01LibraryList.Wrkcat4;
            if (libraryName == "&LIBWK") return Honsha01LibraryList.Oplib;
            if (libraryName == "&IJA") return Honsha01LibraryList.Ijalib;
            if (libraryName == "&SVF") return Honsha01LibraryList.Wsvflib;
            if (libraryName == "&OAC") return Honsha01LibraryList.Oaclib;
            if (libraryName == "&F__") return Honsha01LibraryList.F__lib;
            if (libraryName == "&PIA") return Honsha01LibraryList.Pialib;

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
