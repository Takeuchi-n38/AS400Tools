using System;

namespace Delta.AS400.Partitions
{
    public class Partition
    {
        public readonly string Name;
        Partition(string partitionName)
        {
            Name = partitionName;
        }

        public static Partition Of(string partitionName)
        {
            return new Partition(partitionName);
        }

        public static Partition Test = Of("TEST");
        public static Partition Hofu101 = Of("HOFU101");
        public static Partition Honsha01 = Of("HONSHA01");
        public static Partition Honsha03 = Of("HONSHA03");
        public static Partition Koubai01 = Of("KOUBAI01");
        public static Partition UnknownPartition = Of("UnknownPartition");
        public bool IsUnKnown => this.Equals(UnknownPartition);

    }
}
