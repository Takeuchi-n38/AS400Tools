using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases
{
    public class View : ObjectOfSchema
    {

        public View(Schema aSchema, string aName) : base(aSchema, aName)
        {
        }

        public static View Of(Schema newSchema, string aName)
        {
            return new View(newSchema, aName);
        }

        public static View Of(string aSchemaName, string aName)
        {
            return Of(Schema.Of(aSchemaName), aName);
        }

    }
}
