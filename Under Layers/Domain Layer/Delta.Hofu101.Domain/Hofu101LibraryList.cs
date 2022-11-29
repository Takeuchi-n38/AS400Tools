using Delta.AS400.Libraries;
using Delta.AS400.Partitions;

namespace Delta.Hofu101
{
    public class Hofu101LibraryList
    {
        public static Library Actlib = new Library(Partition.Hofu101, "ACTLIB");
    }
}