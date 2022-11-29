using Delta.AS400.Libraries;
using Delta.AS400.Partitions;

namespace Delta.Honsha03.Domain
{
    public class Honsha03LibraryList
    {
        //ZIK2LIB
        public static Library Zik2lib = new Library(Partition.Honsha03, "Zik2LIB");
        public static Library Kl_lib = new Library(Partition.Honsha03, "KI_LIB");
        public static Library Lfblib = new Library(Partition.Honsha03, "LFBLIB");
        public static Library Lfelib = new Library(Partition.Honsha03, "LFELIB");
        public static Library Master2 = new Library(Partition.Honsha03, "MASTER2");
        public static Library Prodlib = new Library(Partition.Honsha03, "PRODLIB");
        public static Library Seatlib = new Library(Partition.Honsha03, "SEATLIB");
        public static Library Seedslib = new Library(Partition.Honsha03, "ZSEEDSLIB");
        public static Library Wrkcat2 = new Library(Partition.Honsha03, "WRKCAT2");
        public static Library Ybclib = new Library(Partition.Honsha03, "YBCLIB");
        public static Library Dsldlib = new Library(Partition.Honsha03, "DSLDLIB");
        public static Library Weblib = new Library(Partition.Honsha03, "WEBLIB");
        public static Library Miblib = new Library(Partition.Honsha03, "MIBLIB");
        public static Library Wrkcat1 = new Library(Partition.Honsha03, "WRKCAT1");
        public static Library Comnlib = new Library(Partition.Honsha03, "COMNLIB");



    }
}