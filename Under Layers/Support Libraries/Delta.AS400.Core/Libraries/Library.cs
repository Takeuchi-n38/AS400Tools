using Delta.AS400.Objects;
using Delta.AS400.Partitions;
using System;
using System.Collections.Generic;

namespace Delta.AS400.Libraries

{
    public class Library//: IToClassification
    {

        public readonly Partition Partition;

        public readonly string Name;

        public Library(Partition partition, string name)
        {
            Partition = partition;
            Name = name;
        }

        //public string Description => $"{Partition.Name},{Name}";

        public Library Of(string elseLibranyName)
        {
            return new Library(Partition, elseLibranyName);
        }
        public ObjectID ObjectIDOf(string objectName)
        {
            return ObjectID.Of(this, objectName);
        }

        public static Library Of(Partition partition, string name)
        {
            return new Library(partition, name);
        }

        public static Library OfUnKnown(string unKnownLibraryName)
        {
            return new Library(Partition.UnknownPartition, unKnownLibraryName);
        }
        public static Library OfUnKnown()
        {
            return OfUnKnown("UnknownLibrary");
        }

        public IEnumerable<string> ToClassification()
        {
            yield return Partition.Name;
            yield return Name;
        }

        #region "equals"
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var target = (Library)obj;

            if (target.Partition.Name.ToUpper() != Partition.Name.ToUpper()) return false;

            if (target.Name.ToUpper() != Name.ToUpper()) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Partition.Name.GetHashCode() ^ Name.GetHashCode();
        }
        #endregion
    }
}
