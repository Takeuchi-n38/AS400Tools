using Delta.AS400.Libraries;
using Delta.AS400.Partitions;
using Delta.Koubai01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Koubai01.Axs.Tools.Configs
{
    class AxsLibraryFactory : LibraryFactory
    {

        AxsLibraryFactory(List<Library> Libraries) : base(Libraries)
        {
                   
        }
        public static Library Axs = Library.Of(Partition.Koubai01, "AXS");
        public static Library Aaflib = Library.Of(Partition.Koubai01, "AAFLIB");
        public static Library Qtemp = Library.Of(Partition.Koubai01, "QTEMP");

        public static AxsLibraryFactory Of(Library aMainLibrary)
        {
            var ReferLibraries = new List<Library>(){
                Axs,
                Koubai01LibraryList.Axd0001,
                Koubai01LibraryList.Vaalib,
                Aaflib,
                Koubai01LibraryList.Wsvflib,
                Koubai01LibraryList.Oplib,
                Koubai01LibraryList.Prodlib,
                Koubai01LibraryList.Comnlib,
                Koubai01LibraryList.Qgpl,
                //Koubai01LibraryList.Cds,
                Qtemp,
        };
            return new AxsLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;


            /**
            if (libraryName == "&CMN") return Koubai01LibraryList.Comnlib;
            if (libraryName == "&KL_") return Koubai01LibraryList.Kl_lib;
            if (libraryName == "&LFB") return Koubai01LibraryList.Lfblib;
            if (libraryName == "&LFE") return Koubai01LibraryList.Lfelib;
            
            if (libraryName == "&PRD") return Koubai01LibraryList.Prodlib;
            if (libraryName == "&SEAT") return Koubai01LibraryList.Seatlib;

            if (libraryName == "&YBC") return Koubai01LibraryList.Ybclib;
            if (libraryName == "&LIB") return Koubai01LibraryList.Dsldlib;
            if (libraryName == "&WEB") return Koubai01LibraryList.Weblib;
            if (libraryName == "&MIB") return Koubai01LibraryList.Miblib;
            if (libraryName == "&WC1") return Koubai01LibraryList.Wrkcat1;
            if (libraryName == "&FLL") return Koubai01LibraryList.Dsldlib;
            if (libraryName == "&LBLCHK") return Koubai01LibraryList.Lblchklib;
            **/
            if (libraryName == "&PRD") return Koubai01LibraryList.Prodlib;
            if (libraryName == "&LIBWK") return Koubai01LibraryList.Oplib;
            if (libraryName == "&LIB") return Koubai01LibraryList.Oplib;

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
