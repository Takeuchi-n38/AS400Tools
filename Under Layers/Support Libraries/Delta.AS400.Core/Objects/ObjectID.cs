using Delta.AS400.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Delta.AS400.Objects
{
    public class ObjectID//: IToClassification
    {

        public readonly Library Library;

        public readonly string Name;

        ObjectID(Library aLibrary, string aName)
        {
            Library = aLibrary;
            Name = aName;
        }

        public static ObjectID Of(Library aLibrary, string aName)
        {
            return new ObjectID(aLibrary, aName);
        }

        //public string Description => $"{Library.Description},{Name}";

        public ObjectID CreateWithSameLibrary(string objectName)
        {
            return new ObjectID(Library, objectName);
        }

        public ObjectID CreateWithDiffLibrary(Library library)
        {
            return new ObjectID(library, Name);
        }

        //public string ClassificationToString(string separator)
        //{
        //    return string.Join(separator, ToClassification());
        //}

        public IEnumerable<string> ToClassification()
        {
            yield return Library.Partition.Name;
            yield return Library.Name;
            yield return Name;
        }

        #region "equals"

        // override object.Equals
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

            var target = (ObjectID)obj;

            if (!target.Library.Equals(Library)) return false;

            if (target.Name.ToUpper() != Name.ToUpper()) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Library.GetHashCode() ^ Name.GetHashCode();
        }

        #endregion
    }
}
