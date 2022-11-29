using Delta.AS400.Libraries;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.Honsha01;

namespace Delta.Tools.AS400.Honsha01.Jj_lib.Configs
{
    class Jj_libLibraryFactory : LibraryFactory
    {

        Jj_libLibraryFactory(List<Library> Libraries) : base(Libraries)
        {

        }

        public static Jj_libLibraryFactory Of()
        {
            var ReferLibraries = new List<Library>(){
        Honsha01LibraryList.Jj_lib,
        Honsha01LibraryList.Prodlib,
        Honsha01LibraryList.Comnlib,
        Honsha01LibraryList.F__lib,
        Honsha01LibraryList.Master2,
        Honsha01LibraryList.Master9,
        Honsha01LibraryList.Wrkcat2,
        Honsha01LibraryList.Wrkcat4,
        Honsha01LibraryList.Wsvflib,
        Honsha01LibraryList.Oplib,
        //Honsha01LibraryList.Qgpl,
        //Honsha01LibraryList.Ybclib,
        //Honsha01LibraryList.Yagilib,
        };
            return new Jj_libLibraryFactory(ReferLibraries);
        }

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;
            //WRKCAT1   WRKCAT3   WRKCAT5   WRKCAT6   WRKCAT7                                 
            //TSTCAT1   TSTCAT2                                                                                   
            // RTVDTAARA DTAARA(LIBDTAARA (101 10)) RTNVAR(&WC1)
            // RTVDTAARA DTAARA(LIBDTAARA (121 10)) RTNVAR(&WC3)
            // RTVDTAARA DTAARA(LIBDTAARA (141 10)) RTNVAR(&WC5)
            // RTVDTAARA DTAARA(LIBDTAARA (151 10)) RTNVAR(&WC6)
            // RTVDTAARA DTAARA(LIBDTAARA (161 10)) RTNVAR(&WC7)
            // RTVDTAARA DTAARA(LIBDTAARA (201 10)) RTNVAR(&TC1)
            // RTVDTAARA DTAARA(LIBDTAARA (211 10)) RTNVAR(&TC2)

            if (libraryName == "&CMN") return Honsha01LibraryList.Comnlib;
            if (libraryName == "&MS2") return Honsha01LibraryList.Master2;
            if (libraryName == "&MS9") return Honsha01LibraryList.Master9;
            if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            if (libraryName == "&WC4") return Honsha01LibraryList.Wrkcat4;
            if (libraryName == "&SVF") return Honsha01LibraryList.Wsvflib;

            return Library.OfUnKnown(libraryName);
        }
    }
}
