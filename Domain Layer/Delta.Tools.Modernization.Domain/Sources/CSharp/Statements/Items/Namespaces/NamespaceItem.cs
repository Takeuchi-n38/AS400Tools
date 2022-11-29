using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Modernization.Statements.Items.Namespaces
{
    public class NamespaceItem
    {

        public readonly string Name;

        NamespaceItem(string name)
        {
            Name = name;
        }

        public static NamespaceItem Of(string name)
        {
            return new NamespaceItem(name);
        }

        public static NamespaceItem Of(IEnumerable<string> parts)
        {
            return Of(string.Join(@".", parts));
        }

        //public static NamespaceItem DeltaOf(IToClassification aLibrary)
        //{
        //    return Of(Delta.Concat(aLibrary.ToPascalCaseClassification()));
        //}

        //public static NamespaceItem DeltaOf(IToClassification aLibrary, string aLastName)
        //{
        //    return Of(Delta.Concat(aLibrary.ToPascalCaseClassification()).Append(aLastName));
        //}

        static IEnumerable<string> Delta = new List<string>() { "Delta" };

        public string ToUsingLine => $"using {Name};";

        public string ToNamespaceLine => $"namespace {Name}";

        #region
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

            var target = (NamespaceItem)obj;

            if (target.Name.ToUpper() != Name.ToUpper()) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        #endregion
    }
}
