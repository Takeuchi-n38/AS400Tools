using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Honsha01.Lkalib.Tools.Configs
{
    class LkalibLibraryFactory : LibraryFactory
    {

        LkalibLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }

        public static LkalibLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
                 Honsha01LibraryList.Lkalib,
                 Honsha01LibraryList.Prodlib,
                 Honsha01LibraryList.Oplib,
                 Honsha01LibraryList.Master2,
                 Honsha01LibraryList.Seatlib,
                 Honsha01LibraryList.Hostlib,
                 Honsha01LibraryList.Hi_lib,

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
            return new LkalibLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;

            if (libraryName == "&CMN") return Honsha01LibraryList.Comnlib;
            if (libraryName == "&MS2") return Honsha01LibraryList.Master2;
            if (libraryName == "&MS9") return Honsha01LibraryList.Master9;
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&WC1") return Honsha01LibraryList.Wrkcat1;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            if (libraryName == "&WC3") return Honsha01LibraryList.Wrkcat3;
            if (libraryName == "&WC4") return Honsha01LibraryList.Wrkcat4;
            if (libraryName == "&WC5") return Honsha01LibraryList.Wrkcat5;
            if (libraryName == "&WC6") return Honsha01LibraryList.Wrkcat6;
            if (libraryName == "&WC7") return Honsha01LibraryList.Wrkcat7;
            if (libraryName == "&TC1") return Honsha01LibraryList.Tstcat1;
            if (libraryName == "&TC2") return Honsha01LibraryList.Tstcat2;
            if (libraryName == "&SVF") return Honsha01LibraryList.Wsvflib;
            if (libraryName == "&ZZZ") return Honsha01LibraryList.Zzzlib;
            if (libraryName == "&HI_") return Honsha01LibraryList.Hi_lib;
            if (libraryName == "&F__") return Honsha01LibraryList.F__lib;
            if (libraryName == "&YB_") return Honsha01LibraryList.Yb_lib;
            if (libraryName == "&QAD") return Honsha01LibraryList.Qadlib;
            if (libraryName == "&QKD") return Honsha01LibraryList.Qkdlib;
            if (libraryName == "&QKB") return Honsha01LibraryList.Qkblib;
            if (libraryName == "&QKG") return Honsha01LibraryList.Qkglib;
            if (libraryName == "&QKA") return Honsha01LibraryList.Qkalib;
            if (libraryName == "&QKK") return Honsha01LibraryList.Qkklib;
            if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;
            if (libraryName == "&KL_") return Honsha01LibraryList.Kl_lib;
            if (libraryName == "&KI_") return Honsha01LibraryList.Ki_lib;
            if (libraryName == "&JI_") return Honsha01LibraryList.Ji_lib;
            if (libraryName == "&JJ_") return Honsha01LibraryList.Jj_lib;
            if (libraryName == "&LKA") return Honsha01LibraryList.Lkalib;
            if (libraryName == "&MIA") return Honsha01LibraryList.Mialib;
            if (libraryName == "&MIB") return Honsha01LibraryList.Miblib;
            if (libraryName == "&IIA") return Honsha01LibraryList.Iialib;
            if (libraryName == "&IIC") return Honsha01LibraryList.Iiclib;
            if (libraryName == "&IJA") return Honsha01LibraryList.Ijalib;
            if (libraryName == "&IJD") return Honsha01LibraryList.Ijdlib;
            if (libraryName == "&IJF") return Honsha01LibraryList.Ijflib;
            if (libraryName == "&EIA") return Honsha01LibraryList.Eialib;
            if (libraryName == "&EIC") return Honsha01LibraryList.Eiclib;
            if (libraryName == "&OAC") return Honsha01LibraryList.Oaclib;
            //if (libraryName == "&LIB") return Honsha01LibraryList.Liblib;
            if (libraryName == "&PIA") return Honsha01LibraryList.Pialib;
            if (libraryName == "&PIB") return Honsha01LibraryList.Piblib;
            if (libraryName == "&PIC") return Honsha01LibraryList.Piclib;
            if (libraryName == "&PID") return Honsha01LibraryList.Pidlib;
            if (libraryName == "&PIE") return Honsha01LibraryList.Pielib;
            if (libraryName == "&PIF") return Honsha01LibraryList.Piflib;
            if (libraryName == "&PIG") return Honsha01LibraryList.Piglib;
            if (libraryName == "&PIH") return Honsha01LibraryList.Pihlib;
            if (libraryName == "&PIJ") return Honsha01LibraryList.Pijlib;
            if (libraryName == "&HI_") return Honsha01LibraryList.Hi_lib;
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
