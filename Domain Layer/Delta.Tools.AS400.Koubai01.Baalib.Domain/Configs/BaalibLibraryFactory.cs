using Delta.AS400.Libraries;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.Koubai01;

namespace Delta.Tools.AS400.Koubai01.Baalib.Configs
{
    class BaalibLibraryFactory : LibraryFactory
    {

        BaalibLibraryFactory(List<Library> Libraries) : base(Libraries)
        {

        }

        public static BaalibLibraryFactory Of()
        {
            var ReferLibraries = new List<Library>(){
            Koubai01LibraryList.Baalib,
            Koubai01LibraryList.Comnlib,
            Koubai01LibraryList.Pialib,
            Koubai01LibraryList.Qgpl,
            Koubai01LibraryList.Vaalib,
            Koubai01LibraryList.Wrklib,
            //Koubai01LibraryList.Aidlib,
            //Koubai01LibraryList.Eigylib,
            //Koubai01LibraryList.Prodlib,
            //Koubai01LibraryList.Oplib,
            //Koubai01LibraryList.Hostlib,
            //Koubai01LibraryList.Kweblib,
            //Koubai01LibraryList.Gailib,
            //Koubai01LibraryList.Tanlib,
            //Koubai01LibraryList.Ukoblib,
            //Koubai01LibraryList.Vablib,
        };
            return new BaalibLibraryFactory(ReferLibraries);
        }

        //(QRJE QGPL QTEMP COMNLIB VAALIB WRKLIB +
        //                  QQRYLIB QEVX QFNT61 QFNTCPL DCSFNT +
        //                  AUTO400V3

        public override Library Create(string libraryName)
        {
            var lib = Libraries.Find(lib => lib.Name == libraryName);
            if (lib != null) return lib;

            if (libraryName == "&AAB") return Koubai01LibraryList.Aablib;
            if (libraryName == "&AAC") return Koubai01LibraryList.Aaclib;
            if (libraryName == "&AAE") return Koubai01LibraryList.Aaelib;
            if (libraryName == "&BAA") return Koubai01LibraryList.Baalib;
            if (libraryName == "&CMN") return Koubai01LibraryList.Comnlib;
            if (libraryName == "&MS2") return Koubai01LibraryList.Dtm2lib;
            if (libraryName == "&PIG") return Koubai01LibraryList.Piglib;
            if (libraryName == "&VAA") return Koubai01LibraryList.Vaalib;
            if (libraryName == "&VAB") return Koubai01LibraryList.Vablib;

            //if (libraryName == "&AID") return Koubai01LibraryList.Aidlib;
            //if (libraryName == "&PIA") return Koubai01LibraryList.Pialib;
            //if (libraryName == "&LFB") return Honsha01LibraryList.Lfblib;
            //if (libraryName == "&PRD") return Honsha01LibraryList.Prodlib;
            //if (libraryName == "&SEAT") return Honsha01LibraryList.Seatlib;
            //if (libraryName == "&WC2") return Honsha01LibraryList.Wrkcat2;
            //if (libraryName == "&YBC") return Honsha01LibraryList.Ybclib;

            //KOUBAI01.LIBDTAARA
            //COMNLIB   VAALIB    VABLIB    VACLIB    VADLIB    PIALIB    AABLIB    AACLIB    AAELIB    BAALIB    
            //AIDLIB    KCBLIB    PIGLIB    DTM2LIB                                                                
            //DTW1LIB   DTW2LIB   DTW3LIB   DTW4LIB   DTW5LIB   DTW6LIB   DTW7LIB   DTT1LIB   DTT2LIB                                                                           
            return Library.OfUnKnown(libraryName);
        }
    }
}
