using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataAreas
{
    public class DataArea
    {
        public ObjectID ObjectID;
        public Library Library => ObjectID.Library;

        public string Name => ObjectID.Name;

        DataArea(ObjectID aObjectID)
        {
            ObjectID = aObjectID;
        }
        public static DataArea Of(ObjectID aObjectID)
        {
            return new DataArea(aObjectID);
        }

        public static DataArea Of(Library library, string objectName)
        {
            return Of(ObjectID.Of(library, objectName));
        }

        //public bool IsUnKnown => this.Library.IsUnKnown;

        public string Description => $"{string.Join(",", Library.ToClassification())},{Name}";

        public IEnumerable<string> ToPascalCaseClassification => ToClassification().Select(n => n.ToPascalCase());

        public string ClassificationToString(string separator)
        {
            return string.Join(separator, ToClassification());
        }

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

            var target = (DataArea)obj;

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
