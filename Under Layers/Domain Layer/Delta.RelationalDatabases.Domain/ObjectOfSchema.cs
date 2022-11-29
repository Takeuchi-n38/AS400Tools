using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases
{
    public class ObjectOfSchema
    {
        public readonly string Name;

        public readonly Schema Schema;

        public string SchemaName => Schema.Name;

        public string FullName => Schema.Name + "." + Name;

        protected ObjectOfSchema(Schema aSchema, string aName)
        {
            Schema = aSchema;
            Name = aName;
        }
    }
}
